using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntNest : PlaceToDropItem
{
    [SerializeField] private Spawner _eggSpawner;
    [SerializeField] private GameObject _eggPrefab;

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return gameObject.activeSelf;
    }

    public override void TakeItem()
    {
        SpawnAnt();
    }
    private void SpawnAnt()
    {
        _eggSpawner.Spawn(_eggPrefab);
    }
}
