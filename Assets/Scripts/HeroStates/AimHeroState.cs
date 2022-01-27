using UnityEngine;

public class AimHeroState : State
{
    private Hero _hero;
    public override void Init()
    {
        base.Init();
        _hero = Character as Hero;
        Character.ChangeAnimation(AnimationMain.walk, _hero.Target.transform.position);
    }
    public override void Run()
    {
        if (_hero.Target == null) IsFinished = true;
        if (IsFinished)
            return;
        Character.MoveTo(_hero.Target.transform.position);
    }
}
