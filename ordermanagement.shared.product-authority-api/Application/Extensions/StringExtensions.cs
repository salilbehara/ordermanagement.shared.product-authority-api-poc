using System;

namespace ordermanagement.shared.product_authority_api.Application.Extensions
{
    public static class StringExtensions
    {
        public static long DecodeKeyToId(this string key)
        {
            long id = 0;
            var base32Chars = "A0D23E1V456Q7SK8N9BYFGHRPCTJWMXZ".ToCharArray();
            var keyArray = key.ToCharArray();
            Array.Reverse(keyArray);

            for (int i = 0; i < keyArray.Length; i++)
            {
                id = id + ((Array.IndexOf(base32Chars, keyArray[i])) * (long)(Math.Pow((double)32, (double)i)));
            }

            return id - 100000000000;
        }
    }
}
