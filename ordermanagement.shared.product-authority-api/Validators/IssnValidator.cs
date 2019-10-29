using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public static class IssnValidator
    {
        //The format is NNNN-NNNC where N is an integer 0-9 and C is a check digit which may be 0-9 or X.
        //Validation doc states C must be an upper case X, so that is all the regex allows.
        private static readonly Regex _issnRegex = new Regex(@"^(\d{4})-?(\d{3})([\dX])$", RegexOptions.Compiled);

        private static int CharToInt(char character) =>
            character - '0';

        private static char IntToChar(int number) =>
            (char)('0' + number);

        private static int GenerateWeightedSum(string firstSevenCharacters) =>
            firstSevenCharacters.Select((c, i) => CharToInt(c) * (8 - i))
                                .Sum();

        private static bool IsIssnValid<T>(T model, string value)
        {
            var match = _issnRegex.Match(value as string);

            if (!match.Success)
            {
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
            return checkCharacter == match.Groups[3].Value[0];
        }

        public static IRuleBuilderOptions<T, string> IsValidIssn<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsIssnValid)
                              .WithMessage("{PropertyName} has invalid format or check character.");
        }
    }
}