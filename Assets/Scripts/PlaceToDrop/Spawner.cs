using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject eggPrefab)
    {
        float width = transform.lossyScale.x;
        float lenth = transform.lossyScale.z;

        float xRange = Random.Range(-width / 2, width / 2);
        float zRange = Random.Range(-lenth / 2, lenth / 2);

        Vector3 spawnerPosition = transform.position;
        Vector3 spawnPosition = new Vector3(spawnerPosition.x + xRange, spawnerPosition.y + eggPrefab.transform.localScale.y / 2, spawnerPosition.z + zRange);

        Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
    }
}
