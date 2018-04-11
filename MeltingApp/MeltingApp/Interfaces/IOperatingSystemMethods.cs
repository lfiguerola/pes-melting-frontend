using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Interfaces
{
    /// <summary>
    /// Interface that exposes the Operating System methods
    /// </summary>
    public interface IOperatingSystemMethods
    {
        /// <summary>
        /// Shows toasts with an specific text
        /// </summary>
        /// <param name="text"></param>
        void ShowToast(string text);
    }
}
