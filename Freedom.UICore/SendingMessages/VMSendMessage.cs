using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;

namespace Freedom.UICore.SendingMessages
{
    public class VMSendMessage<T> : ValueChangedMessage<List<T>> where T: class
    {
        public VMSendMessage(List<T> ts) : base(ts)
        {

            
        }
    }
}