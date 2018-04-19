using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class ViewModelBase : BindableObject, IDisposable
    {
        public virtual void Dispose()
        {
            GC.Collect();
        }
    }
}