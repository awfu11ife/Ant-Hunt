using System.Collections;
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

    public void Spawn(GameObject prefab, int numberOfSpawnObject = 1, float spawnDelay = 0)
    {
        StartCoroutine(SpawnDelay(prefab, numberOfSpawnObject, spawnDelay));
    }

    private void CalculateSpawnPosition(GameObject prefab)
    {
        float width = transform.lossyScale.x;
        float lenth = transform.lossyScale.z;

        float xRange = Random.Range(-width / 2, width / 2);
        float zRange = Random.Range(-lenth / 2, lenth / 2);

        Vector3 spawnerPosition = transform.position;
        SpawnPosition = new Vector3(spawnerPosition.x + xRange, spawnerPosition.y + prefab.transform.localScale.y / 2, spawnerPosition.z + zRange);
    }

    private IEnumerator SpawnDelay(GameObject prefab, int numberOfSpawnObject, float spawnDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(spawnDelay);

        for (int i = 0; i < numberOfSpawnObject; i++)
        {
            CalculateSpawnPosition(prefab);

            GameObject instantiatedObject = Instantiate(prefab, _parentTransform.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(_parentTransform);

            _onObjectSpawned?.Invoke(instantiatedObject);

            yield return delay;
        }
    }
}
