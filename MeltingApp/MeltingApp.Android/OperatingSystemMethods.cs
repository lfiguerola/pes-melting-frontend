using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MeltingApp.Interfaces;

namespace MeltingApp.Droid
{
    class OperatingSystemMethods : IOperatingSystemMethods
    {
         
        public void ShowToast(string text)
        {
            //com esta fora de la main activity hem de buscar el context
            Toast.MakeText(Android.App.Application.Context, text, ToastLength.Long).Show();
        }
    }
}