namespace AssetsOnMarket.Application.ViewModels
{
    public class PropertyValueViewModel
    {
        public string Property { get; set; }
        public string Value { get; set; }

        public PropertyValueViewModel(string property, string value)
        {
            Property = property;
            Value = value;
        }
    }
}
