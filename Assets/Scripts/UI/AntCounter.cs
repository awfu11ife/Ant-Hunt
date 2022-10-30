using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _antCountText;

    public void UpdateAntCounterText(int currentAntAmount, int maxAntAmount)
    {
        _antCountText.text = $"{currentAntAmount}/{maxAntAmount}";
    }
}
