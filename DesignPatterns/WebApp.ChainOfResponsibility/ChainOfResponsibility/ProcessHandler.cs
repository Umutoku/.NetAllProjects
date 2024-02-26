namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler _nextHandler;

        public IProcessHandler SetNext(IProcessHandler handler) // Bir sonraki işlemi belirler.
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request) // İşlemi gerçekleştirir.
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
