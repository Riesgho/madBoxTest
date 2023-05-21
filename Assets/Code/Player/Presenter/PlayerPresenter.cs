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
    private readonly IWeapon _weapon;

    public PlayerPresenter(IPlayerView view, IPlayerRepository inMemoryPlayer, IWeapon weapon)
    {
        _view = view;
        _inMemoryPlayer = inMemoryPlayer;
        _weapon = weapon;
    }

    public void Attack()
    {
        _view.Attack();
    }

    public void Initialize()
    {
        _view.Initialize(_weapon.Type);
    }

    public void Move(Vector3 direction)
    {
        if(direction != Vector3.zero)
            _view.Move(direction * (_inMemoryPlayer.Speed + _weapon.SpeedModifier) );
        else
        {
            _view.Stop();
        }
    }

    public void StopAttack()
    {
        _view.StopAttack();
    }
}

public enum WeaponType
{
    LongSword,
    CurvedSword,
    GreatSword
}

public interface IWeapon
{
    float SpeedModifier { get; }
    WeaponType Type { get; }
    float AttackRange { get; }
}

public interface IPlayerView
{
    void Move(Vector3 velocity);
    void Initialize(WeaponType weaponType);
    void Stop();
    void Attack();
    void StopAttack();
}

