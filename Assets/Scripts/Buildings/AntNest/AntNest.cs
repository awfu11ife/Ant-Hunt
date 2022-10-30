using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotStats))]
public class AntNest : PlaceToDropItem
{
    [SerializeField] private GameObject _antBotPrefab;
    [SerializeField] private float _startSpawnDelay = 0.4f;

    private BotStats _botStats;
    private AntCounter _antCounter;

    public int MaxNumberOfAnts { get; private set; }
    public int CurrentNumberOfAnts { get; private set; }

    private void Awake()
    {
        _botStats = GetComponent<BotStats>();
        _antCounter = FindObjectOfType<AntCounter>();
    }

    private void OnEnable()
    {
        _botStats.OnAmountChanged += UpdateMaxNumber;
    }

    private void OnDisable()
    {
        _botStats.OnAmountChanged -= UpdateMaxNumber;
    }

    private void Start()
    {
        CurrentNumberOfAnts = _botStats.CurrentNumberOfBots;
        MaxNumberOfAnts = _botStats.CurrentMaxAntAmount;
        _antCounter.UpdateAntCounterText(CurrentNumberOfAnts, MaxNumberOfAnts);
        Spawner.Spawn(_antBotPrefab, CurrentNumberOfAnts, _startSpawnDelay);
    }

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return CurrentNumberOfAnts < MaxNumberOfAnts;
    }

    public override void TakeItem(InventoryViewObject inventoryViewObject)
    {
        SpawnAnt();
        CurrentNumberOfAnts++;
        _antCounter.UpdateAntCounterText(CurrentNumberOfAnts, MaxNumberOfAnts);
    }
    private void SpawnAnt()
    {
        Spawner.Spawn(_antBotPrefab);
    }

    private void UpdateMaxNumber()
    {
        MaxNumberOfAnts = _botStats.CurrentMaxAntAmount;
        _antCounter.UpdateAntCounterText(CurrentNumberOfAnts, MaxNumberOfAnts);
    }
}
