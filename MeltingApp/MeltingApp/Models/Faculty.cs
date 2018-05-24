namespace MeltingApp.Models
{
    public class Faculty : EntityBase
    {
        private string _name;
        private int _location_id;
        private string _address;
        private float _latitude;
        private float _longitude;
        private string _alias;
        private string _telephone;
        private string _url;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public int location_id
        {
            get { return _location_id; }
            set
            {
                _location_id = value;
                OnPropertyChanged(nameof(location_id));
            }
        }

        public string address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(address));
            }
        }

        public float latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }

        public float longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(longitude));
            }

        }
        public string alias
        {
            get { return _alias; }
            set
            {
                _alias = value;
                OnPropertyChanged(nameof(alias));
            }
        }

        public string telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(telephone));
            }
        }

        public string url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(url));
            }
        }
    }
}
