using CodingExam.WebAPI.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExam.WebAPI.Validators
{
    [AttributeUsage(AttributeTargets.Field |
                   AttributeTargets.Property,
                   AllowMultiple = false,
                   Inherited = true)]
    public sealed class CompareNumber : ValidationAttribute
    {
        private string _fieldToCompare;
        private CompareNumberEnums _enum;

        public CompareNumber(string fieldToCompare, CompareNumberEnums @enum)
        {
            _fieldToCompare = fieldToCompare;
            _enum = @enum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyToCompare = validationContext.ObjectType.GetProperty(_fieldToCompare);
            var valueToCompare = propertyToCompare.GetValue(validationContext.ObjectInstance, null);

            switch (_enum)
            {
                case CompareNumberEnums.Equal:
                    if (Convert.ToDouble(value) == Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnums.GreaterThan:
                    if (Convert.ToDouble(value) > Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnums.LessThan:
                    if (Convert.ToDouble(value) < Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnums.GreaterThanEqual:
                    if (Convert.ToDouble(value) >= Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnums.LessThanEqual:
                    if (Convert.ToDouble(value) <= Convert.ToDouble(valueToCompare))
                        return null;
                    break;
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
