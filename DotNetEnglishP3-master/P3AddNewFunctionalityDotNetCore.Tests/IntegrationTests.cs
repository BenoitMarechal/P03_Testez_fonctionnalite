
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class IntegrationTests
    {   //Arrange
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<ProductService> _localizer;
        [Fact]
        public void SaveNewProduct()
        {
            //Arrange        
            var options = new DbContextOptionsBuilder<P3Referential>()
            .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            P3Referential ctx = new (options, _configuration);

            LanguageService languageService = new();
            Cart cart = new ();
            ProductRepository productRepository = new (ctx);
            OrderRepository orderRepository = new(ctx);
            ProductService productService= new (cart, productRepository, orderRepository, _localizer);
            ProductController productController = new (productService, languageService);
            ProductViewModel productViewModel = new() { Id = 10, Name = "Name", Description = "Description ", Details = "Detail", Stock = "1", Price = "150 "};
            //Act
           Product product =new(){ Id=10, Name = "Name",Description = "Description ", Details = "Detail",Quantity = 1,Price = 150};
           productRepository.SaveProduct(product);
            //Assert
           Assert.NotNull(product);

        }


    }
}
