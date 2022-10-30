using UnityEngine;
using UnityEngine.Events;

public class ItemBuyer : PlaceToDropItem
{
    [SerializeField] private int _eggPrice;
    [SerializeField] private int _foodPrice;
    [SerializeField] private ParticleSystem _buyParticle;

    private bool _isAbleToTakeItem = true;

    private UnityEvent<int> _onBuy = new UnityEvent<int>();

    public event UnityAction<int> OnBuy
    {
        add => _onBuy.AddListener(value);
        remove => _onBuy.RemoveListener(value);
    }

    public override bool CheckConditionOfPossibilityToTakeItem()
    {
        return _isAbleToTakeItem;
    }

    public override void TakeItem(InventoryViewObject inventoryViewObject)
    {
        if (inventoryViewObject.CollectableObjectTypes == CollectableObjectTypes.Egg)
            _onBuy?.Invoke(_eggPrice);
        if (inventoryViewObject.CollectableObjectTypes == CollectableObjectTypes.Food)
            _onBuy?.Invoke(_foodPrice);

        Instantiate(_buyParticle, transform.position, Quaternion.identity);
    }
}
