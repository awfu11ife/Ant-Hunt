using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeViewContainer : MonoBehaviour
{
    private UpgradeView[] _upgradeViews;

    private void Awake()
    {
        _upgradeViews = GetComponentsInChildren<UpgradeView>();
    }

    public void Activate()
    {
        foreach (var item in _upgradeViews)
        {
            item.ActivateView();
        }
    }

    public void Deactivate()
    {
        foreach (var item in _upgradeViews)
        {
            item.DeativateView();
        }
    }
}
