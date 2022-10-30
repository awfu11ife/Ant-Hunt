using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Construction))]
public class ConstructionUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _counterText;

    private Construction _construction;

    private void Awake()
    {
        _construction = GetComponent<Construction>();
    }

    private void OnEnable()
    {
        _construction.OnCollectedItemNumberChanged += ChangeCounter;
    }

    private void OnDisable()
    {
        _construction.OnCollectedItemNumberChanged -= ChangeCounter;
    }

    private void ChangeCounter(int currentNumberOfCollectedObjects)
    {
        _counterText.text = $"{currentNumberOfCollectedObjects}/{_construction.RequiredNumberOfItems}";

        if (currentNumberOfCollectedObjects >= _construction.RequiredNumberOfItems)
            _canvas.enabled = false;
    }
}
