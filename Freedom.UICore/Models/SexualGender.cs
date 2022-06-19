using CommunityToolkit.Mvvm.ComponentModel;

namespace Freedom.UICore.Models
{
    public class SexualGender : ObservableObject
    {
        private byte _id;
        private string _genderName;

        public byte Id { get => _id; set => SetProperty(ref _id, value); }

        public string GenderName { get => _genderName; set => SetProperty(ref _genderName, value); }
    }
}