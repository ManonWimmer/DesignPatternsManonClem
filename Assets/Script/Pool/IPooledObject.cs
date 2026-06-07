using UnityEngine;

public interface IPooledObject
{
    void SendObjectToPool();
    void LinkPool(Pool pool);
}
