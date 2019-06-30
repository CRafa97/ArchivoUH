using System;
using System.ComponentModel.DataAnnotations;

//Contains GT, GE, EQ, LE, and LT validations for types that implement IComparable interface.
//https://stackoverflow.com/questions/41900485/custom-validation-attributes-comparing-two-properties-in-the-same-model

namespace ArchivoUH.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public GreaterThanAttribute(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");

            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) > 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreaterThanOrEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public GreaterThanOrEqualAttribute(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) >= 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);

        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CustomEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public CustomEqualAttribute(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) == 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LessThanOrEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public LessThanOrEqualAttribute(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) <= 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public LessThanAttribute(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) < 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}