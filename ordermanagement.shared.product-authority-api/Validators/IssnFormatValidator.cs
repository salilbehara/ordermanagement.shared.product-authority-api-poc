using FluentValidation.Validators;
using System;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public class IssnFormatValidator : PropertyValidator
    {
        public IssnFormatValidator() : base("{ValidationMessage}")
        {

        }

        private static int CharToInt(char character) =>
            character - '0';

        private static char IntToChar(int number) =>
            (char)('0' + number);

        private static int GenerateWeightedSum(string firstSevenCharacters) =>
            firstSevenCharacters.Select((c, i) => CharToInt(c) * (8 - i))
                                .Sum();

        private static void SetValidationMessage(PropertyValidatorContext context, string message) =>
            context.MessageFormatter.AppendArgument("ValidationMessage", message);

        private static bool AreFirstSevenCharactersValid(string issn) =>
            issn.Take(7)
                .All(c => Char.IsNumber(c));

        private static bool IsCheckCharacterValid(string issn) =>
            Char.IsNumber(issn.Last()) || (issn.Last() == 'X');

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var issn = context.PropertyValue as string;
            var isValidFormat = !String.IsNullOrEmpty(issn)
                                    && (issn.Length == 8)
                                    && AreFirstSevenCharactersValid(issn)
                                    && IsCheckCharacterValid(issn);

            if (!isValidFormat)
            {
                SetValidationMessage(context, "{PropertyName} has an invalid format.");

                return false;
            }

            var firstSevenCharacters = issn.Substring(0, 7);
            var weightedSum = GenerateWeightedSum(firstSevenCharacters);
            var remainder = (weightedSum % 11);
            char checkCharacter;

            if (remainder == 0)
            {
                checkCharacter = '0';
            }
            else
            {
                var checkDigit = 11 - (weightedSum % 11);
                checkCharacter = checkDigit == 10 ? 'X' : IntToChar(checkDigit);
            }

            //Match group value is a string. Regex limts to one character, so we can safely
            //take the first character for a direct character-to-character comparison
            if(checkCharacter != issn[7])
            {
                SetValidationMessage(context, "{PropertyName} has an invalid check digit.");

                return false;
            }

            return true;
        }
    }
}