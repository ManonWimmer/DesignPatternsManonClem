using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Collider _trigger = null;

    private List<IDamageable> _damaged = new();

    public event Action<IDamageable> OnDamageableHit;
    // ----- FIELDS ----- //
    private void Start()
    {
        DeactivateTrigger();
    }

    public void ActivateTrigger(float duration)
    {
        _damaged.Clear();

        if (!_trigger)
        {
            Debug.LogError("Trigger null in damage trigger");
            return;
        }

        _trigger.enabled = true;
        StartCoroutine(AutoDeactivate(duration));
    }

    private IEnumerator AutoDeactivate(float duration)
    {
        yield return new WaitForSeconds(duration);
        DeactivateTrigger();
    }

    public void DeactivateTrigger()
    {
        if (!_trigger)
        {
            Debug.LogError("Trigger null in damage trigger");
            return;
        }

        _trigger.enabled = false;
        _damaged.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableProxy proxy = other.GetComponent<DamageableProxy>();

        if (!proxy) 
            return;

        IDamageable damageable = proxy.GetDamageable();

        if (damageable == null)
            return;

        if (_damaged.Contains(damageable))
            return;

        _damaged.Add(damageable);

        OnDamageableHit?.Invoke(damageable);
    }
}
