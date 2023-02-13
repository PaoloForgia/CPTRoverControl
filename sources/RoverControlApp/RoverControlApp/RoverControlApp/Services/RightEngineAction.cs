using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoverControlApp.Services
{
    /// <summary>
    /// Right Engine implementation of <c>RepeatableAction</c>.
    /// <para />
    /// Used to repeatedly send Bluetooth commands with a small delay.
    /// </summary>
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
                bluetooth.Send(Commands.EngineRight(Speed));
                Thread.Sleep(DELAY);
            }
        }
     
        void OnRightEngineStop()
        {
            // No action required
        }
    }
}
