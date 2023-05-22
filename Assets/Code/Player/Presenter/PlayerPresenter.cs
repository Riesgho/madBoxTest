using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class PlayerPresenter
{
    private readonly IPlayerView _view;
    private readonly IPlayerRepository _inMemoryPlayer;
    private IWeapon _weapon;
    private readonly ISubject<Unit> _applyDamage;
    private readonly CompositeDisposable _disposable;
    private bool _canAttackAgain = true;

    public PlayerPresenter(IPlayerView view, IPlayerRepository inMemoryPlayer, IWeapon weapon,
        ISubject<Unit> applyDamage)
    {
        _view = view;
        _inMemoryPlayer = inMemoryPlayer;
        _weapon = weapon;
        _applyDamage = applyDamage;
        _disposable = new CompositeDisposable();
    }

    public void Attack()
    {
        if (_canAttackAgain)
        {
            ShowAttackAnimation();
            _view.ApplyDamage(_weapon.ApplyDamageSpeed)
                .DoOnCompleted(()=> _applyDamage.OnNext(Unit.Default))
                .Subscribe()
                .AddTo(_disposable);
        }
           
    }

    private void ShowAttackAnimation()
    {
        _view.Attack(_weapon.AttackSpeedModifier)
            .DoOnCompleted(() =>
            {
                _canAttackAgain = true;
            })
            .DoOnSubscribe(() => _canAttackAgain = false)
            .Subscribe()
            .AddTo(_disposable);
    }

    public void Initialize()
    {
        _view.Initialize(_weapon.Type);
    }

    public void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
            _view.Move(direction * (_inMemoryPlayer.Speed + _weapon.SpeedModifier));
        else
        {
            _view.Stop();
        }
    }

    public void StopAttack()
    {
        _view.StopAttack();
        _canAttackAgain = true;
    }

    public void ChangeWeapon(IWeapon weapon)
    {
        _weapon = weapon;
        Debug.Log(_weapon.Type);
        _view.ChangeWeapon(_weapon.Type);
    }
}

public interface IPlayerView
{
    void Move(Vector3 velocity);
    void Initialize(WeaponType weaponType);
    void Stop();
    IObservable<Unit> Attack(float speed);
    void StopAttack();
    IObservable<Unit> ApplyDamage(float speed);
    void ChangeWeapon(WeaponType weaponType);
}