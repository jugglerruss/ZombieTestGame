using UnityEngine;

public class AimZombieState : State
{
    private Zombie _zombie;
    public override void Init()
    {
        base.Init();
        _zombie = Character as Zombie;
    }
    public override void Run()
    {
        if (_zombie.Target == null) IsFinished = true;
        if (IsFinished)
            return;

        Character.ChangeAnimation(AnimationMain.walk, _zombie.Target.transform.position);
        Character.MoveTo(_zombie.Target.transform.position);
    }
    public override string ToString()
    {
        return "AimTarget";
    }
}
