using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodPiecesContainer : MonoBehaviour
{
    private int _piecesCount;
    private bool _isAbleToEat = true;
    private List<CollectableObject> _pieces = new List<CollectableObject>();

    private UnityEvent<FoodPiecesContainer> _onPossibilityToEatChanged = new UnityEvent<FoodPiecesContainer>();

    public bool IsAbleToEat => _isAbleToEat;

    public event UnityAction<FoodPiecesContainer> OnPossibilityToEatChanged
    {
        add => _onPossibilityToEatChanged.AddListener(value);
        remove => _onPossibilityToEatChanged.RemoveListener(value);
    }

    private void Awake()
    {
        FindAllPieces();
    }

    private void OnEnable()
    {
        foreach (CollectableObject piece in _pieces)
        {
            piece.OnItemCollected += ChangeNumberOfPieces;
        }
    }

    private void OnDisable()
    {
        foreach (CollectableObject piece in _pieces)
        {
            piece.OnItemCollected -= ChangeNumberOfPieces;
        }
    }

    public CollectableObject ReturnNearestPiece(Vector3 collectorPosition)
    {
        CollectableObject nearestPiece = null;
        float distance = Mathf.Infinity;

        foreach (var piece in _pieces)
        {
            float currentDistance = (piece.transform.position - collectorPosition).sqrMagnitude;

            if (currentDistance < distance && piece.IsAbleToTake == true)
            {
                nearestPiece = piece;
                distance = currentDistance;

            }
        }

        if (nearestPiece != null)
            nearestPiece.DisablePart();

        return nearestPiece;
    }

    private void FindAllPieces()
    {
        _pieces.Clear();

        foreach (CollectableObject piece in transform.GetComponentsInChildren<CollectableObject>())
        {
            _pieces.Add(piece);
        }

        _piecesCount = _pieces.Count;
    }

    private void ChangeNumberOfPieces()
    {
        _piecesCount--;

        if (_piecesCount <= 0)
        {
            _isAbleToEat = false;
            _onPossibilityToEatChanged?.Invoke(this);
        }
    }
}
