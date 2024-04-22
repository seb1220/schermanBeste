﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using MSG = CalcChat.MSG;

namespace Vier_Gewinnt
{
    public interface Receiver
    {
        void ReceiveMessage(MSG m, Transfer t);
        void TransferDisconnected(Transfer t);

        void AddDebugInfo(Transfer t, String m, bool sent);
    }
}
