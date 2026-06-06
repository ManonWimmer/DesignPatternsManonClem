using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Stats")]
    [SerializeField] private AlterableStatsController _stats;

    [Header("Damage")]
    [SerializeField] private DamageTrigger _damageTrigger;

    private bool _bIsInCooldown = false;
    private float _waitedCooldownTime = 0;
    private float _currentCooldownDuration = 0;

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

        healthController.TakeDamage(_stats.GetStat(StatType.AttackDamage));
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
            _damageTrigger.ActivateTrigger(_stats.GetStat(StatType.AttackDamageTriggerTime));

        _bIsInCooldown = true;
        _currentCooldownDuration = _stats.GetStat(StatType.AttackCooldown);

        OnAttack?.Invoke();
    }

    private void Update()
    {
        if (!_stats)
            return;

        if (_bIsInCooldown)
        {
            _waitedCooldownTime += Time.deltaTime;

            if (_waitedCooldownTime > _currentCooldownDuration)
            {
                _bIsInCooldown = false;
                _waitedCooldownTime = 0;
            }
        }
    }
}
