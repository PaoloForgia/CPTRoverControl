using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    public class LeftEngineAction: RepeatableAction
    {
        public int Speed { get; set; }
        public LeftEngineAction() : base()
        {
        }

        public void Start(int speed)
        {
            Speed = speed;
            Start(RunLeftEngine, OnLeftEngineStop);
        }

        void RunLeftEngine(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _bluetooth.Send(Commands.EngineLeft(Speed));
                Thread.Sleep(DELAY);
            }
        }

        void OnLeftEngineStop()
        {
            // No action required
        }
    }
}
