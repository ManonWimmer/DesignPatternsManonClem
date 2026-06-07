using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance => _instance;

    [Header("Pools")]
    [SerializeField] private List<Pool> _poolList;
    private Dictionary<string, Pool> _poolDict;

    [Header("Enemies patrols")]
    [SerializeField] private List<GameObject> _patrolPoints = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) _instance = this;
        else Destroy(gameObject);

        _poolDict = new Dictionary<string, Pool>();

        foreach (Pool pool in _poolList)
        {
            _poolDict.Add(pool.Tag, pool);
            pool.CreateObjects();
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDict.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with {tag} doesn't exist.");
            return null;
        }

        if (_poolDict[tag].Count > 0)
        {
            print("spawn");

            GameObject obj = _poolDict[tag].GetObject();

            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            SetupObject(obj);
            return obj;
        }

        return null;
    }

    private void SetupObject(GameObject obj)
    {
        if(obj.TryGetComponent(out BehaviorGraphAgent agent))
        {
            bool value = agent.BlackboardReference.SetVariableValue("PatrolPoints", _patrolPoints);
            Debug.Log(value);
        }
    }
}
