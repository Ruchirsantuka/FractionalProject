namespace FractionalNumberType
{
    public class MathUtils
    {
        public static int GetLCM(int a, int b)
        {
            var gcd = GetGCD(a, b);
            return Math.Abs( a / gcd * b);
        }

        public static int GetGCD(int a, int b)
        {
            return GetGCDUnsigned(Math.Abs(a), Math.Abs(b));
        }

        public static int GetGCDUnsigned(int a, int b)
        {
            // Everything divides 0
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            // base case
            if (a == b)
                return a;

            // a is greater
            if (a > b)
                return GetGCDUnsigned(a % b, b);

            return GetGCDUnsigned(a, b % a);
        }

        public static void Reduce(ref int a, ref int b)
        {
            var gcd = GetGCD(a, b);
            a /= gcd;
            b /= gcd;
        }
    }
}