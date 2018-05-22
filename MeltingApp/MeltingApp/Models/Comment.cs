using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class Comment : EntityBase
    {
        private string _content;

        public string content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(content));
            }
        }
    }
}
