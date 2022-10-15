using UnityEngine;
using UnityEngine.Events;

public class Construction : PlaceToDropItem
{
    [SerializeField] private int _requiredNumberOfItems;
    [SerializeField] private GameObject _placeToDropEggsPrefab;

    private int _numberOfCollectedObjects = 0;

    private UnityEvent<bool> _onCollectedEnought = new UnityEvent<bool>();
    private UnityEvent<int> _onCollectedItemNumberChanged = new UnityEvent<int>();

    public event UnityAction<bool> OnCollectedEnough
    {
        add => _onCollectedEnought.AddListener(value);
        remove => _onCollectedEnought.RemoveListener(value);
    }

    public event UnityAction<int> OnCollectedItemNumberChanged
    {
        add => _onCollectedItemNumberChanged.AddListener(value);
        remove => _onCollectedItemNumberChanged.RemoveListener(value);
    }

    public int RequiredNumberOfItems => _requiredNumberOfItems;

    private void Start()
    {
        _onCollectedItemNumberChanged?.Invoke(_numberOfCollectedObjects);
    }

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return _numberOfCollectedObjects < _requiredNumberOfItems;
    }

    public override void TakeItem()
    {
        AddItem();

        if (_numberOfCollectedObjects == _requiredNumberOfItems)
        {
            _onCollectedEnought?.Invoke(_numberOfCollectedObjects == _requiredNumberOfItems);
            Instantiate(_placeToDropEggsPrefab, transform.position, Quaternion.identity);
        }
    }

    private void AddItem()
    {
        _numberOfCollectedObjects++;
        _onCollectedItemNumberChanged?.Invoke(_numberOfCollectedObjects);
    }
}
