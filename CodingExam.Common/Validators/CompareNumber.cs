using CodingExam.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExam.Common.Validators
{
    [AttributeUsage(AttributeTargets.Field |
                   AttributeTargets.Property,
                   AllowMultiple = false,
                   Inherited = true)]
    public sealed class CompareNumber : ValidationAttribute
    {
        private string _fieldToCompare;
        private CompareNumberEnum _enum;

        public CompareNumber(string fieldToCompare, CompareNumberEnum @enum)
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
                case CompareNumberEnum.Equal:
                    if (Convert.ToDouble(value) == Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnum.GreaterThan:
                    if (Convert.ToDouble(value) > Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnum.LessThan:
                    if (Convert.ToDouble(value) < Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnum.GreaterThanEqual:
                    if (Convert.ToDouble(value) >= Convert.ToDouble(valueToCompare))
                        return null;
                    break;
                case CompareNumberEnum.LessThanEqual:
                    if (Convert.ToDouble(value) <= Convert.ToDouble(valueToCompare))
                        return null;
                    break;
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
