using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

                var regex = new Regex(@"^-?[0-9]+([,.][0-9]+)?$");
                if (!regex.IsMatch(value as string))
                {
                    return new ValidationResult("PriceNotANumber");
                }
              
                if (double.TryParse(value as string, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue)  && numericValue >= 0.01)
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

            var regex = new Regex(@"^-?[0-9]+([,.][0]+)?$");
            if (!regex.IsMatch(value as string))
            {
                return new ValidationResult("QuantityNotAnInteger");
            }

            if (double.TryParse(value as string, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue) && numericValue >= 0.01)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("QuantityNotGreaterThanZero");

        }
    }

}
