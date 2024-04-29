using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat
{
    internal interface Receiver
    {
        public void Receive(MSG msg, Transfer transfer);

        public void CloseConnection(Transfer transfer);
    }
}
