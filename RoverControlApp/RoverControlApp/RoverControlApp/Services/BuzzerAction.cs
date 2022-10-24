using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    public class BuzzerAction: RepeatableAction
    {
        public BuzzerAction() : base()
        {
        }

        public void Start()
        {
            Start(RunBuzzer, OnBuzzerStop);
        }

        void RunBuzzer(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                bluetooth.Send(Commands.Buzzer(true));
                Thread.Sleep(DELAY);
            }
        }
        void OnBuzzerStop()
        {
            bluetooth.Send(Commands.Buzzer(false));
        }
    }
}
