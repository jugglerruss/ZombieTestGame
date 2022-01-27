using UnityEngine;

public class AttackHeroState : State
{
    private Hero _hero;
    public override void Init()
    {
        base.Init();
        _hero = Character as Hero;
    }
    public override void Run()
    {
        if (_hero.Target == null) IsFinished = true;
        if (IsFinished)
            return;

        if (Character.IsReloaded && !_hero.Target.IsDead)
        {
            if (_hero.IsEnemyInAttackRange)
            {
                Character.ChangeAnimation(AnimationMain.attack);
                _hero.Target.TakeDamage(Character.Damage, _hero);
                _hero.Shot();
                Character.Reload();
            }
            else
            {
                Character.MoveTo(_hero.Target.transform.position);
            }
        }

    }
    
}
