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

        private HelpElment _elementos;
        public Command NavigateToHelpPageCommand { get; set; }
        public HelpElment elementos {

            get { return _elementos; }
            set
            {
                _elementos = value;
                OnPropertyChanged(nameof(User));
            }

        }

        public ObservableCollection<HelpElment> ListElments;

       public HelpViewModel()
        {
            ListElments = new ObservableCollection<HelpElment>();
            LoadElements();

        }

        public void LoadElements() {

            Console.WriteLine("hollaaa");
            ListElments.Add(new HelpElment
            {
                Nombre = "¿Que es el Karma?",
                Descripcion = " Sistema de puntuacion"
            });



            ListElments.Add(new HelpElment
            {
                Nombre = "¿Como encontrar mas información sobre tu universidad?",
                Descripcion = " Si al resgistrarte has introducido el nombre de tu Facultad/Universidad  podrás ver esta información accediendo  a tu perfil"
            });

        }


       
    }
}