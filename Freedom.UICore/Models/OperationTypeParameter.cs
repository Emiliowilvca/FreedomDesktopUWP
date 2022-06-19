namespace Freedom.UICore.Models
{
    public class OperationTypeParameter
    {
        public OperationTypeParameter(int maxItemSelect, int[] subTypes, string token)
        {
            MaxItemSelect = maxItemSelect;
            SubTypes = subTypes;
            Token = token;
        }

        public string Token { get; set; } = string.Empty;

        public int MaxItemSelect { get; set; }

        public int[] SubTypes { get; set; }
    }
}