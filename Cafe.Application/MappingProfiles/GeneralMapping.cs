using AutoMapper;
using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.DTOs.Orders;
using Cafe.Application.DTOs.Payments;
using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Products;
using Cafe.Application.DTOs.Tables;
using Cafe.Application.DTOs.Users;
using Cafe.Domain.Entities;

namespace Cafe.Application.MappingProfiles
{
    public class GeneralMapping : Profile
    {


        public GeneralMapping()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<ProductIngredient, ProductIngredientCreateDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>()
    .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.ProductIngredients));


            CreateMap<Table, TableCreateDto>().ReverseMap();
            CreateMap<Table, TableGetDto>()
     .ForMember(dest => dest.ActiveOrders, opt => opt.MapFrom(src =>
         src.Orders.Select(o => new OrderSummaryDto
         {
             OrderId = o.Id,
             CreatedAt = o.CreatedAt,
             TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
         }).ToList()
     ));
            CreateMap<Table, TableUpdateDto>().ReverseMap();

            CreateMap<Payment, PaymentCreateDto>().ReverseMap();
            CreateMap<Payment, PaymentUpdateDto>().ReverseMap();
            CreateMap<Payment, PaymentGetDto>().ReverseMap();

            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemsCreateDto>().ReverseMap();


            //  CreateMap<Order, OrderGetDto>().ReverseMap();
            //   CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>()
    .ForMember(dest => dest.OrderItems, opt => opt.Ignore()); // OrderItems elle kontrol ediliyor

            CreateMap<OrderItemsCreateDto, OrderItem>();


            // AutoMapper profile:
            CreateMap<Order, OrderGetDto>()
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Table.Name))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));


            CreateMap<Ingredient, IngredientCreateDto>().ReverseMap();
            CreateMap<Ingredient, IngredientUpdateDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<ProductIngredient, ProductIngredientCreateDto>().ReverseMap();
            CreateMap<ProductIngredient, ProductIngredientGetDto>().ReverseMap();

            CreateMap<ProductIngredient, ProductIngredientAddDto>().ReverseMap();
            CreateMap<ProductIngredient, ProductIngredientUpdateDto>().ReverseMap();
            CreateMap<ProductIngredient, ProductIngredientDto>().ReverseMap();


            CreateMap<Ingredient, StockAlertDto>()
    .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Name));


            CreateMap<UserCreateDto, User>()
    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
    .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<User, UserForLoginDto>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


            CreateMap<OperationClaim, OperationClaimCreateDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimUpdateDto>().ReverseMap();



            CreateMap<UserOperationClaim, UserOperationClaimCreateDto>().ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimListDto>().ReverseMap();





        }
    }
}
