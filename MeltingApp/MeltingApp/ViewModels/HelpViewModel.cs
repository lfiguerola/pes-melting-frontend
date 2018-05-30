using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MeltingApp.Models;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class HelpViewModel : ViewModelBase
    {

        private HelpElement _element;
        private ObservableCollection<HelpElement> _listElements;
        public Command NavigateToHelpPageCommand { get; set; }
        public HelpElement Element {

            get { return _element; }
            set
            {
                _element = value;
                OnPropertyChanged(nameof(Element));
            }

        }

  
        public ObservableCollection<HelpElement> ListElements {
            get {
              
                return _listElements;
            }
            set {
                Console.WriteLine("ListElments Set");
                _listElements = value;
                OnPropertyChanged(nameof(_listElements));
            }


        }

       public HelpViewModel()
        {
            Console.WriteLine("Help View Model constructora");
            ListElements = new ObservableCollection<HelpElement>();
            LoadElements();

        }

        public void LoadElements() {

            Console.WriteLine("hollaaa Load Elements");
            ListElements.Add(new HelpElement
            {
                Nombre = "¿Que es el Karma?",
                Descripcion = " Sistema de puntuacion"
            });



            ListElements.Add(new HelpElement
            {
                Nombre = "¿Como encontrar mas información sobre tu universidad?",
                Descripcion = " Si al resgistrarte has introducido el nombre de tu Facultad/Universidad  podrás ver esta información accediendo  a tu perfil"
            });

        }


       
    }
}