using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig
{
    public class HttpMessage
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string AuthToken { get; set; }
        public HttpMessage() { }
    }
}
