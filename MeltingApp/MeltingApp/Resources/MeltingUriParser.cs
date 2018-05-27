using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Resources
{
    public class MeltingUriParser
    {
        readonly Dictionary<string, string> _parseRules = new Dictionary<string, string>();

        public void AddParseRule(string uriParameterName, string uriParameterValue)
        {
            if (_parseRules.ContainsKey(uriParameterName)) return;

            _parseRules.Add(uriParameterName, uriParameterValue);
        }

        public string ParseUri(string uri)
        {
            foreach (var key in _parseRules.Keys)
            {
                uri = uri.Replace(key, _parseRules[key]);
            }
            return uri;
        }
    }
}
