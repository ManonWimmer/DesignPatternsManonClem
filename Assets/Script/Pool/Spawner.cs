using System.Collections;
using TreeEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private bool _active;
    [SerializeField, Min(0f)] private float _spawnDelay = 2f;

    private PoolManager _poolManager;
    private Coroutine _spawnCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _poolManager = PoolManager.Instance;

        if (_active)
            StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine != null) 
            return;

        if (PoolManager.Instance == null)
        {
            Debug.LogError("[Spawner] PoolManager instance not found.");
            return;
        }

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine == null) 
            return;

        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnCoroutine()
    {
        var delay = new WaitForSeconds(_spawnDelay);
        while (true)
        {
            PoolManager.Instance.SpawnFromPool(_tag, transform.position, Quaternion.identity);
            yield return delay;
        }
    }    
}
