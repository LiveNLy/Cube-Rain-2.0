using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _cubePosition;

    private void OnEnable()
    {
        _cubeSpawner.SendWhereObjDestroed += GetPosition;
        _cubeSpawner.SpawningBomb += GetObject;
    }

    private void OnDisable()
    {
        _cubeSpawner.SendWhereObjDestroed -= GetPosition;
        _cubeSpawner.SpawningBomb -= GetObject;
    }

    protected override Vector3 SetPosition()
    {
        return _cubePosition;
    }

    protected override void SetObject(Bomb obj)
    {
        base.SetObject(obj);
        obj.Explode();
    }

    private void GetPosition(Vector3 position)
    {
        _cubePosition = position;
    }
}
