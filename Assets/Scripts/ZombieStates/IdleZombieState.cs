using UnityEngine;

public class IdleZombieState : State
{
    private Zombie zombie;
    private int steps;
    private Vector3 nextPosition;
    public override void Init()
    {
        base.Init();
        zombie = Character as Zombie; 
        nextPosition = new Vector3(Random.Range(-3, 3), Random.Range(-2, 2));
        Character.ChangeAnimation(AnimationMain.walk);
        steps = 0;
    }
    public override void Run()
    {
        if (IsFinished)
            return;
        zombie.SlowMoveTo(nextPosition);
        steps++;
        if (zombie.StepsToChangeDirection == steps) IsFinished = true;
    }
    public override string ToString()
    {
        return "Idle";
    }
}
