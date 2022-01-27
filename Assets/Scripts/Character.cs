using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Collider2D _circleView;
    [SerializeField] protected Collider2D _circleAttack;
    [SerializeField] protected Transform _forwardDirection;

    protected Collider2D _bodyCollider;
    protected CharacterData _characterData;
    protected State _stateIdle;
    protected State _stateAim;
    protected State _stateAttack;
    protected State _stateCurrent;
    public int Damage { get => _characterData.Damage; }
    public bool IsReloaded { get; protected set; }
    public bool IsEnemyInAttackRange { get; protected set; }
    public bool IsDead { get; protected set; }
  
    protected void Init()
    {
        var radiousView = (float)_characterData.ViewAreaRadius / 50;
        _circleView.transform.localScale = new Vector3(radiousView, radiousView);
        var radiousAttack = (float)_characterData.DamageAreaRadious / 50;
        _circleAttack.transform.localScale = new Vector3(radiousAttack, radiousAttack);

        _bodyCollider = GetComponent<Collider2D>();
        IsReloaded = true;
    }
    protected virtual void FixedUpdate()
    {
        DrawAngle();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void DrawAngle()
    {
        var viewAngle1 = DirectionFromAngle(Vector2.SignedAngle(Vector2.right, _forwardDirection.localPosition) - _characterData.ViewAreaAngle / 2);
        var viewAngle2 = DirectionFromAngle(Vector2.SignedAngle(Vector2.right, _forwardDirection.localPosition) + _characterData.ViewAreaAngle / 2);
        Debug.DrawLine(transform.position, transform.position + viewAngle1 * (float)_characterData.ViewAreaRadius / 100);
        Debug.DrawLine(transform.position, transform.position + viewAngle2 * (float)_characterData.ViewAreaRadius / 100);
    }

    protected void SetState(State startState)
    {
        _stateCurrent = startState;
        _stateCurrent.Character = this;
        _stateCurrent.Init();
    }
    public void MoveTo(Vector3 position)
    {
        Rotate(position);
        transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * _characterData.Velocity / 50);
    }
    public void MoveTo(Vector3 position, int velocity)
    {
        Rotate(position);
        transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * velocity / 50);
    }

    public void Rotate(Vector3 position)
    {
        _forwardDirection.position = Vector2.MoveTowards(_forwardDirection.position, position, Time.deltaTime * _characterData.TurnVelocity / 50);
    }

    protected bool PositionInAngle(Vector3 pos, int viewAreaAngle)
    {
        var direction = (pos - transform.position).normalized;
        if (Vector3.Angle(_forwardDirection.localPosition, direction) < viewAreaAngle / 2)
            return true;
        return false;
    }
    private Vector3 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
    internal void Reload()
    {
        IsReloaded = false;
        StartCoroutine(WaitReload());
    }
    internal void ChangeAnimation(AnimationMain animationMain, Vector3 position = default)
    {
        if(position == default)
            position = _forwardDirection.localPosition;
        AnimationDirection direction = AnimationDicrionary.GetDirection(position);
        SetAnimation(animationMain, direction);
    }
    private void SetAnimation(AnimationMain animationMain, AnimationDirection direction)
    {
        var animationName = $"{AnimationDicrionary.MainDict[animationMain]}{AnimationDicrionary.DirectionDict[direction]}";
        var info = _animator.GetCurrentAnimatorClipInfo(0);
        if (info[0].clip.name != animationName)
        {
            _animator.Play(animationName);
        }
    }
    public void TargetExitAttackRange()
    {
        IsEnemyInAttackRange = false;
    }
    private IEnumerator WaitReload()
    {
        yield return new WaitForSeconds(_characterData.ReloadTime);
        IsReloaded = true;
    }
}