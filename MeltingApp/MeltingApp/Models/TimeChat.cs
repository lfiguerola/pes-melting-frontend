using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
   public  class TimeChat:EntityBase
    {
        int _since;

        public int since {

            get { return _since; }
            set {
                _since = value;
                OnPropertyChanged(nameof(since));

            }

        }

    }
}
