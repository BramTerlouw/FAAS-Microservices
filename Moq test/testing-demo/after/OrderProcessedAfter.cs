using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing_demo.services;

namespace testing_demo.after
{
    public class OrderProcessedAfter
    {
        private readonly ISMPTSender _smtpSender;

        //Class is now decoupled from the SmtpSender through ctor injection
        //Loosely coupled architecture!
        //Hollywood principle
        public OrderProcessedAfter(ISMPTSender smtpSender)
        {
            _smtpSender = smtpSender;
        }

        public bool Finalize()
        {
            _smtpSender.SendMail("message");

            return true;
        }
    }
}
