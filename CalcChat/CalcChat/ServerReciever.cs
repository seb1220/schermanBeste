using CalcChat;
using CalculatorWPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using MSG = CalcChat.MSG;

namespace Vier_Gewinnt
{
    public class ServerReciever : Receiver
    {
        void Receiver.AddDebugInfo(Transfer t, string m, bool sent)
        {

        }

        void Receiver.ReceiveMessage(MSG m, Transfer t)
        {
            if (m.Type == MSG.MSGType.CalcRequest)
            {
                MSG response = new MSG
                {
                    Type = MSG.MSGType.CalcResponse,
                    ClientName = m.ClientName,
                    Expression = m.Expression,
                    Time = DateTime.Now
                };

                try
                {
                    response.Result = CalcResult(m.Expression);
                }
                catch (Exception e)
                {
                    response.Result = e.Message;
                }
                
                foreach (Transfer tr in MainWindow.transferList)
                {
                    tr.Send(response);
                }
            }

        }

        void Receiver.TransferDisconnected(Transfer t)
        {

        }

       string CalcResult(string expression)
       {
            if (expression == string.Empty)
            {
                throw new Exception("Expression is empty");
            }

            List<Token> tokens = Tokenizer.Tokenize(expression);
            Parser parser = new Parser(tokens);
            var exp = parser.Parse();

            List<string> variables = new List<string>();
            foreach (Token t in tokens)
            {
                if (t.type == Token.Type.Variable)
                {
                    variables.Add(t.Text);
                }
            }
            // ask for values of variables
            Dictionary<string, double> values = new Dictionary<string, double>();

            foreach (string s in variables)
            {
                //inputbox should be displayed in the middle of the screen
                string value = Microsoft.VisualBasic.Interaction.InputBox
                    ("Please enter a value for " + s,
                    "Value for " + s, "0", (int)SystemParameters.PrimaryScreenWidth / 2,
                    (int)SystemParameters.PrimaryScreenHeight / 2);
                values.Add(s, double.Parse(value));
            }

            var result = exp.Evaluate(values);
            return result.ToString();
        }
    }
}
