using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnObject(_repeatRate));
    }

    protected override Vector3 SetPosition()
    {
        Vector3 position = transform.position;
        float minRandom = -15f;
        float maxRandom = 15f;

        position.x = transform.position.x - Random.Range(minRandom, maxRandom);
        position.z = transform.position.z - Random.Range(minRandom, maxRandom);

        return position;
    }

    protected IEnumerator SpawnObject(float seconds)
    {
        var wait = new WaitForSeconds(seconds);

        while (enabled)
        {
            GetObject();
            yield return wait;
        }
    }
}