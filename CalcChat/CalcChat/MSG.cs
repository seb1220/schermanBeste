using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcChat
{
    public class MSG
    {
        public enum MSGType
        {
            Configruation,
            CalcRequest,
            CalcResponse,
        }

        public MSGType Type { get; set; }

        public string ClientName { get; set; }

        public string Expression { get; set; }

        public string Result { get; set; }

        public DateTime Time { get; set; }
    }
}
