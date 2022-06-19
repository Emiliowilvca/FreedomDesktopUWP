namespace Freedom.UICore.Models
{
    public class ViewData
    {

        public ViewData(string pageKey, string parameterValue)
        {
            PageKey = pageKey;
            ParameterValue = parameterValue;
        }

        public string PageKey { get; set; }

        public string ParameterValue { get; set; }
    }
}