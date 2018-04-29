using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MeltingApp.Behaviors
{
    class FieldsLowerCaseBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += bindable_TextChanged;
        }

        void bindable_TextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            entry.Text = entry.Text.ToLower();
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= this.bindable_TextChanged;
        }
    }
}
