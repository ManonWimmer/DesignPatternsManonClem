using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Stats")]
    [SerializeField] private StatsSO _stats;

    [Header("Damage")]
    [SerializeField] private DamageTrigger _damageTrigger;

    private bool _bIsInCooldown = false;
    private float _waitedCooldownTime = 0;

    private bool _bIsTriggerEnabled = false;
    private float _waitedTriggerTime = 0;

    public event Action OnAttack;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_damageTrigger)
            _damageTrigger.OnHealthControllerDamaged += ApplyDamageToHealthController;
    }

    private void OnDestroy()
    {
        if (_damageTrigger)
            _damageTrigger.OnHealthControllerDamaged -= ApplyDamageToHealthController;
    }

    public void ApplyDamageToHealthController(HealthController healthController)
    {
        if (!healthController ||!_stats)
            return;

        healthController.TakeDamage(_stats.AttackDamage);
    }

    public bool CanAttack()
    {
        return !_bIsInCooldown;
    }

    public void Attack()
    {
        if (!CanAttack())
            return;

        if (_damageTrigger)
        {
            _damageTrigger.ActivateTrigger();
            _bIsTriggerEnabled = true;
        }

        _bIsInCooldown = true;

        OnAttack?.Invoke();
    }

    private void Update()
    {
        if (!_stats)
            return;

        if (_bIsInCooldown)
        {
            _waitedCooldownTime += Time.deltaTime;

            if (_waitedCooldownTime > _stats.AttackCooldown)
            {
                _bIsInCooldown = false;
                _waitedCooldownTime = 0;
            }
        }
        
        if (_bIsTriggerEnabled && _damageTrigger)
        {
            _waitedTriggerTime += Time.deltaTime;

            if (_waitedTriggerTime > _stats.AttackDamageTriggerTime)
            {
                _damageTrigger.DeactivateTrigger();
                _bIsTriggerEnabled = false;
                _waitedTriggerTime = 0;
            }
        }
    }
}
