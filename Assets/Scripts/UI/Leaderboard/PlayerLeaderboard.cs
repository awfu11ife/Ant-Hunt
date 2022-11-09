using UnityEngine;
using TMPro;

public class PlayerLeaderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _place;
    [SerializeField] private TMP_Text _score;

    public TMP_Text Name => _name;
    public TMP_Text Place => _place;
    public TMP_Text Score => _score;
}
