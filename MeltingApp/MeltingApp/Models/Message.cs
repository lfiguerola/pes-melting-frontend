using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MeltingApp.Helpers;
using MeltingApp.Models;
 class Message : ObservableObject
{ 

    private string _text;
    private bool _isIncoming;
    private string _username;

    public string Text
    {

        get { return _text; }
        set {
            _text = value;
            OnPropertyChanged(nameof(Text));
        }
    }
    public bool IsIncoming
    {
        get { return _isIncoming; }
        set {
            _isIncoming = value;
            OnPropertyChanged(nameof(IsIncoming));
        }

    }

    public string Username {
        get { return _username; }
        set {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }

    }
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }
}
