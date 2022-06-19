namespace Freedom.Core
{
    //public static class Conversions
    //{
    //    public static decimal ToDecimal(this string value)
    //    {
    //        string input = value.Trim();
    //        decimal result = 0m;

    //        input = input.Replace(" ", "");

    //        if (!string.IsNullOrEmpty(input) == false)
    //        {
    //            return result;
    //        }
    //        // check if input has , and . for thousands separator and decimal place
    //        if (input.Contains(',') && input.Contains('.'))
    //        {
    //            // find the decimal separator, might be , or .
    //            int decimalpos = input.LastIndexOf(',') > input.LastIndexOf('.') ? input.LastIndexOf(',') : input.LastIndexOf('.');
    //            // uses | as a temporary decimal separator
    //            input = input.Substring(0, decimalpos) + "|" + input.Substring(decimalpos + 1);
    //            // formats the output removing the , and . and replacing the temporary | with .
    //            input = input.Replace(".", "").Replace(",", "").Replace("|", ".");
    //        }
    //        // replaces , with .
    //        if (input.Contains(','))
    //        {
    //            input = input.Replace(',', '.');
    //        }
    //        // checks if the input number has thousands separator and no decimal places
    //        if (input.Count(item => item == '.') > 1)
    //        {
    //            input = input.Replace(".", "");
    //        }

    //        // tries to convert input to decimal
    //        if (decimal.TryParse(input, out result) == true)
    //        {
    //            CultureInfo cultureInfo = new CultureInfo("en-US");
    //            result = decimal.Parse(input, NumberStyles.AllowLeadingSign |
    //                                          NumberStyles.AllowDecimalPoint |
    //                                          NumberStyles.AllowThousands, cultureInfo);
    //        }
    //        return result;
    //    }




    //}
}