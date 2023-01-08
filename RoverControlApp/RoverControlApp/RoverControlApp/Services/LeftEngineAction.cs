using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    /// <summary>
    /// Left Engine implementation of <c>RepeatableAction</c>.
    /// <para />
    /// Used to repeatedly send Bluetooth commands with a small delay.
    /// </summary>>
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
                bluetooth.Send(Commands.EngineLeft(Speed));
                Thread.Sleep(DELAY);
            }
        }

        void OnLeftEngineStop()
        {
            // No action required
        }
    }
}
