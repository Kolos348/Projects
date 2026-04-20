from django.urls import path
from . import views

urlpatterns = [
    # Main shop page
    path('', views.shop, name='shop'),

    # Products API — JS fetch() calls this
    path('api/products/', views.products_api, name='products_api'),

    # Cart API
    path('api/cart/',        views.cart_api,    name='cart_api'),
    path('api/cart/add/',    views.cart_add,    name='cart_add'),
    path('api/cart/remove/', views.cart_remove, name='cart_remove'),
    path('api/cart/update/', views.cart_update, name='cart_update'),
]
