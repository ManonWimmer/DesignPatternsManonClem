using System;
using System.Collections.Generic;
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

    public void CreateObjects()
    {
        _objectPool = new Queue<GameObject>();

        for (int i = 0; i < _size; i++)
        {
            GameObject obj = GameObject.Instantiate(_prefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }
}
