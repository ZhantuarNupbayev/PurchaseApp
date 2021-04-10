using AutoMapper;
using DataLayer.Entities;
using DataTransfer.DTO.Categories;
using DataTransfer.DTO.Products;
using DataTransfer.DTO.Purchases;
using DataTransfer.DTO.Users;

namespace DataTransfer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<CategoryForCreationDto, Category>();

            CreateMap<CategoryForUpdateDto, Category>();

            // Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductForCreationDto, Product>();

            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>().ReverseMap();

            // User
            CreateMap<UserForRegistrationDto, User>();

            // Purchase
            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();

            CreateMap<PurchaseForCreationDto, Purchase>();

            CreateMap<PurchaseForUpdateDto, Purchase>();
            CreateMap<PurchaseForUpdateDto, Purchase>().ReverseMap();

        }
       
    }
}
