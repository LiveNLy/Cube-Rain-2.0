using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private BombSpawner _spawner;

    private int _explosionTime;
    private int _minExplosionTime = 2;
    private int _maxExplosionTime = 5;
    private WaitForEndOfFrame _wait = new WaitForEndOfFrame();

    private void Awake()
    {
        _explosionTime = Random.Range(_minExplosionTime, _maxExplosionTime + 1);
    }

    public void Explode()
    {
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        _renderer.material.color = new Color(0, 0, 0, 1);
        float colorChannelAMax = 1f;
        float colorChannelA = 1f;

        while (_renderer.material.color.a > 0)
        {
            _renderer.material.color = new Color(0, 0, 0, colorChannelA -= colorChannelAMax * Time.deltaTime / _explosionTime);


            yield return _wait;
        }

        _exploder.Explode();
        _spawner.ReleaseObject(this);
    }
}
