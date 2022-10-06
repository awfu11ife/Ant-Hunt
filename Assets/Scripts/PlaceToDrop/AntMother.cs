using System.Collections.Generic;
using UnityEngine;

public class AntMother : MonoBehaviour
{
    [SerializeField] private CollectableObjectTypes _requiredType;
    [SerializeField] private EggSpawner _eggSpawner;
    [SerializeField] private GameObject _eggPrefab;
    [SerializeField] private List<Construction> _constructions = new List<Construction>();

    private bool _isAbleToSpawnEggs = false;

    public CollectableObjectTypes requiredType => _requiredType;
    public bool IsAbleToSpawnEggs => _isAbleToSpawnEggs;

    private void OnEnable()
    {
        foreach (var item in _constructions)
        {
            item.OnCollectedEnough += SetAbleToSpawnEggs;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _constructions)
        {
            item.OnCollectedEnough -= SetAbleToSpawnEggs;
        }
    }

    public void SpawnEgg()
    {
        _eggSpawner.Spawn(_eggPrefab);
    }
    private void SetAbleToSpawnEggs()
    {
        _isAbleToSpawnEggs = true;
    }
}
