using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;

    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;

    public event Action<Vector3> CubeDestroying;

    private int _lifeTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out MeshCollider platform))
        {
            StartCoroutine(CountLifeTime());
        }
    }

    protected IEnumerator CountLifeTime()
    {
        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime + 1);

        yield return new WaitForSeconds(_lifeTime);

        CubeDestroying?.Invoke(transform.position);
        _spawner.ReleaseObject(this);
    }
}