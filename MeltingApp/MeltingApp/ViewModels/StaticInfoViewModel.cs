using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.ViewModels
{
    public class StaticInfoViewModel : ViewModelBase
    {
        private StaticInfo _staticInfo;
        private string _responseMessage;

        public StaticInfo StaticInfo
        {
            get { return _staticInfo; }
            set
            {
                _staticInfo = value;
                OnPropertyChanged(nameof(StaticInfo));
            }
        }
        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
            }
        }

        public StaticInfoViewModel()
        {
            StaticInfo = new StaticInfo();
        }
    }
}
