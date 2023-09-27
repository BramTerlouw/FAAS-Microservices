using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing_demo.services
{
    public class SMPTServer : ISMPTSender
    {
        public bool SendMail(string message)
        {
            Console.WriteLine("Sending mail with " + message);
            return true;
        }
    }
}
