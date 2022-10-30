using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodPiecesContainer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _eatParticle;

    private bool _isAbleToEat = true;
    private List<CollectableObject> _pieces = new List<CollectableObject>();

    private UnityEvent<CollectableObject> _onPieceRemoved = new UnityEvent<CollectableObject>();

    public bool IsAbleToEat => _isAbleToEat;

    public event UnityAction<CollectableObject> OnPieceRemoved
    {
        add => _onPieceRemoved.AddListener(value);
        remove => _onPieceRemoved.RemoveListener(value);
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
            piece.OnItemCollected += CreateEatParticle;
        }
    }

    private void OnDisable()
    {
        foreach (CollectableObject piece in _pieces)
        {
            piece.OnItemCollected -= ChangeNumberOfPieces;
            piece.OnItemCollected -= CreateEatParticle;
        }
    }

    public CollectableObject ReturnNearestPiece(Vector3 collectorPosition)
    {
        CollectableObject nearestPiece = null;
        float distance = Mathf.Infinity;

        foreach (var piece in _pieces)
        {
            float currentDistance = (piece.transform.position - collectorPosition).sqrMagnitude;

            if (currentDistance < distance && piece.IsAbleToTake == true && piece.isActiveAndEnabled == true)
            {
                nearestPiece = piece;
                distance = currentDistance;
            }
        }
        return nearestPiece;
    }

    private void FindAllPieces()
    {
        _pieces.Clear();

        foreach (CollectableObject piece in transform.GetComponentsInChildren<CollectableObject>())
        {
            _pieces.Add(piece);
        }
    }

    private void ChangeNumberOfPieces(CollectableObject piece)
    {
        _pieces.Remove(piece);

        if (_pieces.Count == 0)
        {
            _isAbleToEat = false;
            _onPieceRemoved?.Invoke(piece);
        }
    }

    private void CreateEatParticle(CollectableObject piece)
    {
        var particleMain = Instantiate(_eatParticle, piece.transform.position, Quaternion.identity).main;
        particleMain.startColor = piece.Color;
    }
}
