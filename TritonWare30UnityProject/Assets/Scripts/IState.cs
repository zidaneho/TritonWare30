public interface IState
{
    public void OnAwake();
    public void OnEnter();
    public void OnUpdate();
    public void OnFixedUpdate();
    public void OnExit();
}

public interface ITransition {
    IState To { get; }
    IPredicate Condition { get; }
}
public interface IPredicate {
    bool Evaluate();
}
public class Transition : ITransition {
    public IState To { get; }
    public IPredicate Condition { get; }

    public Transition(IState to, IPredicate condition) {
        To = to;
        Condition = condition;
    }
}
public class BaseMonsterState : IState
{
    
    public void OnAwake()
    {
        
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}