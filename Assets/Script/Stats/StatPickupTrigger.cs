using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickupTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private StatPickupSO _data;

    private List<StatModifier> _appliedModifiers = new();

    public event Action OnCollected;
    // ----- FIELDS ----- //

    private void OnTriggerEnter(Collider other)
    {
        StatsProxy statsProxy = other.GetComponent<StatsProxy>();
        AlterableStatsController stats = null;
        if (!statsProxy) 
            return;

        if (statsProxy)
            stats = statsProxy.GetStats();

        if (!stats)
            return;

        foreach (var mod in _data.Modifiers)
        {
            var copy = new StatModifier(mod.Stat, mod.Operator, mod.Value);
            _appliedModifiers.Add(copy);
            stats.AddModifier(copy);
        }

        OnCollected?.Invoke();

        if (_data.Duration > 0)
            StartCoroutine(RemoveAfter(stats, _data.Duration));
        else
            gameObject.SetActive(false); 
    }

    private IEnumerator RemoveAfter(AlterableStatsController stats, float duration)
    {
        yield return new WaitForSeconds(duration);

        foreach (var mod in _appliedModifiers)
            stats.RemoveModifier(mod);

        _appliedModifiers.Clear();
        gameObject.SetActive(false);
    }
}
