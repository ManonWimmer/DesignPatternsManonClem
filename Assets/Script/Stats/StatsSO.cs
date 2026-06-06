using System.Collections.Generic;
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

    private Dictionary<StatType, float> _baseValues;
    // ----- FIELDS ----- //

    public void Initialize()
    {
        _baseValues = new Dictionary<StatType, float>
        {
            { StatType.AttackDamage,         _attackDamage         },
            { StatType.AttackSpeed,          _attackSpeed          },
            { StatType.AttackCooldown,       _attackCooldown       },
            { StatType.AttackDamageTriggerTime, _attackDamageTriggerTime },
            { StatType.MoveSpeed,            _moveSpeed            },
            { StatType.MaxHealth,            _maxHealth            },
        };
    }

    public float GetBaseValue(StatType stat)
    {
        if (_baseValues == null) 
            Initialize();

        return _baseValues.TryGetValue(stat, out float val) ? val : 0f;
    }
}
