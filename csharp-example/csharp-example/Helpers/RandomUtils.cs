using System;
using Bogus;

namespace csharp_example.Helpers
{
    public static class RandomUtils
    {

        public static string GetRandomString(int n)
        {
            return Guid.NewGuid().ToString("N").Substring(0, n);
        }

        public static string GenerateNumberStringWithLength(int numberLength)
        {
            var random = new Random();
            var number = "";
            int i;
            for (i = 1; i <= numberLength; i++)
            {
                number += random.Next(0, 9).ToString();
            }
            return number;
        }

        //Person pers = new Person();
        //var fn = pers.FirstName;

        public static string  GetRandomNumberStringFromInterval(int i)
        {
            return GetRandomNumberFromInterval(i).ToString();
        }

        public static int GetRandomNumberFromInterval(int i)
        {
            var random = new Random();
            return random.Next(1, i);
        }
    }
}
