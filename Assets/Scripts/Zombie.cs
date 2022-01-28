using System.Collections;
using UnityEngine;

public class Zombie : Character
{
    private int _stepsToChangeDirection, _stepsToCoolDown, _passiveVelocity;
    public int StepsToChangeDirection { get => _stepsToChangeDirection; }
    public Hero Target { get; protected set; }
    public Zombie TargetFollow { get; protected set; }
    public State FollowState;
    private HeroDetectCollider _heroDetectColliderView;
    private HeroDetectCollider _heroDetectColliderAttack;
    public void Initialize(CharacterData characterData, int stepsToChangeDirection, int stepsToCoolDown, int passiveVelocity, bool isDebug)
    {
        _stepsToChangeDirection = stepsToChangeDirection;
        _stepsToCoolDown = stepsToCoolDown;
        _characterData = characterData;
        _passiveVelocity = passiveVelocity;
        _stateIdle = new IdleZombieState();
        _stateAim = new AimZombieState();
        _stateAttack = new AttackZombieState();
        FollowState = new FollowZombieState();
        _heroDetectColliderView = _circleView.GetComponent<HeroDetectCollider>();
        _heroDetectColliderAttack = _circleAttack.GetComponent<HeroDetectCollider>();
        SetState(_stateIdle);
        Init();
        DebugViewSetActive(isDebug);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_stateCurrent == null || IsDead)
            return;
        if (!_stateCurrent.IsFinished)
        {
            _stateCurrent.Run();
        }
        else
        {
            if (Target != null && IsReloaded)
            {
                if (IsEnemyInAttackRange)
                    SetState(_stateAttack);
                else
                    SetState(_stateAim);
            }
            else
            {
                if (TargetFollow != null)
                    SetState(FollowState);
                else
                    SetState(_stateIdle);
            }
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    protected override void DrawAngle()
    {
        base.DrawAngle();
        _heroDetectColliderView.DrawLines(_pointsView, Color.white);
        _heroDetectColliderAttack.DrawLines(_pointsAttack, Color.red);
    }
    public override void DebugViewSetActive(bool isDebug)
    {
        base.DebugViewSetActive(isDebug);
        _heroDetectColliderView.DebugViewSetActive(isDebug);
        _heroDetectColliderAttack.DebugViewSetActive(isDebug);
    }
    public void TakeDamage(int damage, Hero hero)
    {
        _characterData.Health -= damage;
        if (_stateCurrent != _stateAttack && _stateCurrent != _stateAim)
            _stateCurrent.IsFinished = true;
        if (Target == null)
            Target = hero;
        if (_characterData.Health <= 0)
            StartCoroutine(WaitDead());
    }
    public void HeroViewDetect(Hero hero)
    {
        if (PositionInAngle(hero.transform.position, _characterData.ViewAreaAngle) && Target == null)
        {
            Target = hero;
            if (_stateCurrent != _stateAttack && _stateCurrent != _stateAim)
                _stateCurrent.IsFinished = true;
        }
    }
    public void ZombieViewDetect(Zombie zombie)
    {
        if (!PositionInAngle(zombie.transform.position, _characterData.ViewAreaAngle) || TargetFollow != null || zombie == this)
            return;
        if (zombie._stateCurrent is IdleZombieState)
            return;
        if (_stateCurrent == _stateIdle)
        {
            TargetFollow = zombie;
            _stateCurrent.IsFinished = true;
        }

    }
    public void TargetDissapear()
    {
        Target = null;
    }
    public void TargetFollowDissapear()
    {
        TargetFollow = null;
    }
    public void HeroInAttackRangeDetect(Hero hero)
    {
        if (!PositionInAngle(hero.transform.position, _characterData.DamageAreaAngle))
            return;
        IsEnemyInAttackRange = true;
        if (_stateCurrent != _stateAttack)
            _stateCurrent.IsFinished = true;
    }
    public void SlowMoveTo(Vector3 position)
    {
        MoveTo(position, _passiveVelocity);
    }
    private IEnumerator WaitDead()
    {
        IsDead = true;
        ChangeAnimation(AnimationMain.dead);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
