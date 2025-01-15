public interface IEvent 
{
    void AddListener(IAction action);   
    void RemoveListener(IAction action);
    void Notify();
}