using UnityEngine;

public class PooledMinion : MonoBehaviour, IPooledObject
{
    [SerializeField] private HealthController _healthController;
    
    private Pool _pool;


    private void Awake()
    {
        _healthController.OnDie += SendObjectToPool;
    }

    public void LinkPool(Pool pool)
    {
        _pool = pool;
    }

    public void SendObjectToPool()
    {
        _pool.AddObject(gameObject);
        gameObject.SetActive(false);
        _healthController.Health = _healthController.MaxHealth;
    }
}
