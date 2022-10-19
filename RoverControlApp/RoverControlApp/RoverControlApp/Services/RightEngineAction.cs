using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    public class RightEngineAction: RepeatableAction
    {
        public int Speed { get; set; }
        public RightEngineAction() : base()
        {
        }

        public void Start(int speed)
        {
            Speed = speed;
            Start(RunRightEngine, OnRightEngineStop);
        }

        void RunRightEngine(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _bluetooth.Send(Commands.EngineRight(Speed));
                Thread.Sleep(DELAY);
            }
        }
     
        void OnRightEngineStop()
        {
            // No action required
        }
    }
}
