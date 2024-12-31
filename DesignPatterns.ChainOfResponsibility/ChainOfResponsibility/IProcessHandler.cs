namespace DesignPatterns.ChainOfResponsibility.ChainOfResponsibility;

public interface IProcessHandler
{
    IProcessHandler SetNext(IProcessHandler processHandler);
    Task<object> handle(object o);
}
