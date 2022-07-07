namespace BoilerplateService.Infrastructures.Extensions
{
    /// <summary>
    /// Extension methods for String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether this string is insensitive equal with another string.
        /// </summary>
        public static bool EqualsIgnoreCase(this string s, string o)
        {
            return string.Equals(s, o, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Indicates whether this string is not null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool HasValue(this string str)
        {
            return !IsNullOrEmpty(str) && !IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Deserialize an XML string to object.
        /// </summary>
        /// <param name="xml">XML string.</param>
        /// <typeparam name="T">The type being deserialized.</typeparam>
        /// <returns>The object deserialized as the specified type.</returns>
        public static T FromXML<T>(this string xml)
        {
            using (TextReader reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Remove all white spaces from a string.
        /// </summary>
        /// <param name="s">string needs to trim.</param>
        /// <returns>Trimmed string.</returns>
        public static string RemoveWhiteSpaces(this string s)
        {
            return Regex.Replace(s, @"\s+", "");
        }

        /// <summary>
        /// Format a numeric string to decimal format
        /// </summary>
        /// <param name="s">Numeric string needs to format.</param>
        /// <param name="numberOfDecimalPlaces">Number of decimal places.</param>
        /// <returns>Decimal string value.</returns>
        public static string ToDecimalString(this string s, int numberOfDecimalPlaces = 2)
        {
            if (decimal.TryParse(s, out var number))
            {
                number = decimal.Round(number, numberOfDecimalPlaces, MidpointRounding.AwayFromZero);
                return number.ToString();
            }
            else
            {
                return "0.00";
            }
        }
        /// <summary>
        /// Convert a numeric string to decimal number
        /// </summary>
        /// <param name="s">Numeric string needs to convert.</param>
        /// <param name="numberOfDecimalPlaces">Number of decimal places.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ToDecimal(this string s, int numberOfDecimalPlaces = 2)
        {
            if (decimal.TryParse(s, out var number))
            {
                number = decimal.Round(number, numberOfDecimalPlaces, MidpointRounding.AwayFromZero);
                return number;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert a numeric string to int number
        /// </summary>
        /// <param name="s">Numeric string needs to convert.</param>
        /// <returns>Int value.</returns>
        public static int ToInt(this string s)
        {
            if (s.IsNullOrWhiteSpace() || !int.TryParse(s, out var number))
            {
                return 0;
            }
            else
            {
                return number;
            }
        }

        /// <summary>
        /// Mask the source string with the mask char except for the last exposed chars.
        /// </summary>
        /// <param name="s">Original string to mask.</param>
        /// <param name="numberExposed">Number of characters exposed in masked value.</param>
        /// <param name="maskChar">Masking character used in mask</param>
        /// <returns>The masked string.</returns>
        public static string Mask(this string s, int numberExposed, char maskChar = '*')
        {
            if (s.IsNullOrWhiteSpace())
            {
                return s;
            }

            string maskedString;

            if (s.Length >= numberExposed)
            {
                maskedString = s.Substring(s.Length - numberExposed).PadLeft(s.Length, maskChar);
            }
            else
            {
                maskedString = new String(maskChar, numberExposed);
            }

            return maskedString;
        }

        /// <summary>
        /// Mask the source string with the mask char except for the last exposed chars.
        /// </summary>
        /// <param name="s">Original string to mask.</param>
        /// <param name="inputFormat">A standard or custom date and time format string. (Default value "dd MMM yyyy")</param>
        /// <param name="outputFormat">A standard or custom date and time format string. (Default value "dd MMM yyyy")</param>
        /// <returns>The masked string.</returns>
        public static string ToDateTimeString(this string s, string inputFormat = "", string outputFormat = "dd MMM yyyy")
        {
            if (s.IsNullOrWhiteSpace())
            {
                return s;
            }

            DateTime dateTime;

            if (inputFormat.IsNullOrEmpty())
            {
                if (!DateTime.TryParse(s, null, DateTimeStyles.None, out dateTime))
                {
                    return s;
                }
            }
            else
            {
                if (!DateTime.TryParseExact(s, inputFormat, null, DateTimeStyles.None, out dateTime))
                {
                    return s;
                }
            }

            return dateTime.ToString(outputFormat);
        }
    }
}