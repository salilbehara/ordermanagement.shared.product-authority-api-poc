using FluentValidation.Validators;
using System.Linq;
using System.Text.RegularExpressions;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public class IssnFormatValidator : PropertyValidator
    {
        //The format is NNNN-NNNC where N is an integer 0-9 and C is a check digit which may be 0-9 or X.
        //Validation doc states C must be an upper case X, so that is all the regex allows.
        private static readonly Regex _issnRegex = new Regex(@"^(\d{4})-?(\d{3})([\dX])$", RegexOptions.Compiled);

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

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var match = _issnRegex.Match(context.PropertyValue as string ?? "");

            if (!match.Success)
            {
                SetValidationMessage(context, "{PropertyName} has an invalid format.");

                return false;
            }

            /*
             * Match group 1: First four characters
             * Match group 2: Next three characters
             * Match group 3: Check character
             */

            var firstSevenCharacters = match.Groups[1].Value + match.Groups[2].Value;
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
            if(checkCharacter != match.Groups[3].Value[0])
            {
                SetValidationMessage(context, "{PropertyName} has an invalid check digit.");

                return false;
            }

            return true;
        }
    }
}