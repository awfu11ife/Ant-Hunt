using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class BotTargetSwitcher : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private List<FoodPiecesContainer> _food = new List<FoodPiecesContainer>();
    private List<BotMover> _antBots = new List<BotMover>();
    private AntMother _antMother;

    private UnityEvent<FoodPiecesContainer> _onTargetDisable = new UnityEvent<FoodPiecesContainer>();

    public event UnityAction<FoodPiecesContainer> OnTargetDisable
    {
        add => _onTargetDisable.AddListener(value);
        remove => _onTargetDisable.RemoveListener(value);
    }

    private void Awake()
    {
        FindAllFood();
        _antMother = FindObjectOfType<AntMother>();
    }

    private void OnEnable()
    {
        _spawner.OnObjectSpawned += AddAnt;

        foreach (var foodPieceContainer in _food)
        {
            foodPieceContainer.OnPossibilityToEatChanged += ChangeCurrentTargetIfPreviousDisable;
        }
    }

    private void OnDisable()
    {
        _spawner.OnObjectSpawned -= AddAnt;
    }

    public GameObject FindAvaliablTarget(bool isInventoryFull)
    {
        if (isInventoryFull == false)
        {

            var eatableObject = _food.FirstOrDefault(s => s.IsAbleToEat == true);

            if (eatableObject == null)
                return null;
            else
                return eatableObject.gameObject;
        }
        else
        {
            return _antMother.gameObject;
        }
    }

    private void FindAllFood()
    {
        foreach (FoodPiecesContainer food in FindObjectsOfType<FoodPiecesContainer>())
        {
            _food.Add(food);
        }
    }

    private void AddAnt(GameObject ant)
    {
        if (ant.TryGetComponent(out BotMover botMover))
            _antBots.Add(botMover);
    }

    private void ChangeCurrentTargetIfPreviousDisable(FoodPiecesContainer foodPiecesContainer)
    {
        _onTargetDisable?.Invoke(foodPiecesContainer);
    }
}
