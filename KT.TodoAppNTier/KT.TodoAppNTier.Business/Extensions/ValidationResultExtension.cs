using FluentValidation.Results;
using KT.TodoAppNTier.Common.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public  static List<CustomValidationErrors> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationErrors> customValidationErrors = new List<CustomValidationErrors>();
            foreach (var item in validationResult.Errors)
            {
                customValidationErrors.Add(new CustomValidationErrors()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName
                });
            }
            return customValidationErrors;
        }
    }
}
