namespace MeltingApp.Models
{
    public class University : EntityBase
    {

        private string _name;
        private string _location_id;
        private string _address;
        private string _latitude;
        private string _longitude;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public string location_id
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

        public string latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }

        public string longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(longitude));
            }

        }
    }
}
