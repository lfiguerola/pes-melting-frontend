using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeltingApp.Annotations;

namespace MeltingApp.Models
{
    public class EntityBase : INotifyPropertyChanged
    {
        private int _id;
        private string _token;
        public event PropertyChangedEventHandler PropertyChanged;

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        //aixo ho he de fer millor
        public string token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnPropertyChanged(nameof(token));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}