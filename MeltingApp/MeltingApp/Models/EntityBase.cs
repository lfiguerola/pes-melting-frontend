﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeltingApp.Annotations;
using SQLite;

namespace MeltingApp.Models
{
    public class EntityBase : INotifyPropertyChanged
    {
        private int _id;
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}