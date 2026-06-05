using UnityEngine;

[CreateAssetMenu(fileName = "StatsSO", menuName = "Scriptable Objects/StatsSO")]
public class StatsSO : ScriptableObject
{
    // ----- FIELDS ----- //
    [Header("Attack")]
    [SerializeField] private float _attackDamage = 1;
    [SerializeField] private float _attackSpeed = 1;
    [SerializeField] private float _attackCooldown = 3;
    [SerializeField] private float _attackDamageTriggerTime = 1;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3; // to integrate in anim with move ? 

    [Header("Health")]
    [SerializeField] private float _maxHealth = 3;

    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }
    public float AttackDamageTriggerTime { get => _attackDamageTriggerTime; set => _attackDamageTriggerTime = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    // ----- FIELDS ----- //
}
