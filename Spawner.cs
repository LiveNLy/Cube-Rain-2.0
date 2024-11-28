using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<P> : MonoBehaviour where P : MonoBehaviour
{
    [SerializeField] private P _prefab;
    [SerializeField] private int _poolDefaultCapacity = 5;
    [SerializeField] private int _poolMaxSize = 12;

    private ObjectPool<P> _pool;

    public event Action<float, float, float> SendInfo;
    public event Action<Vector3> SendWhereObjDestroed;
    public event Action SpawningBomb;

    private float _createdObjects;

    private void Awake()
    {
        _pool = new ObjectPool<P>(
            actionOnGet: (obj) => SetObject(obj),
            createFunc: () => InstantiateObject(),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize);
    }

    public void ReleaseObject(P obj)
    {
        SendWhereObjDestroed?.Invoke(obj.transform.position);
        _pool.Release(obj);
        SpawningBomb?.Invoke();
    }

    protected abstract Vector3 SetPosition();

    private P InstantiateObject()
    {
        return Instantiate(_prefab);
    }

    protected void GetObject()
    {
        _pool.Get();
    }

    protected virtual void SetObject(P obj)
    {
        ++_createdObjects;
        SendInfo?.Invoke(_createdObjects, _pool.CountAll, _pool.CountActive);

        obj.transform.position = SetPosition();
        obj.gameObject.SetActive(true);
    }
}