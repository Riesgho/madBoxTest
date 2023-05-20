using UnityEngine;

[CreateAssetMenu(menuName = "Player/Joystick", fileName = "Joystick Configuration")]
public class PlayerMovementConfig: ScriptableObject
{
    [SerializeField] private float speed;
    public float Speed => speed;
}