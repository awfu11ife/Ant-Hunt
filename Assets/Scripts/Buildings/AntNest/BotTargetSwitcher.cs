using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotTargetSwitcher : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private BotStats _botStats;

    private List<FoodPiecesContainer> _food = new List<FoodPiecesContainer>();
    private List<BotMover> _antBots = new List<BotMover>();
    private AntMother _antMother;
    private ItemBuyer _itemBuyer;

    private UnityEvent<CollectableObject> _onTargetDisable = new UnityEvent<CollectableObject>();

    public event UnityAction<CollectableObject> OnTargetDisable
    {
        add => _onTargetDisable.AddListener(value);
        remove => _onTargetDisable.RemoveListener(value);
    }

    private void Awake()
    {
        FindAllFood();
        _antMother = FindObjectOfType<AntMother>();
        _itemBuyer = FindObjectOfType<ItemBuyer>();
    }

    private void OnEnable()
    {
        _spawner.OnObjectSpawned += AddAnt;

        foreach (var foodPieceContainer in _food)
        {
            foodPieceContainer.OnPieceRemoved += ChangeCurrentTargetIfDisable;
        }
    }

    private void OnDisable()
    {
        _spawner.OnObjectSpawned -= AddAnt;
    }

    public GameObject FindAvaliablTarget(bool isInventoryFull, Vector3 currentPosition)
    {
        var eatableObject = ChooseNearestFood(currentPosition);

        if (isInventoryFull == false && eatableObject != null)
            return eatableObject.ReturnNearestPiece(currentPosition).gameObject;
        else //if (_botStats.CurrentMaxAntAmount <= _antBots.Count && isInventoryFull == true)
            return _itemBuyer.gameObject;
        //else
        //    return _antMother.gameObject;
    }

    private void FindAllFood()
    {
        foreach (FoodPiecesContainer food in FindObjectsOfType<FoodPiecesContainer>())
        {
            _food.Add(food);
        }
    }

    private FoodPiecesContainer ChooseNearestFood(Vector3 currentPosition)
    {
        FoodPiecesContainer nearestFood = null;
        float distance = Mathf.Infinity;

        foreach (var food in _food)
        {
            float currentDistance = (food.transform.position - currentPosition).sqrMagnitude;

            if (currentDistance < distance && food.IsAbleToEat == true && food.isActiveAndEnabled == true)
            {
                nearestFood = food;
                distance = currentDistance;
            }

        }

        return nearestFood;
    }

    private void AddAnt(GameObject ant)
    {
        if (ant.TryGetComponent(out BotMover botMover))
            _antBots.Add(botMover);
    }

    private void ChangeCurrentTargetIfDisable(CollectableObject piece)
    {
        _onTargetDisable?.Invoke(piece);
    }
}
