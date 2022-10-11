using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoverControlApp.Services
{
    public abstract class RepeatableAction
    {
        protected CancellationTokenSource _tokenSource;

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
