using UnityEngine;

public class AntMother : PlaceToDropItem
{
    [SerializeField] private GameObject _eggPrefab;
    [SerializeField] private int _requiredNumberOfPiecesToSpawn = 1;
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private GameObject _openState;
    [SerializeField] private GameObject _closeState;

    private Construction[] _constructions;
    private bool _isAbleToSpawnEggs = false;
    private int _currentNumberOfCollectedPieces;

    public bool IsAbleToSpawnEggs => _isAbleToSpawnEggs;

    private void Awake()
    {
        _constructions = FindObjectsOfType<Construction>();
    }

    private void OnEnable()
    {
        foreach (var construction in _constructions)
        {
            construction.OnCollectedEnough += SetAbleToSpawnEggs;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _constructions)
        {
            item.OnCollectedEnough -= SetAbleToSpawnEggs;
        }
    }

    public override void TakeItem(InventoryViewObject inventoryViewObject)
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
        Instantiate(_openParticle, transform);
        _closeState.SetActive(false);
        _openState.SetActive(true);
    }

    private void SpawnEgg()
    {
        _currentNumberOfCollectedPieces++;

        if (_requiredNumberOfPiecesToSpawn == _currentNumberOfCollectedPieces)
        {
            Spawner.Spawn(_eggPrefab);
            _currentNumberOfCollectedPieces = 0;
        }
    }
}
