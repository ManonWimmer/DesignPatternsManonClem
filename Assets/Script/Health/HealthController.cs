using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    // ----- FIELDS ----- //
    [Header("Stats")]
    [SerializeField] private StatsSO _stats;

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

        _maxHealth = _stats.MaxHealth;
        _health = _maxHealth;
        OnHealthChanged?.Invoke(_health, _maxHealth);
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
}
