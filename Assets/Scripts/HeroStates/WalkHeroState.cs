using UnityEngine;

public class WalkHeroState : State
{
    private Vector3 nextPosition;
    public override void Init()
    {
        base.Init();
        nextPosition = new Vector3(Random.Range(-3, 3), Random.Range(-2, 2));
        Character.ChangeAnimation(AnimationMain.walk, nextPosition);
    }
    public override void Run()
    {
        if (IsFinished)
            return;

        Character.MoveTo(nextPosition);

        if (nextPosition == Character.transform.position)
            IsFinished = true;
    }
    public override string ToString()
    {
        return "Walk";
    }
}
