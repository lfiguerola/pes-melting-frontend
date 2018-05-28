using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class HelpElement : EntityBase
    {
        private String _nombre;
        private String _description;

       public String Nombre {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }
        public String Descripcion
        {
            get { return _description; }

            set
            {
                _description = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }
    }
}
