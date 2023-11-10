
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Data;
using Moq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Tests

{
    public class ProductServiceTests
    {
        
        private static P3Referential _context;
        static ProductRepository productRepositoryInstance = new ProductRepository(_context);
        static OrderRepository orderRepositoryInstance = new OrderRepository(_context);        

        static List<string> validityResult(ProductViewModel product)
        {
            var mockCart = Mock.Of<ICart>();
            var mockProductRepository = Mock.Of<IProductRepository>();
            var mockOrderRepository = Mock.Of<IOrderRepository>();
            var mockLocalizer = Mock.Of<IStringLocalizer<ProductService>>();

            var productService = new ProductService(
                mockCart,
                mockProductRepository,
                mockOrderRepository,
                mockLocalizer
            );
          return  productService.CheckProductModelErrors(product);
        }


        //  CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            

        [Fact]
        public void CheckProductModelErrors_ValidProduct()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = 100.5,
                Stock = 5,
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
        }
        
        [Fact]
        public void CheckProductModelErrors_MissingName()
        {
            var product = new ProductViewModel
            {
                Name = "",
                Price = 100.5,
                Stock = 5,
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Equal("Please enter a name", validityResult(product)[0]);          
           
        }

    
        [Fact]
        public void CheckProductModelErrors_PriceNotGreaterThanZero()
        {
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = -100.5,
                Stock = 5,
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Equal("The price must be greater than zero", validityResult(product)[0]);
        }
        [Fact]
        public void CheckProductModelErrors_QuantityNotGreaterThanZero()
        {  // Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = 100.5,
                Stock = -1,
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Equal("The stock must be greater than zero", validityResult(product)[0]);

        }
        [Fact]
        public void CheckProductModelErrors_MissingNameAndNegativePrice()
        {  // Arrange
            var product = new ProductViewModel
            {
                Name = "",
                Price = -100.5,
                Stock = 1,
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Equal(2,validityResult(product).Count);
            Assert.Contains("Please enter a name", validityResult(product));
            Assert.Contains("The price must be greater than zero", validityResult(product));

        }
        [Fact]
        public void CheckProductModelErrors_AllErrorMessagesInFrench()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            var product = new ProductViewModel
            {
                Name = "",
                Price = -100.5,
                Stock = -5,
                Description = "",
                Details = ""
            };
            //Assert
            Assert.Equal(3, validityResult(product).Count);
            Assert.Contains("Veuillez saisir un nom", validityResult(product));
            Assert.Contains("Le stock doit être supérieur à zéro", validityResult(product));
            Assert.Contains("Le prix doit être supérieur à zéro", validityResult(product));
        }
        //[Fact]
        //public void CheckProductModelErrors_PriceNotGreaterThanZero_DotSeparator()
        //{
        //    var product = new ProductViewModel
        //    {
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("PriceNotGreaterThanZero", validityResult(product)[0]);
        //}
        //[Fact]
        //public void CheckProductModelErrors_PriceNotGreaterThanZero_CommaSeparator()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("PriceNotGreaterThanZero", validityResult(product)[0]);
        //}
        //[Fact]
        //public void CheckProductModelErrors_MissingQuantity()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("MissingQuantity", validityResult(product)[0]);
        //}
        //[Fact]
        //public void CheckProductModelErrors_QuantityNotAnInteger()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock =0,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("The value entered for the stock must be a integer", validityResult(product)[0]);
        //}
        //[Fact]
        //public void CheckProductModelErrors_ValidProductCommaSeparator()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Empty(validityResult(product));
        //}
        //[Fact]
        //public void CheckProductModelErrors_ValidProductDotSeparator()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Empty(validityResult(product));
        //}
        //[Fact]
        //public void CheckProductModelErrors_MissingPrice()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("MissingPrice", validityResult(product)[0]);
        //}
        //[Fact]
        //public void CheckProductModelErrors_PriceNotANumber()
        //{
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Single(validityResult(product));
        //    Assert.Equal("PriceNotANumber", validityResult(product)[0]);                  
        // }

        //[Fact]
        //public void CheckProductModelErrors_QuantityNegativeDoubleDotSeparator()
        //{  // Arrange
        //    var product = new ProductViewModel
        //    {
        //        Name = "Name",
        //        Price = 100.5,
        //        Stock = 5,
        //        Description = "Valid description",
        //        Details = "Valid details"
        //    };
        //    //Assert
        //    Assert.Equal(2, validityResult(product).Count);
        //    Assert.Equal("The value entered for the stock must be a integer", validityResult(product)[0]);
        //    Assert.Equal("The stock must be greater than zero", validityResult(product)[1]);
        //}
    }
}
        
 
