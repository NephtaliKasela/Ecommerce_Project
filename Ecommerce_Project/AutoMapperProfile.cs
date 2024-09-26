using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce_Project.Controllers;
using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.City;
using Ecommerce_Project.DTOs.Continent;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Store;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;

namespace Ecommerce_Project
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
			// Continent
			CreateMap<Continent, GetContinentDTO>();
			CreateMap<UpdateContinentDTO, Continent>();
			CreateMap<AddContinentDTO, Continent>();

            // Country
            CreateMap<Country, GetCountryDTO>();
            CreateMap<UpdateCountryDTO, Country>();
            CreateMap<AddCountryDTO, Country>();

			// City
			CreateMap<City, GetCityDTO>();
			CreateMap<UpdateCityDTO, City>();
			CreateMap<AddCityDTO, City>();

            // Category
			//CreateMap<GetCategoryDTO, Category>();
			CreateMap<AddCategoryDTO, Category>();
			CreateMap<Category, GetCategoryDTO>();
			CreateMap<UpdateCategoryDTO, Category>();

            CreateMap<AddSubcategoryDTO, Subcategory>();
            CreateMap<Subcategory, GetSubcategoryDTO>();
            CreateMap<UpdateSubcategoryDTO, GetSubcategoryDTO>();

            // Store
            //CreateMap<GetStoreDTO, Store>();
            CreateMap<Store, GetStoreDTO>();
            CreateMap<AddStoreDTO, Store>();

            // Product
            CreateMap<AddProductDTO, Product>();
            CreateMap<Product, GetProductDTO>();
            CreateMap<UpdateProductDTO, GetProductDTO>();

            // Cart
            CreateMap<AddCartDTO, Cart>();
            CreateMap<Cart, GetCartDTO>();
        }
    }
}