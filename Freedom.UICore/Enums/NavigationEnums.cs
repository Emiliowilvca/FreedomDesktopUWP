using System.ComponentModel;

namespace Freedom.UICore
{
    public enum contentControlType
    {
        [Description("MainContentControl")]
        MainContent,

        [Description("ModalContentControl")]
        ModalContent,

        [Description("SideBarHeaderContentControl")]
        SideBarHeaderContentControl,

        [Description("SideBarDetailContentControl")]
        SideBarDetailContentControl,

        [Description("TopBarContentControl")]
        TopBarContentControl,

        [Description("StatusBarContentControl")]
        StatusBarContentControl
    }
}