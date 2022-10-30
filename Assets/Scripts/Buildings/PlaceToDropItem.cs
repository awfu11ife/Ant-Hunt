using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlaceToDropItem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CollectableObjectTypes[] _collectableObjectTypes;

    public Spawner Spawner => _spawner;

    public abstract void TakeItem(InventoryViewObject inventoryViewObject);

    public abstract bool CheckConditionOfPossibilityToTakeItem();

    public bool TryMatchRequiredType(CollectableObjectTypes sentCollectableObjectType)
    {
        foreach (CollectableObjectTypes collectableObjectType in _collectableObjectTypes)
        {
            if (sentCollectableObjectType == collectableObjectType)
                return true;
        }

        return false;
    }
}
