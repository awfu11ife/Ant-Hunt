using UnityEngine;
using UnityEngine.Events;

public class Construction : PlaceToDropItem
{
    [SerializeField] private int _requiredNumberOfItems;
    [SerializeField] private GameObject _placeToDropEggsPrefab;

    private int _numberOfCollectedObjects;
    private UnityEvent<bool> _onCollectedEnough = new UnityEvent<bool>();

    public event UnityAction<bool> OnCollectedEnough
    {
        add => _onCollectedEnough.AddListener(value);
        remove => _onCollectedEnough.RemoveListener(value);
    }

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return gameObject.activeSelf;
    }

    public override void TakeItem()
    {
        AddItem();

        if (_numberOfCollectedObjects == _requiredNumberOfItems)
        {
            gameObject.SetActive(false);
            _onCollectedEnough?.Invoke(!gameObject.activeSelf);
            Instantiate(_placeToDropEggsPrefab, transform.position, Quaternion.identity);
        }
    }

    private void AddItem()
    {
        _numberOfCollectedObjects++;
    }
}
