using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    private int _ammo;
    private State _stateSearchAmmo;
    private State _stateWalk; 
    public Zombie Target { get; protected set; }
    public Action<int> OnHPChange;
    public Action<int> OnAmmoChange;
    public Action OnDead;
    public void Initialize(CharacterData characterData, int ammo)
    {
        _characterData = characterData;
        _ammo = ammo;
        _stateIdle = new IdleHeroState();
        _stateAim = new AimHeroState();
        _stateAttack = new AttackHeroState();
        _stateSearchAmmo = new SearchAmmoHeroState();
        _stateWalk = new WalkHeroState();
        _stateCurrent = _stateIdle;
        SetState(_stateWalk);
        Init();
        OnAmmoChange?.Invoke(_ammo);
        OnHPChange?.Invoke(_characterData.Health);
    }
    private void OnDestroy()
    {
        OnHPChange = null;
        OnAmmoChange = null;
        OnDead = null;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!_stateCurrent.IsFinished)
        {
            _stateCurrent.Run();
        }
        else
        {
            if (_ammo == 0)
                SetState(_stateSearchAmmo);
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
                    if (_stateCurrent == _stateIdle)
                        SetState(_stateWalk);
                    else
                        SetState(_stateIdle);
                }
            }
        }
    }
    public void TakeDamage(int damage, Zombie zombie)
    {
        _characterData.Health -= damage;
        OnHPChange?.Invoke(_characterData.Health);
        if (_stateCurrent != _stateAttack && _stateCurrent != _stateAim)
            _stateCurrent.IsFinished = true;
        if (Target == null)
            Target = zombie;
        if (_characterData.Health <= 0)
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
    public Vector3 FindAmmoBox()
    {
        var ammoBox = FindObjectOfType<AmmoBox>();
        if (ammoBox != null)
            return ammoBox.transform.position;
        return Vector3.zero;
    }
    public void TakeAmmoBox(int ammo)
    {
        _ammo += ammo;
        OnAmmoChange?.Invoke(_ammo);
    }
    public void Shot()
    {
        _ammo --;
        OnAmmoChange?.Invoke(_ammo);
    }
    public void ZombieViewDetect(Zombie zombie)
    {
        if(PositionInAngle(zombie.transform.position, _characterData.ViewAreaAngle) && Target==null)
        {
            if (_ammo > 0 && !zombie.IsDead)
            {
                Target = zombie;
                if (_stateCurrent != _stateAttack && _stateCurrent != _stateAim)
                    _stateCurrent.IsFinished = true;
            }
        }
    }
    public void TargetDissapear()
    {
        Target = null;
    }
    public void ZombieInAttackRangeDetect(Zombie zombie)
    {
        if ( !PositionInAngle(zombie.transform.position, _characterData.DamageAreaAngle) )
            return;
        IsEnemyInAttackRange = true;
        if (_stateCurrent != _stateAttack)
            _stateCurrent.IsFinished = true;
    }
}
