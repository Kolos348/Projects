import json
from django.http import JsonResponse
from django.shortcuts import render
from django.views.decorators.http import require_GET, require_POST
from django.views.decorators.csrf import csrf_exempt
from .models import Product, Brand, Category, Cart, CartItem, Order, OrderItem


# ── MAIN SHOP PAGE ──
def shop(request):
    brands     = Brand.objects.all()
    categories = Category.objects.all()
    return render(request, 'store/shop.html', {
        'brands':     brands,
        'categories': categories,
    })


# ── PRODUCTS API — called by JS fetch() ──
@require_GET
def products_api(request):
    qs = Product.objects.filter(is_active=True).select_related('brand', 'category')

    # Category filter
    cat = request.GET.get('category')
    if cat and cat != 'all':
        qs = qs.filter(category__slug=cat)

    # Price filter
    min_price = request.GET.get('min_price')
    max_price = request.GET.get('max_price')
    if min_price: qs = qs.filter(price__gte=min_price)
    if max_price: qs = qs.filter(price__lte=max_price)

    # Brand filter — multiple brands sent as ?brand=Apple&brand=Samsung
    brands = request.GET.getlist('brand')
    if brands:
        qs = qs.filter(brand__name__in=brands)

    # Rating filter
    min_rating = request.GET.get('min_rating')
    if min_rating and float(min_rating) > 0:
        qs = qs.filter(rating__gte=min_rating)

    # Search
    q = request.GET.get('q', '').strip()
    if q:
        qs = qs.filter(name__icontains=q) | qs.filter(brand__name__icontains=q)

    # Sort
    sort_map = {
        'price-asc':  'price',
        'price-desc': '-price',
        'newest':     '-created_at',
        'rating':     '-rating',
        'popular':    '-review_count',
    }
    sort = request.GET.get('sort', 'default')
    qs = qs.order_by(sort_map.get(sort, 'id'))

    # Serialize
    products = []
    for p in qs:
        products.append({
            'id':           p.id,
            'name':         p.name,
            'brand':        p.brand.name if p.brand else '',
            'category':     p.category.slug if p.category else '',
            'price':        float(p.price),
            'old_price':    float(p.old_price) if p.old_price else None,
            'rating':       float(p.rating),
            'review_count': p.review_count,
            'badge':        p.badge,
            'stock':        p.stock,
            'image_url':    p.image_url or '',
        })

    return JsonResponse({'products': products, 'count': len(products)})


# ── CART API ──
def get_or_create_cart(request):
    if request.user.is_authenticated:
        cart, _ = Cart.objects.get_or_create(user=request.user)
    else:
        session_id = request.session.session_key
        if not session_id:
            request.session.create()
            session_id = request.session.session_key
        cart, _ = Cart.objects.get_or_create(session_id=session_id)
    return cart


@require_GET
def cart_api(request):
    cart = get_or_create_cart(request)
    items = []
    for item in cart.items.select_related('product'):
        items.append({
            'id':        item.product.id,
            'name':      item.product.name,
            'brand':     item.product.brand.name if item.product.brand else '',
            'price':     float(item.product.price),
            'image_url': item.product.image_url or '',
            'quantity':  item.quantity,
            'subtotal':  float(item.product.price * item.quantity),
        })
    total = sum(i['subtotal'] for i in items)
    return JsonResponse({'items': items, 'total': total, 'count': sum(i['quantity'] for i in items)})


@csrf_exempt
@require_POST
def cart_add(request):
    data       = json.loads(request.body)
    product_id = data.get('product_id')
    quantity   = int(data.get('quantity', 1))
    try:
        product = Product.objects.get(id=product_id, is_active=True)
    except Product.DoesNotExist:
        return JsonResponse({'error': 'Product not found'}, status=404)
    cart = get_or_create_cart(request)
    item, created = CartItem.objects.get_or_create(cart=cart, product=product)
    if not created:
        item.quantity += quantity
    else:
        item.quantity = quantity
    item.save()
    total_count = sum(i.quantity for i in cart.items.all())
    return JsonResponse({'success': True, 'cart_count': total_count})


@csrf_exempt
@require_POST
def cart_remove(request):
    data       = json.loads(request.body)
    product_id = data.get('product_id')
    cart = get_or_create_cart(request)
    cart.items.filter(product_id=product_id).delete()
    total_count = sum(i.quantity for i in cart.items.all())
    return JsonResponse({'success': True, 'cart_count': total_count})


@csrf_exempt
@require_POST
def cart_update(request):
    data       = json.loads(request.body)
    product_id = data.get('product_id')
    quantity   = int(data.get('quantity', 1))
    cart = get_or_create_cart(request)
    try:
        item = cart.items.get(product_id=product_id)
        if quantity <= 0:
            item.delete()
        else:
            item.quantity = quantity
            item.save()
    except CartItem.DoesNotExist:
        pass
    total_count = sum(i.quantity for i in cart.items.all())
    return JsonResponse({'success': True, 'cart_count': total_count})
