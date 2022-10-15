using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;

    private UnityEvent<GameObject> _onObjectSpawned = new UnityEvent<GameObject>();

    public event UnityAction<GameObject> OnObjectSpawned
    {
        add => _onObjectSpawned.AddListener(value);
        remove => _onObjectSpawned.RemoveListener(value);
    }
    public Vector3 SpawnPosition { get; private set; }

    public void Spawn(GameObject prefab)
    {
        CalculateSpawnPosition(prefab);

        GameObject instantiatedObject = Instantiate(prefab, _parentTransform.position, Quaternion.identity);
        instantiatedObject.transform.SetParent(transform);

        _onObjectSpawned?.Invoke(instantiatedObject);
    }

    private void CalculateSpawnPosition(GameObject eggPrefab)
    {
        float width = transform.lossyScale.x;
        float lenth = transform.lossyScale.z;

        float xRange = Random.Range(-width / 2, width / 2);
        float zRange = Random.Range(-lenth / 2, lenth / 2);

        Vector3 spawnerPosition = transform.position;
        SpawnPosition = new Vector3(spawnerPosition.x + xRange, spawnerPosition.y + eggPrefab.transform.localScale.y / 2, spawnerPosition.z + zRange);
    }
}
