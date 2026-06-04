using UnityEngine;

public interface IDamageable 
{
    // ----- FIELDS ----- //
    public float Health { get; set; }
    // ----- FIELDS ----- //

    public void TakeDamage(float damage);
}
