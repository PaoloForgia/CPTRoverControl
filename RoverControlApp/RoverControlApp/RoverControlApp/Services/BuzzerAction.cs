using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    public class BuzzerAction: RepeatableAction
    {
        public void Start()
        {
            base.Start(RunBuzzer, OnBuzzerStop);
        }

        void RunBuzzer(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("B1\n");
                Thread.Sleep(1000);
            }
        }
        void OnBuzzerStop()
        {
            Console.WriteLine("B0\n");
        }
    }
}
