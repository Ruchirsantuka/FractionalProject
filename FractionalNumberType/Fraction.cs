using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionalNumberType
{
    /// <summary>
    /// A fractional number defined using a whole number, a numerator and a denominator
    /// The denominator is always converted into a positive number by multiplying all numbers by -1 if denominator is negative
    /// The absolute value of numerator is always less than denominator.
    /// </summary>
    public class Fraction
    {
        public int WholeNumber { get; }
        public int Numerator { get; }
        public int Denomenator { get; }
        public Fraction(int numerator, int denomenator)
        {
            MathUtils.Reduce(ref numerator, ref denomenator);
            if (denomenator < 0)
            {
                Numerator = -numerator;
                Denomenator = -denomenator;
            }
            else
            {
                Numerator = numerator;
                Denomenator = denomenator;
            }
            if (Numerator == 0)
            {
                Denomenator = 1;
            }
            else if (Math.Abs(Numerator) >= Denomenator)
            {
                var extraWholeNum = Numerator / Denomenator;
                Numerator -= extraWholeNum * Denomenator;
                WholeNumber += extraWholeNum;
            }
        }
        public Fraction(int wholeNumber, int numerator, int denomenator)
        {
            MathUtils.Reduce(ref numerator, ref denomenator);
            WholeNumber = wholeNumber;
            if (denomenator < 0)
            {
                Numerator = -numerator;
                Denomenator = -denomenator;
            }
            else
            {
                Numerator = numerator;
                Denomenator = denomenator;
            }
            if (Numerator == 0)
            {
                Denomenator = 1;
                WholeNumber += wholeNumber;
            }
            else if (Math.Abs(Numerator) >= Denomenator)
            {
                var extraWholeNum = Numerator / Denomenator;
                Numerator -= extraWholeNum * Denomenator;
                WholeNumber += extraWholeNum;
            }
            if (WholeNumber < 0 && Numerator > 0)
            {
                WholeNumber++;
                Numerator -= Denomenator;
            }
            else if (WholeNumber > 0 && Numerator < 0)
            {
                WholeNumber--;
                Numerator += Denomenator;
            }
        }

        /// <summary>
        /// Returns an equivalent Double
        /// </summary>
        /// <returns></returns>
        public double ToDouble()
        {
            return WholeNumber + (double)Numerator / Denomenator;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            var other = obj as Fraction;
            if (other == null) return false;
            return this == other;
        }

        public override int GetHashCode()
        {
            return $"{WholeNumber}_{Numerator}_{Denomenator}".GetHashCode();
        }

        public override string ToString()
        {
            return $"{WholeNumber} {Numerator}/{Denomenator}";
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            var lcmOfDenominators = MathUtils.GetLCM(a.Denomenator, b.Denomenator);
            var newNumerator = a.Numerator * (lcmOfDenominators / a.Denomenator) + b.Numerator * (lcmOfDenominators / b.Denomenator);
            return new Fraction(a.WholeNumber + b.WholeNumber, newNumerator, lcmOfDenominators);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            var lcmOfDenominators = MathUtils.GetLCM(a.Denomenator, b.Denomenator);
            var newNumerator = a.Numerator * (lcmOfDenominators / a.Denomenator) - b.Numerator * (lcmOfDenominators / b.Denomenator);
            return new Fraction(a.WholeNumber - b.WholeNumber, newNumerator, lcmOfDenominators);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            var numA = a.Numerator;
            var numB = b.Numerator;
            var denA = a.Denomenator;
            var denB = b.Denomenator;
            MathUtils.Reduce(ref numA, ref denB);
            MathUtils.Reduce(ref denA, ref numB);

            return new Fraction(a.WholeNumber * b.WholeNumber, numA * numB, denA * denB)
                + new Fraction(a.WholeNumber * b.Numerator, b.Denomenator)
                + new Fraction(b.WholeNumber * a.Numerator, a.Denomenator);
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            var numA = a.Numerator;
            var numB = b.Numerator;
            var denA = a.Denomenator;
            var denB = b.Denomenator;
            MathUtils.Reduce(ref numA, ref numB);
            MathUtils.Reduce(ref denA, ref denB);

            return new Fraction(numA * denB, denA * numB);
        }

        public static Fraction operator +(Fraction a, int b)
        {
            return new Fraction(a.WholeNumber + b, a.Numerator, a.Denomenator);
        }

        public static Fraction operator -(Fraction a, int b)
        {
            return new Fraction(a.WholeNumber - b, a.Numerator, a.Denomenator);
        }

        public static Fraction operator *(Fraction a, int b)
        {
            return new Fraction(a.WholeNumber * b, a.Numerator * b, a.Denomenator);
        }
        public static Fraction operator /(Fraction a, int b)
        {
            return new Fraction(a.WholeNumber / b, (a.WholeNumber % b) * a.Denomenator + a.Numerator, a.Denomenator * b);
        }
        public static Fraction operator +(int b, Fraction a)
        {
            return new Fraction(a.WholeNumber + b, a.Numerator, a.Denomenator);
        }

        public static Fraction operator -(int b, Fraction a)
        {
            return new Fraction(b - a.WholeNumber, -a.Numerator, a.Denomenator);
        }

        public static Fraction operator *(int b, Fraction a)
        {
            return new Fraction(a.WholeNumber * b, a.Numerator * b, a.Denomenator);
        }
        public static Fraction operator /(int b, Fraction a)
        {
            return new Fraction(b * a.Denomenator, a.WholeNumber * a.Denomenator + a.Numerator);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            if (a.WholeNumber != b.WholeNumber)
                return a.WholeNumber > b.WholeNumber;
            return a.Numerator * b.Denomenator > b.Numerator * a.Denomenator;
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            if (a.WholeNumber != b.WholeNumber)
                return a.WholeNumber < b.WholeNumber;
            return a.Numerator * b.Denomenator < b.Numerator * a.Denomenator;
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.WholeNumber == b.WholeNumber && a.Numerator == b.Numerator && a.Denomenator == b.Denomenator;
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return a.WholeNumber != b.WholeNumber || a.Numerator != b.Numerator || a.Denomenator != b.Denomenator;
        }

        public static bool operator >=(Fraction a, Fraction b)
        {
            return a == b || a > b;
        }
        public static bool operator <=(Fraction a, Fraction b)
        {
            return a == b || a < b;
        }
    }
}
