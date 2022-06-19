namespace Freedom.UICore.Interface
{
    public interface IStatusNotifyService
    {
        void ShowSuccess(string message);

        void ShowWarning(string message);

        void ShowError(string message);

        void ShowInformation(string message);
    }
}