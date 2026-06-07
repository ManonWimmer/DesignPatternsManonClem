using System.Collections;
using UnityEngine;

public interface IPooledObject
{
    public Pool Pool { get; }

    IEnumerator SendObjectToPool();
    void LinkPool(Pool pool);
}
