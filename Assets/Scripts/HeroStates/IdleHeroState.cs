using UnityEngine;

public class IdleHeroState : State
{
    private Hero _hero;
    private int _timer;
    public override void Init()
    {
        base.Init();
        _hero = Character as Hero;
        Character.ChangeAnimation(AnimationMain.idle);
        _timer = 0;
    }
    public override void Run()
    {
        if (_hero.Target != null || _timer == 20) IsFinished = true;
        if (IsFinished)
            return;
        _timer++;
    }
    public override string ToString()
    {
        return "Idle";
    }
}
