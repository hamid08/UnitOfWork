using AutoMapper;
using RohaniShop.Entities;
using RohaniShop.Entities.identity;
using RohaniShop.ViewModels.Manage;
using RohaniShop.ViewModels.RoleManager;
using RohaniShop.ViewModels.UserManager;
using System;
using System.Collections.Generic;
using System.Text;
using RohaniShop.ViewModels;
using RohaniShop.ViewModels.Customer;
using RohaniShop.ViewModels.Driver;
using RohaniShop.ViewModels.Factor;
using RohaniShop.ViewModels.FactorProduct;
using RohaniShop.ViewModels.Product;

namespace RohaniShop.IocConfig.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Role, RolesViewModel>().ReverseMap()
                    .ForMember(p => p.Users, opt => opt.Ignore())
                    .ForMember(p => p.Claims, opt => opt.Ignore());

            

            CreateMap<User, UsersViewModel>().ReverseMap()
                  .ForMember(p => p.Claims, opt => opt.Ignore());

            CreateMap<User, ProfileViewModel>().ReverseMap()
                   .ForMember(p => p.Claims, opt => opt.Ignore());
          
            CreateMap<Customer, CustomersViewModel>().ReverseMap();
            CreateMap<Driver, DriversViewModel>().ReverseMap();
            CreateMap<Product, ProductsViewModel>().ReverseMap();
            CreateMap<Factor, FactorsViewModel>().ReverseMap();
            CreateMap<FactorProduct, FactorProductsViewModel>().ReverseMap();



        }
    }
}
