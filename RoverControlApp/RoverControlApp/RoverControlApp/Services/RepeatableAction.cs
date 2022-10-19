using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoverControlApp.Services
{
    public abstract class RepeatableAction
    {
        protected static readonly int DELAY = 100;
        protected CancellationTokenSource _tokenSource;
        protected Bluetooth _bluetooth;

        public bool IsActive => _tokenSource != null;

        public RepeatableAction()
        {
            _bluetooth = Bluetooth.Instance;
        }

        protected void Start(Action<CancellationToken> action, Action onStop)
        {
            _tokenSource = new CancellationTokenSource();
            _tokenSource.Token.Register(() => onStop());
            Task.Run(() => action(_tokenSource.Token), _tokenSource.Token);
        }

        public void Stop()
        {
            _tokenSource.Cancel();
        }
    }
}
