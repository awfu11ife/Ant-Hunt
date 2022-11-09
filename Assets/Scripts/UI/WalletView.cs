using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyAmountText;

    private float _shortHand = 1000;
    private string[] _names = new[]
    {
        "",
        "K",
        "M",
        "B"
    };

    public void UpdateMoneyText(float currentMoneyAmount)
    {
        string outputValue;

        if (currentMoneyAmount != 0)
        {
            currentMoneyAmount = Mathf.Round((float)currentMoneyAmount);
            int i = 0;

            while (i < _names.Length && currentMoneyAmount >= _shortHand)
            {
                currentMoneyAmount /= _shortHand;
                i++;
            }

            outputValue = currentMoneyAmount.ToString("#.##") + _names[i];
        }
        else if(currentMoneyAmount == 0)
        {
            outputValue = currentMoneyAmount.ToString();
        }
        else
        {
            outputValue = "MAX";
        }

        _moneyAmountText.text = $"{outputValue}$";
    }
}
