using NUnit.Framework;
using System;
using System.Collections;
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
