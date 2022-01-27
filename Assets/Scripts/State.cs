using UnityEngine;

public abstract class State
{
    public bool IsFinished;
    [HideInInspector] public Character Character;

    public virtual void Init() {
        IsFinished = false;
    }
    public abstract void Run();
}
