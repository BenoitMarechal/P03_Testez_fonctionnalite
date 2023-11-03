using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }
        [Required(ErrorMessage = "MissingQuantity")]
        [StockValidation(ErrorMessage = "StockValidation")]
        public string Stock { get; set; }


        [Required(ErrorMessage = "MissingPrice")]
        [PriceValidation(ErrorMessage = "PriceValidation")] 


        public string Price { get; set; }
    }
   
        public class PriceValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (string.IsNullOrWhiteSpace(value as string))
                {
                    return ValidationResult.Success;
                }

                // Validate the format using a regex
                var regex = new Regex(@"^-?[0-9]+([,.][0-9]+)?$");
                if (!regex.IsMatch(value as string))
                {
                    return new ValidationResult("PriceNotANumber");
                }

                // Check if it's greater than or equal to zero
                if (decimal.TryParse(value as string, out decimal numericValue) && numericValue >= 0.01m)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("PriceNotGreaterThanZero");
            }
        }
    public class StockValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return ValidationResult.Success;
            }

            if (int.TryParse(value as string, out int intValue))
            {
                if (intValue >= 0)
                {
                    return ValidationResult.Success; // Valid positive integer
                }
                else
                {
                    return new ValidationResult("QuantityNotGreaterThanZero");
                }
            }

            return new ValidationResult("QuantityNotAnInteger");
        }
    }

}
