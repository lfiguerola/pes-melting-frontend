using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    class SendChatQuery
    {
        public string body { get; set; }
        public int user_id { get; set; }
        public int utc_timestamp { get; set; }
    }
}
