using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    class TimeChatQuery: EntityBase
    {
       private int _since;

        public int  since
        {
            get { return _since; }
            set
            {
                _since = value;
                OnPropertyChanged(nameof(since));
            }
        }
    }
}
