using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EtcdNet
{
    internal class TaskSupervisor : IDisposable
    {
        private readonly ConcurrentBag<Task> _tasks;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _token;
        private bool _isLive;

        public TaskSupervisor()
        {
            _tasks = new ConcurrentBag<Task>();
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _isLive = true;
        }

        public void Supervise(Action action, TimeSpan pollingInterval)
        {                         
            var th = Task.Factory.StartNew(() =>
            {
                while (_isLive)
                {
                    _token.ThrowIfCancellationRequested();
                    action();
                    Thread.Sleep(pollingInterval);
                }
            }, _token);
            _tasks.Add(th);
        }

        public void Dispose()
        {
            _isLive = false;
            _tokenSource.Cancel();

            Task.WaitAll(_tasks.ToArray(), 5000);
        }
    }
}