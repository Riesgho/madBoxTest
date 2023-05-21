using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private WeaponPrefabs prefab;
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Idling = Animator.StringToHash("idling");
    private static readonly int Attacking = Animator.StringToHash("attacking");
    private Vector3 _velocity;

    public void Initialize(WeaponType weaponType)
    {
        Instantiate(prefab.GetPrefabFor(weaponType), weaponSlot);
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

    public void Attack()
    {
        animator.SetTrigger(Attacking);
    }

    public void StopAttack()
    {
        animator.ResetTrigger(Attacking);
    }
}


[System.Serializable]
public class WeaponPrefab
{
    public GameObject Prefab;
    public WeaponType WeaponType;
}