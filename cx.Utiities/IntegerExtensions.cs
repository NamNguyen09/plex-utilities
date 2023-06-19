using System;
using System.Globalization;

namespace cx.Utiities
{
    public static class IntegerExtentions
    {
        public static bool IsNumericType(this object expression)
        {
            if (expression == null || expression is DateTime)
            {
                return false;
            }
            if (expression is Int16 || expression is Int32 || expression is Int64 || expression is Decimal
                || expression is Single || expression is Double || expression is Boolean)
            {
                return true;
            }

            double parseVal;
            var isParse = double.TryParse(expression.ToString(), out parseVal);

            if (isParse)
                return true;

            isParse = double.TryParse(expression.ToString().Replace(".", ","), out parseVal);

            return isParse;
        }

        public static bool IsNumericType(this object expression, CultureInfo culture)
        {
            if (expression == null || expression is DateTime)
            {
                return false;
            }
            if (expression is Int16 || expression is Int32 || expression is Int64 || expression is Decimal
                || expression is Single || expression is Double || expression is Boolean)
            {
                return true;
            }
            try
            {
                if (expression is string)
                {
                    double.Parse(expression as string, culture);
                }
                else
                {
                    double.Parse(expression.ToString(), culture);
                }
                return true;
            }
            catch // just dismiss errors but return false
            {

            }

            return false;
        }

        public static int GetMin(this int number1, int number2, int number3)
        {
            return Math.Min(number1, Math.Min(number2, number3));
        }
    }
}
