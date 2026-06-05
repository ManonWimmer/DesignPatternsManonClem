using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance => _instance;

    [SerializeField] private List<Pool> _poolList;
    private Dictionary<string, Pool> _poolDict;

    private void Awake()
    {
        if (Instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _poolDict = new Dictionary<string, Pool>();

        foreach(Pool pool in _poolList)
        {
            _poolDict.Add(pool.Tag, pool);
            pool.CreateObjects();
        }
    }
}
