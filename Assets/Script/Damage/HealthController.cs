using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    // ----- FIELDS ----- //
    private float _health;
    public float Health { get => _health; set => _health = value; }
    // ----- FIELDS ----- //

    public void TakeDamage(float damage)
    {
        Debug.Log($"take damage : {damage}");
    }
}
