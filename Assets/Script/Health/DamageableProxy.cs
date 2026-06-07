using UnityEngine;

public class DamageableProxy : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private MonoBehaviour _damageableTarget;
    private IDamageable _damageable;
    // ----- FIELDS ----- //

    private void Awake()
    {
        _damageable = _damageableTarget as IDamageable;
        if (_damageable == null)
            Debug.LogError("DamageableProxy : _damageableTarget n'implémente pas IDamageable", this);
    }

    public IDamageable GetDamageable()
    {
        return _damageable;
    }
}
