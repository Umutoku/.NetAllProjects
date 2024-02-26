namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler handler); // Bir sonraki işlemi belirler.
        object Handle(object request); // İşlemi gerçekleştirir.
    }
}
