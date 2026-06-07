using System.Collections;
using UnityEngine;

public class PooledMinion : MonoBehaviour, IPooledObject
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private BehaviorController _behaviorController;
    [SerializeField] private float _timeBeforeReturningToPool;
    
    private Pool _pool;

    public Pool Pool => _pool;

    private void Start()
    {
        _healthController.OnDie += StartReturnToPool;
    }

    public void LinkPool(Pool pool)
    {
        _pool = pool;
    }


    private void Update()
    {
        //Debug.Log(transform.position);
    }

    private void StartReturnToPool()
    {
        StartCoroutine(SendObjectToPool());
    }

    public IEnumerator SendObjectToPool()
    {
        yield return new WaitForSeconds(_timeBeforeReturningToPool);
        _pool.AddObject(gameObject);
        gameObject.SetActive(false);
        _healthController.RefreshMaxHealth();

        if (_behaviorController)
            _behaviorController.RestartBehavior();
    }
}
