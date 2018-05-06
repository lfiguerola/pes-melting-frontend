using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeltingApp.Annotations;
using SQLite;
using SQLite.Net.Attributes;

namespace MeltingApp.Models
{
    public class EntityBase : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// local database id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int dbId { get; set; }

        /// <summary>
        /// id from backend
        /// </summary>
        public int id { get; set; }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}