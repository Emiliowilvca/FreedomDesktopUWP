namespace Freedom.UICore.Models
{
    public class NavigationParameter
    {
        public NavigationParameter()
        {
        }

        public NavigationParameter(int maxItemSelect = 1, string parameterId = "")
        {
            MaxItemSelect = maxItemSelect;
            ParameterId = parameterId;
        }

        public int MaxItemSelect { get; set; }


        public string ParameterId { get; set; } = string.Empty;



    }
}