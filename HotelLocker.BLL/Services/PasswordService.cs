using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.BLL.Services
{
    public static class PasswordService
    {
        public static string GeneratePassword(int length)
        {
            List<int> forbiddenIndexes = new List<int>();
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            string upperCases = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            string lowerCases = "abcdefghijklmnopqrstuvwxyz";
            string num = "0123456789";
            string symbols = "!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            int index = random.Next(0, length);
            while(forbiddenIndexes.Contains(index))
                 index = random.Next(0, length);

            chars[index] = upperCases[random.Next(0, upperCases.Length)];

            index = random.Next(0, length);
            while (forbiddenIndexes.Contains(index))
                index = random.Next(0, length);

            chars[index] = lowerCases[random.Next(0, lowerCases.Length)];

            index = random.Next(0, length);
            while (forbiddenIndexes.Contains(index))
                index = random.Next(0, length);

            chars[index] = num[random.Next(0, num.Length)];

            index = random.Next(0, length);
            while (forbiddenIndexes.Contains(index))
                index = random.Next(0, length);

            chars[index] = symbols[random.Next(0, symbols.Length)];

            return new string(chars);
        }
    }
}
