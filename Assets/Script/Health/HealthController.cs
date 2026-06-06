using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    // ----- FIELDS ----- //
    [Header("Stats")]
    [SerializeField] private AlterableStatsController _stats;

    private float _health;
    private float _maxHealth;

    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDie;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (!_stats)
            return;

        _stats.OnStatChanged += HandleStatChanged;
        RefreshMaxHealth();

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    private void OnDestroy()
    {
        if (!_stats) return;

        _stats.OnStatChanged -= HandleStatChanged;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"take damage : {damage}");
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            OnDie?.Invoke();
        }

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    private void HandleStatChanged(StatType stat)
    {
        if (stat != StatType.MaxHealth) 
            return;

        float newMax = _stats.GetStat(StatType.MaxHealth);
        float delta = newMax - _maxHealth;
        _maxHealth = newMax;
        _health = Mathf.Clamp(_health + delta, 0f, _maxHealth);

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    private void RefreshMaxHealth()
    {
        _maxHealth = _stats.GetStat(StatType.MaxHealth);
        _health = _maxHealth;
    }
}
