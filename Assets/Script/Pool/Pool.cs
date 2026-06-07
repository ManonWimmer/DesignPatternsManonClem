using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Pool
{
    [SerializeField] private string _tag;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _size;

    private Queue<GameObject> _objectPool;

    public string Tag => _tag;
    public GameObject Prefab => _prefab;
    public int Size => _size;
    public int Count => _objectPool.Count;

    public void CreateObjects()
    {
        _objectPool = new Queue<GameObject>();

        for (int i = 0; i < _size; i++)
        {
            GameObject obj = GameObject.Instantiate(_prefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);

            if(obj.TryGetComponent(out IPooledObject pooledObject))
            {
                pooledObject.LinkPool(this);
            }
        }
    }

    public GameObject GetObject()
    {
        return _objectPool.Dequeue();
    }

    public void AddObject(GameObject obj)
    {
        _objectPool.Enqueue(obj);
    }
}
