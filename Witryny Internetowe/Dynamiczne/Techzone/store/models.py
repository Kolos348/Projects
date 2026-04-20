from django.db import models


class Brand(models.Model):
    name       = models.CharField(max_length=100, unique=True)
    slug       = models.SlugField(max_length=100, unique=True)
    logo_url   = models.CharField(max_length=500, blank=True, null=True)
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table = 'brands'

    def __str__(self):
        return self.name


class Category(models.Model):
    name      = models.CharField(max_length=100)
    slug      = models.SlugField(max_length=100, unique=True)
    icon      = models.CharField(max_length=10, blank=True, null=True)
    parent    = models.ForeignKey('self', on_delete=models.SET_NULL, null=True, blank=True)
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table = 'categories'
        verbose_name_plural = 'categories'

    def __str__(self):
        return self.name


class Product(models.Model):
    BADGE_CHOICES = [
        ('sale', 'Sale'),
        ('new',  'New'),
        ('hot',  'Hot'),
    ]

    name         = models.CharField(max_length=255)
    slug         = models.SlugField(max_length=255, unique=True)
    description  = models.TextField(blank=True, null=True)
    price        = models.DecimalField(max_digits=10, decimal_places=2)
    old_price    = models.DecimalField(max_digits=10, decimal_places=2, null=True, blank=True)
    stock        = models.IntegerField(default=0)
    brand        = models.ForeignKey(Brand, on_delete=models.SET_NULL, null=True)
    category     = models.ForeignKey(Category, on_delete=models.SET_NULL, null=True)
    badge        = models.CharField(max_length=20, choices=BADGE_CHOICES, null=True, blank=True)
    rating       = models.DecimalField(max_digits=3, decimal_places=2, default=0)
    review_count = models.IntegerField(default=0)
    image_url    = models.CharField(max_length=500, blank=True, null=True)
    is_active    = models.BooleanField(default=True)
    created_at   = models.DateTimeField(auto_now_add=True)
    updated_at   = models.DateTimeField(auto_now=True)

    class Meta:
        db_table = 'products'

    def __str__(self):
        return self.name


class Cart(models.Model):
    user       = models.ForeignKey('auth.User', on_delete=models.CASCADE, null=True, blank=True)
    session_id = models.CharField(max_length=255, blank=True, null=True)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)

    class Meta:
        db_table = 'carts'


class CartItem(models.Model):
    cart       = models.ForeignKey(Cart, on_delete=models.CASCADE, related_name='items')
    product    = models.ForeignKey(Product, on_delete=models.CASCADE)
    quantity   = models.IntegerField(default=1)
    added_at   = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table = 'cart_items'
        unique_together = ('cart', 'product')


class Order(models.Model):
    STATUS_CHOICES = [
        ('pending',   'Pending'),
        ('confirmed', 'Confirmed'),
        ('shipped',   'Shipped'),
        ('delivered', 'Delivered'),
        ('cancelled', 'Cancelled'),
    ]

    user            = models.ForeignKey('auth.User', on_delete=models.SET_NULL, null=True)
    status          = models.CharField(max_length=30, choices=STATUS_CHOICES, default='pending')
    total_price     = models.DecimalField(max_digits=10, decimal_places=2)
    shipping_name   = models.CharField(max_length=200, blank=True)
    shipping_street = models.CharField(max_length=255, blank=True)
    shipping_city   = models.CharField(max_length=100, blank=True)
    shipping_zip    = models.CharField(max_length=20, blank=True)
    shipping_phone  = models.CharField(max_length=20, blank=True)
    notes           = models.TextField(blank=True)
    created_at      = models.DateTimeField(auto_now_add=True)
    updated_at      = models.DateTimeField(auto_now=True)

    class Meta:
        db_table = 'orders'


class OrderItem(models.Model):
    order        = models.ForeignKey(Order, on_delete=models.CASCADE, related_name='items')
    product      = models.ForeignKey(Product, on_delete=models.SET_NULL, null=True)
    product_name = models.CharField(max_length=255)
    quantity     = models.IntegerField()
    unit_price   = models.DecimalField(max_digits=10, decimal_places=2)

    class Meta:
        db_table = 'order_items'
