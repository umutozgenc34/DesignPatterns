namespace DesignPatterns.ChainOfResponsibility.ChainOfResponsibility;

public abstract class Processhandler : IProcessHandler
{
    private IProcessHandler nextProcessHandler;

    public virtual async Task<object> handle(object o)
    {
        if (nextProcessHandler != null)
        {
            return await nextProcessHandler.handle(o);
        }
        return null;
    }

    public IProcessHandler SetNext(IProcessHandler processHandler)
    {
        nextProcessHandler = processHandler;
        return nextProcessHandler;
    }
}