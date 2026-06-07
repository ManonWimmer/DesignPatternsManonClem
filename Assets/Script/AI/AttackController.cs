using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Stats")]
    [SerializeField] private AlterableStatsController _stats;

    [Header("Damage")]
    [SerializeField] private DamageTrigger _damageTrigger;

    [Header("Check is dead")]
    [SerializeField] private HealthController _healthController;

    private bool _isInCooldown = false;
    private float _waitedCooldownTime = 0;
    private float _currentCooldownDuration = 0;

    private bool _canAttack = true;

    public event Action OnAttack;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_damageTrigger)
            _damageTrigger.OnDamageableHit += ApplyDamage;

        if (_healthController)
            _healthController.OnDie += HandleDeath;
    }

    private void OnDestroy()
    {
        if (_damageTrigger)
            _damageTrigger.OnDamageableHit -= ApplyDamage;

        if (_healthController)
            _healthController.OnDie -= HandleDeath;
    }

    private void HandleDeath()
    {
        _canAttack = false;
    }

    public void ApplyDamage(IDamageable damageable)
    {
        if (damageable == null || !_stats)
            return;

        damageable.TakeDamage(_stats.GetStat(StatType.AttackDamage));
    }

    public bool CanAttack()
    {
        return !_isInCooldown && _canAttack;
    }

    public void Attack()
    {
        if (!CanAttack())
            return;

        print("attack");

        if (_damageTrigger)
            _damageTrigger.ActivateTrigger(_stats.GetStat(StatType.AttackDamageTriggerTime));

        _isInCooldown = true;
        _currentCooldownDuration = _stats.GetStat(StatType.AttackCooldown);

        OnAttack?.Invoke();
    }

    private void Update()
    {
        if (!_stats)
            return;

        if (_isInCooldown)
        {
            _waitedCooldownTime += Time.deltaTime;

            if (_waitedCooldownTime > _currentCooldownDuration)
            {
                _isInCooldown = false;
                _waitedCooldownTime = 0;
            }
        }
    }
}
