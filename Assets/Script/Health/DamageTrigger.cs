using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Collider _trigger = null;

    private List<HealthController> _damaged = new();

    public event Action<HealthController> OnHealthControllerDamaged;
    // ----- FIELDS ----- //
    private void Start()
    {
        DeactivateTrigger();
    }

    public void ActivateTrigger()
    {
        if (!_trigger)
        {
            Debug.LogError("Trigger null in damage trigger");
            return;
        }

        _trigger.enabled = true;
    }

    public void DeactivateTrigger()
    {
        if (!_trigger)
        {
            Debug.LogError("Trigger null in damage trigger");
            return;
        }

        _trigger.enabled = false;

        if (_damaged.Count > 0)
            _damaged.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthProxy healthProxy = other.GetComponent<HealthProxy>();

        if (!healthProxy)
            return;

        HealthController healthController = healthProxy.GetHealthController();

        if (!healthController)
            return;

        // Pas plusieurs hit sur le meme durant la meme activation de trigger
        if (_damaged.Contains(healthController))
            return;

        _damaged.Add(healthController);
        OnHealthControllerDamaged?.Invoke(healthController);
    }
}
