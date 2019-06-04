namespace Computer_Era_X.Validators
{
    public static class NumberValidator
    {
        /// <summary>
        /// Takes a string, checks whether it is a number (int) and returns a value. If the string is not a number, returns 0.
        /// </summary>
        /// <param name="number">Number in string representation</param>
        /// <returns></returns>
        /// 
        public static int GetIntFromString(string number)
        {
            if (int.TryParse(number, out int result))
            {
                return result;
            }
            else { return 0; }
        }

        /// <summary>
        /// Takes a string, checks whether it is a number (int) and returns a value in a string representation if the string is not a number, returns 0 in string representation.
        /// </summary>
        /// <param name="number">Number in string representation</param>
        /// <returns></returns>
        /// 
        public static string IntFromText(string number)
        {
            return GetIntFromString(number).ToString();
        }

        public static double GetDoubleFromString(string number)
        {
            if (double.TryParse(number, out double result))
            {
                return result;
            }
            else { return 0; }
        }

        public static string DoubleFromText(string number)
        {
            return GetDoubleFromString(number).ToString();
        }
    }
}
