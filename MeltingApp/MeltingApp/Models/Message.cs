using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MeltingApp.Models;

public class Message : EntityBase {
    private string _text;
    private bool _isIncoming;

    public Message()
    {
    }

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
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }
}
