using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlterableStatsController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private StatsSO _baseStats;

    private Dictionary<StatType, List<StatModifier>> _dictStatModifiers = new();
    // ----- FIELDS ----- //

    public float GetStat(StatType stat)
    {
        float baseVal = GetBaseStat(stat);
        if (!_dictStatModifiers.TryGetValue(stat, out var list)) 
            return baseVal;

        float flat = list.Where(m => m.Operator == ModifierOperator.Add).Sum(m => m.Value);
        float percentAdd = list.Where(m => m.Operator == ModifierOperator.PercentAdd).Sum(m => m.Value);
        float percentMult = list.Where(m => m.Operator == ModifierOperator.PercentMult).Aggregate(1f, (acc, m) => acc * (1 + m.Value));

        return (baseVal + flat) * (1 + percentAdd) * percentMult;
    }

    public void AddModifier(StatModifier mod)
    {
        if (!_dictStatModifiers.ContainsKey(mod.Stat)) _dictStatModifiers[mod.Stat] = new List<StatModifier>();
        _dictStatModifiers[mod.Stat].Add(mod);
    }

    public void RemoveModifier(StatModifier mod)
    {
        _dictStatModifiers[mod.Stat]?.Remove(mod);
    }

    public void ClearStat(StatType stat)
    {
        _dictStatModifiers[stat]?.Clear();
    }

    public void ClearAll()
    {
        _dictStatModifiers.Clear();
    }

    private float GetBaseStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.AttackDamage:
                return _baseStats.AttackDamage;

            case StatType.AttackSpeed:
                return _baseStats.AttackSpeed;

            case StatType.AttackCooldown:
                return _baseStats.AttackCooldown;

            case StatType.AttackDamageTriggerTime:
                return _baseStats.AttackDamageTriggerTime;

            case StatType.MoveSpeed:
                return _baseStats.MoveSpeed;

            case StatType.MaxHealth:
                return _baseStats.MaxHealth;

            default:
                return 0;
        }
    }
}

public enum StatType
{
    AttackDamage, 
    AttackSpeed, 
    AttackCooldown,
    AttackDamageTriggerTime, 

    MoveSpeed, 

    MaxHealth
}

public enum ModifierOperator 
{ 
    Add, 
    PercentAdd, 
    PercentMult 
}

[System.Serializable]
public class StatModifier
{
    // ----- FIELDS ----- //
    public StatType Stat;
    public ModifierOperator Operator;
    public float Value;
    // ----- FIELDS ----- //

    public StatModifier(StatType stat, ModifierOperator op, float value, object source = null)
    {
        Stat = stat; 
        Operator = op; 
        Value = value;
    }
}
