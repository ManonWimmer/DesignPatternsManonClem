using System.Collections;
using UnityEngine;

public interface IPooledObject
{
    IEnumerator SendObjectToPool();
    void LinkPool(Pool pool);
}
