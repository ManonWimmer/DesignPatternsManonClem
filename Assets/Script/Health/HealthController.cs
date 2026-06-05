using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    // ----- FIELDS ----- //
    [Header("Values")]
    [SerializeField] private float _maxHealth = 3f;

    private float _health;
    public float Health { get => _health; set => _health = value; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDie;
    // ----- FIELDS ----- //

    private void Start()
    {
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
