using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.Models
{
    class MyDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        public MyDataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingMessageCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingMessageCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null) return null;
            return messageVm.IsIncoming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }
    }
}
