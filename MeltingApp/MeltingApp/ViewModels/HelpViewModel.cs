using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Models;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class HelpViewModel : ViewModelBase
    {
   
       public List<HelpElment> _elementos { get; set; }

       public HelpViewModel()
        {
            _elementos = new List<HelpElment>();
            LoadElements();

        }

        public void LoadElements() {

            _elementos.Add(new HelpElment
            {
                Nombre = "¿Que es el Karma?",
                Descripcion = " Sistema de puntuacion"
            });



            _elementos.Add(new HelpElment
            {
                Nombre = "¿Como encontrar mas información sobre tu universidad?",
                Descripcion = " Si al resgistrarte has introducido el nombre de tu Facultad/Universidad  podrás ver esta información accediendo  a tu perfil"
            });

        }


        public Command NavigateToHelpPageCommand { get; set; }
    }
}