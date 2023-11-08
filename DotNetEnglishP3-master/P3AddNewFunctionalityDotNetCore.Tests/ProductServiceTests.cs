
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

namespace P3AddNewFunctionalityDotNetCore.Tests

{
    public class ProductServiceTests
    {
        
        private static P3Referential _context;
        static ProductRepository productRepositoryInstance = new ProductRepository(_context);
        static OrderRepository orderRepositoryInstance = new OrderRepository(_context);


      //  CultureInfo.CurrentUICulture = new CultureInfo("en-GB");

        [Fact]
        public void CheckProductModelErrors_ValidProduct()
        {// Arrange
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            
            var productService = new ProductService(
                    cartInstance,  
                    productRepositoryInstance, 
                    orderRepositoryInstance, 
                    localizer  
                );

            var validProduct = new ProductViewModel
            {              
                Name = "Name",
                Price = "100",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };

            // Assert          
            ////Valid Product
            Assert.Empty(productService.CheckProductModelErrors(validProduct));


        }
        [Fact]
        public void CheckProductModelErrors_ValidProductCommaSeparator()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

            var validProduct = new ProductViewModel
            {
                Name = "Name",
                Price = "100,5",
                Stock = "5,0",
                Description = "Valid description",
                Details = "Valid details"
            };

            // Assert          
            ////Valid Product
            Assert.Empty(productService.CheckProductModelErrors(validProduct));


        }
        [Fact]
        public void CheckProductModelErrors_ValidProductDotSeparator()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

            var validProduct = new ProductViewModel
            {
                Name = "Name",
                Price = "100.5",
                Stock = "5.0",
                Description = "Valid description",
                Details = "Valid details"
            };

            // Assert          
            ////Valid Product
            Assert.Empty(productService.CheckProductModelErrors(validProduct));


        }
        [Fact]
        public void CheckProductModelErrors_MissingName()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

            var missingName = new ProductViewModel
            {
                Name = "",
                Price = "1000",
                Stock = "100",
                Description = "Valid description",
                Details = "Valid details"
            };
          
            //// Missing Name
            Assert.Single(productService.CheckProductModelErrors(missingName));
            Assert.Equal("MissingName", productService.CheckProductModelErrors(missingName)[0]);
           
        }

        [Fact]
        public void CheckProductModelErrors_MissingPrice()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

         
            var missingPrice = new ProductViewModel
            {
                Name = "Name",
                Price = "",
                Stock = "100",
                Description = "Valid description",
                Details = "Valid details"
            };
           
            ///// Missing Price
            Assert.Single(productService.CheckProductModelErrors(missingPrice));
            Assert.Equal("MissingPrice", productService.CheckProductModelErrors(missingPrice)[0]);
           
        }

        [Fact]
        public void CheckProductModelErrors_PriceNotANumber()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

          
            var priceNotANumber = new ProductViewModel
            {
                Name = "Name",
                Price = "ZZZ",
                Stock = "100",
                Description = "Valid description",
                Details = "Valid details"
            };
          
          
            ////Price Not a Number
            Assert.Single( productService.CheckProductModelErrors(priceNotANumber));
            Assert.Equal("PriceNotANumber", productService.CheckProductModelErrors(priceNotANumber)[0]);           
        }
        /// <summary>
        /// TODO: ADD Not greater than zero with different separators
        /// </summary>
        [Fact]
        public void CheckProductModelErrors_PriceNotGreaterThanZero()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();
            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

         
            var priceNotGreaterThanZero = new ProductViewModel
            {
                Name = "Name",
                Price = "-1000.5",
                Stock = "100",
                Description = "Valid description",
                Details = "Valid details"
            };

            // Assert          
           
            ////Price Not Greter Than Zero
            Assert.Single(productService.CheckProductModelErrors(priceNotGreaterThanZero));
            Assert.Equal("PriceNotGreaterThanZero", productService.CheckProductModelErrors(priceNotGreaterThanZero)[0]);
        }

        [Fact]
        public void CheckProductModelErrors_MissingQuantity()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );


            var missingQuantity = new ProductViewModel
            {
                Name = "Name",
                Price = "100",
                Stock = "",
                Description = "Valid description",
                Details = "Valid details"
            };

            ///// Missing Price
            Assert.Single(productService.CheckProductModelErrors(missingQuantity));
            Assert.Equal("MissingQuantity", productService.CheckProductModelErrors(missingQuantity)[0]);

        }

        [Fact]
        public void CheckProductModelErrors_QuantityNotAnInteger()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );


            var quantityNotAnInteger = new ProductViewModel
            {
                Name = "Name",
                Price = "100",
                Stock = "1.5",
                Description = "Valid description",
                Details = "Valid details"
            };

            ///// Missing Price
            Assert.Single(productService.CheckProductModelErrors(quantityNotAnInteger));
            Assert.Equal("QuantityNotAnInteger", productService.CheckProductModelErrors(quantityNotAnInteger)[0]);

        }

        [Fact]
        public void CheckProductModelErrors_QuantityNotGreaterThanZero()
        {  // Arrange
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


          
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );


            var quantityNotGreaterThanZero = new ProductViewModel
            {
                Name = "Name",
                Price = "100",
                Stock = "-15",
                Description = "Valid description",
                Details = "Valid details"
            };

            ///// Missing Price
            Assert.Single(productService.CheckProductModelErrors(quantityNotGreaterThanZero));
            Assert.Equal("QuantityNotGreaterThanZero", productService.CheckProductModelErrors(quantityNotGreaterThanZero)[0]);

        }
    }
}
        
 
