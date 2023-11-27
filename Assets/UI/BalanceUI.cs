using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceDisplay;
    Balance balance;


    void Start()
    {
        //balanceDisplay = GetComponent<TextMeshPro>();
        if (balanceDisplay == null)
        {
            Debug.Log("balance is null");
        }


        balance = FindObjectOfType<Balance>();

        UpdateBalanceDisplay();
    }

    public void UpdateBalanceDisplay()
    {
        balanceDisplay.text = "$" + balance.CurrentBalance;
    }
}
