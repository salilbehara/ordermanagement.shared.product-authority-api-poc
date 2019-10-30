using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ordermanagement.shared.product_authority_api.test
{
    public static class PropertyValidatorExtentions
    {
        public static IEnumerable<ValidationFailure> GetValidationFailures<T, TProperty>(this PropertyValidator validator, object value, Expression<Func<T, TProperty>> propertyExpression, string fieldName = "test field")
        {
            var selector = ValidatorOptions.ValidatorSelectors.DefaultValidatorSelectorFactory();
            var validationContext = new ValidationContext(value, new PropertyChain(), selector);
            var propertyValidatorContext = new PropertyValidatorContext(validationContext, PropertyRule.Create(propertyExpression), fieldName);

            return validator.Validate(propertyValidatorContext);
        }

        public static bool IsValid<T>(this PropertyValidator validator, T value, string fieldName = "test field") =>
            !GetValidationFailures<T, T>(validator, value, v => v, fieldName).Any();

        public static bool IsValid<T, TProperty>(this PropertyValidator validator, T value, Expression<Func<T, TProperty>> propertyExpression, string fieldName = "test field") =>
            !GetValidationFailures(validator, value, propertyExpression, fieldName).Any();
    }
}
