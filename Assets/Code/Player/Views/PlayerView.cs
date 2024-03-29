using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip attackAnimationClip;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private WeaponPrefabs prefab;
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Idling = Animator.StringToHash("idling");
    private static readonly int Attacking = Animator.StringToHash("attacking");
    private Vector3 _velocity;
    private GameObject _weapon;

    public void Initialize(WeaponType weaponType)
    {
       _weapon = Instantiate(prefab.GetPrefabFor(weaponType), weaponSlot);
    }
    public void Move(Vector3 velocity)
    {
        animator.SetTrigger(Running);
        _velocity = velocity;
    }

    private void FixedUpdate()
    {
        if(_velocity == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(_velocity);
        transform.position += _velocity;
    }

    public void Stop()
    {
        animator.ResetTrigger(Running);
        animator.SetTrigger(Idling);
        _velocity = Vector3.zero;
    }

    public IObservable<Unit> Attack(float speed)
    {
        animator.SetTrigger(Attacking);
        animator.speed = speed;
        return Observable.Timer(TimeSpan.FromSeconds(attackAnimationClip.length))
            .Select(_ => Unit.Default).Take(1);
    }
    
    public IObservable<Unit> ApplyDamage(float speed) =>
        Observable.Timer(TimeSpan.FromSeconds(attackAnimationClip.length*speed))
            .Select(_ => Unit.Default).Take(1);

    public void ChangeWeapon(WeaponType weaponType)
    {
        Destroy(_weapon);
        _weapon = Instantiate(prefab.GetPrefabFor(weaponType), weaponSlot);
    }

    public void StopAttack()
    {
        animator.speed = 1;
        animator.ResetTrigger(Attacking);
    }
}