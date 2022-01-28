using UnityEngine;

public class SearchAmmoHeroState : State
{
    private Vector3 nextPosition;
    public override void Init()
    {
        base.Init();
        Hero hero = Character as Hero;        
        nextPosition = hero.FindAmmoBox();
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
        return "SearchAmmo";
    }
}
