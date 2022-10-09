using System.Collections.Generic;
using UnityEngine;

public class AntMother : PlaceToDropItem
{
    [SerializeField] private Spawner _eggSpawner;
    [SerializeField] private GameObject _eggPrefab;

    private Construction[] _constructions;
    private bool _isAbleToSpawnEggs = false;

    public bool IsAbleToSpawnEggs => _isAbleToSpawnEggs;

    private void Awake()
    {
        _constructions = FindObjectsOfType<Construction>();
    }

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

    public override void TakeItem()
    {
        SpawnEgg();
    }

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return _isAbleToSpawnEggs;
    }

    private void SetAbleToSpawnEggs(bool isAntNestActivated)
    {
        _isAbleToSpawnEggs = isAntNestActivated;
    }

    private void SpawnEgg()
    {
        _eggSpawner.Spawn(_eggPrefab);
    }
}
