using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class AlterableStatsController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private StatsSO _baseStats;

    private Dictionary<StatType, List<StatModifier>> _modifiers = new();

    public event Action<StatType> OnStatChanged;
    // ----- FIELDS ----- //

    public float GetStat(StatType stat)
    {
        float value = _baseStats.GetBaseValue(stat);

        if (!_modifiers.TryGetValue(stat, out var list) || list.Count == 0)
            return value;

        // Apply modifiers in order
        foreach (var mod in list)
        {
            switch (mod.Operator)
            {
                case ModifierOperator.Add: value += mod.Value; break;
                case ModifierOperator.PercentAdd: value *= (1f + mod.Value); break;
                case ModifierOperator.PercentMult: value *= (1f + mod.Value); break;
            }
        }
        return value;
    }

    public void AddModifier(StatModifier mod)
    {
        if (!_modifiers.ContainsKey(mod.Stat))
            _modifiers[mod.Stat] = new List<StatModifier>();

        _modifiers[mod.Stat].Add(mod);
        OnStatChanged?.Invoke(mod.Stat);
    }

    public void RemoveModifier(StatModifier mod)
    {
        if (_modifiers[mod.Stat]?.Remove(mod) == true)
            OnStatChanged?.Invoke(mod.Stat);
    }

    public void ClearStat(StatType stat)
    {
        _modifiers[stat]?.Clear();
    }

    public void ClearAll()
    {
        _modifiers.Clear();
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

    public StatModifier(StatType stat, ModifierOperator op, float value)
    {
        Stat = stat; 
        Operator = op; 
        Value = value;
    }
}
