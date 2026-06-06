using System.Collections;
using TreeEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private bool _active;

    private PoolManager _poolManager;
    private Coroutine _spawnCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _poolManager = PoolManager.Instance;

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_active)
        {
            _poolManager.SpawnFromPool(_tag, transform.position, Quaternion.identity);
            yield return new WaitForEndOfFrame();
        }
    }    
}
