using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoverControlApp.Services
{
    abstract class RepeatableAction
    {
        protected static readonly int DELAY = 100;
        protected CancellationTokenSource tokenSource;
        protected Bluetooth bluetooth;

        public bool IsActive => tokenSource != null;

        public RepeatableAction()
        {
            bluetooth = Bluetooth.Instance;
        }

        protected void Start(Action<CancellationToken> action, Action onStop)
        {
            tokenSource = new CancellationTokenSource();
            tokenSource.Token.Register(() => onStop());
            Task.Run(() => action(tokenSource.Token), tokenSource.Token);
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
    }
}
