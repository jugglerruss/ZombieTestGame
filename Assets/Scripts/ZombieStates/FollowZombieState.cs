using UnityEngine;

public class FollowZombieState : State
{
    private Zombie _zombie;
    public override void Init()
    {
        base.Init();
        _zombie = Character as Zombie;
    }
    public override void Run()
    {
        if (IsFinished)
            return;
        if (_zombie.TargetFollow != null)
        {
            Character.MoveTo(_zombie.TargetFollow.transform.position);
            Character.ChangeAnimation(AnimationMain.walk, _zombie.TargetFollow.transform.position);
        }
        else
            IsFinished = true;

    }
}
