using UnityEngine;

public class AttackZombieState : State
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
        if (Character.IsReloaded)
        {
            if (_zombie.IsEnemyInAttackRange)
            {
                Character.ChangeAnimation(AnimationMain.attack);
                _zombie.Target.TakeDamage(Character.Damage, _zombie);
                Character.Reload();
            }
            else
            {
                Character.MoveTo(_zombie.Target.transform.position);
            }
                
        }
    }
    
}
