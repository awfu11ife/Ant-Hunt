using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlaceToDropItem : MonoBehaviour
{
    [SerializeField] private CollectableObjectTypes _collectableObjectTypes;

    public CollectableObjectTypes RequiredType => _collectableObjectTypes;

    public abstract void TakeItem();

    public abstract bool CheckConditionOfPossibilityToTakeItem();
}
