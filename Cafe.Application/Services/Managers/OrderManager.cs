using AutoMapper;
using Cafe.Application.DTOs.Orders;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Application.Utilities.Results;
using Cafe.Domain;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Managers
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IOrderItemDal _orderItemDal;
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        private readonly ITableDal _tableDal;


        public OrderManager(IOrderDal orderDal, IMapper mapper, IProductDal productDal, IOrderItemDal orderItemDal, ITableDal tableDal)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _productDal = productDal;
            _orderItemDal = orderItemDal;
            _tableDal = tableDal;
        }

        public async Task<IResult> Add(OrderCreateDto orderCreateDto)
        {
            // ✅ TableId geçerli mi?
            var table = await _tableDal.GetAsync(t => t.Id == orderCreateDto.TableId);
            if (table == null)
                return new ErrorResult($"Geçersiz masa numarası: {orderCreateDto.TableId}");


            // 1. Order nesnesini oluştur
            var newOrder = new Order
            {
                TableId = orderCreateDto.TableId,
                CreatedAt = DateTime.Now,
                IsPaid = false
            };
         
            // 2. Önce veritabanına yaz ki Id oluşsun
            await _orderDal.AddAsync(newOrder); // içinde SaveChangesAsync() olmalı

            // 3. Ürünleri bul ve OrderItems olarak kaydet
            foreach (var item in orderCreateDto.Items)
            {
                var product = await _productDal.GetAsync(p => p.Id == item.ProductId);
                if (product == null) continue;
                // 🔍 Yeterli stok kontrolü (opsiyonel ama önerilir)
                if (product.Stock < item.Quantity)
                    return new ErrorResult($"Yetersiz ürün stoğu: {product.Name} (Mevcut: {product.Stock}, İstenen: {item.Quantity})");

                // 🔻 Stok düş
                product.Stock -= item.Quantity;
                await _productDal.UpdateAsync(product);

                // ⚠️ Kritik stok kontrolü
                if (product.Stock <= product.MinStockThreshold)
                {
                    Console.WriteLine($"⚠️ Ürün stoğu kritik seviyeye düştü: {product.Name} (Mevcut: {product.Stock})");
                }

                // Sipariş satırını ekle
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    OrderId = newOrder.Id // 🔥 artık OrderId var
                };

                await _orderItemDal.AddAsync(orderItem); // OrderItem ayrı ekleniyor
            }

            return new SuccessResult("Sipariş başarıyla eklendi.");
        }


        public async Task<IResult> Delete(Order order)
        {
            await _orderDal.DeleteAsync(order);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<OrderGetDto>>> GetAllAsync()
        {
            var orders = await _orderDal.GetAllWithDetailsAsync();

            // AutoMapper ile Entity -> DTO dönüşümü
            var orderDtos = _mapper.Map<List<OrderGetDto>>(orders);


            return new SuccessDataResult<List<OrderGetDto>>(orderDtos);

        }

        public async Task<IDataResult<OrderGetDto>> GetById(int id)
        {

            var order = await _orderDal.GetOrderWithDetailsAsync(id);

            if (order == null)
                return new ErrorDataResult<OrderGetDto>("Sipariş bulunamadı.");

            var dto = _mapper.Map<OrderGetDto>(order);
            return new SuccessDataResult<OrderGetDto>(dto);
        }

        public async Task<IResult> Update(OrderUpdateDto orderUpdateDto)
        {
            var existingOrder = await _orderDal.GetAsync(o => o.Id == orderUpdateDto.Id);
            if (existingOrder == null)
                return new ErrorResult("Sipariş bulunamadı.");

            // Eski sipariş ürünlerini silmeden önce stokları geri ekle
            var oldItems = await _orderItemDal.GetAllAsync(oi => oi.OrderId == existingOrder.Id);
            foreach (var item in oldItems)
            {
                var product = await _productDal.GetAsync(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Stock += item.Quantity;
                    await _productDal.UpdateAsync(product);
                }

                await _orderItemDal.DeleteAsync(item);
            }

            // Yeni ürünleri işle
            foreach (var itemDto in orderUpdateDto.Items)
            {
                var product = await _productDal.GetAsync(p => p.Id == itemDto.ProductId);
                if (product == null)
                    continue;

                if (product.Stock < itemDto.Quantity)
                    return new ErrorResult($"Yetersiz ürün stoğu: {product.Name} (Mevcut: {product.Stock}, İstenen: {itemDto.Quantity})");

                product.Stock -= itemDto.Quantity;
                await _productDal.UpdateAsync(product);

                if (product.Stock <= product.MinStockThreshold)
                    Console.WriteLine($"⚠️ Ürün stoğu kritik seviyeye düştü: {product.Name} (Mevcut: {product.Stock})");

                var newOrderItem = _mapper.Map<OrderItem>(itemDto);
                newOrderItem.OrderId = existingOrder.Id;
                newOrderItem.UnitPrice = product.Price; // 💡 doğru fiyatla kaydet

                await _orderItemDal.AddAsync(newOrderItem);
            }

            return new SuccessResult("Sipariş başarıyla güncellendi.");
        }

        public async Task<IResult> DeleteOrderAsync(int orderId)
        {
            var order = await _orderDal.GetAsync(o => o.Id == orderId);
            if (order == null)
                return new ErrorResult("Sipariş bulunamadı.");

            if (order.IsPaid)
                return new ErrorResult("Ödenmiş sipariş silinemez.");

            await _orderDal.DeleteAsync(order);
            return new SuccessResult("Sipariş başarıyla silindi.");
        }


    }
}
