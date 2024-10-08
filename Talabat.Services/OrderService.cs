﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,
                            IUnitOfWork unitOfWork,
                            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var Basket = await _basketRepository.GetBaskeyAsync(basketId);
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    OrderItems.Add(orderItem);
                }
            }

            var subTotal = OrderItems.Sum(item => item.Quantity * item.Price);

            var deliviryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var Spec = new OrderWithPaymentIntentSpecification(Basket.PaymentIntentId);
            var ExOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);

            if(ExOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(ExOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }
            var order = new Order(buyerEmail, shippingAddress, deliviryMethod, OrderItems, subTotal, Basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var Result = await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            return order;
        }

        public Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var Spec = new OrderSpecifications(buyerEmail, orderId);
            var Order = _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);
            return Order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var Spec = new OrderSpecifications(buyerEmail);
            var Orders = _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return Orders;
        }
    }
}
