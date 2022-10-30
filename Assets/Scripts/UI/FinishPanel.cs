using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private float _animationDuration;

    private WaitForSeconds _enableDelay;

    private void Start()
    {
        _enableDelay = new WaitForSeconds(_animationDuration);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), _animationDuration);
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, _animationDuration);
        StartCoroutine(EnableDelay());
    }

    private IEnumerator EnableDelay()
    {
        yield return _enableDelay;
        gameObject.SetActive(false);
    }
}
