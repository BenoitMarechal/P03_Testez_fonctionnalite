using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        //[Required(ErrorMessage = "ErrorMissingName")]
        [MinLength(2,ErrorMessage = "Must contain at least 2 characters")]
       // [RegularExpression(@"\S", ErrorMessage = "The Name field cannot be empty or contain only whitespace.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public string Stock { get; set; }

        public string Price { get; set; }
    }
}
