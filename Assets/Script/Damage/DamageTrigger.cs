using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Collider _trigger = null;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthProxy healthProxy = other.GetComponent<HealthProxy>();

        if (!healthProxy)
            return;

        HealthController healthController = healthProxy.GetHealthController();

        if (!healthController)
            return;

        OnHealthControllerDamaged?.Invoke(healthController);
    }
}
