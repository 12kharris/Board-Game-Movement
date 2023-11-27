using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balance : MonoBehaviour
{
   [SerializeField] int startingBalance;
   [SerializeField] int currentBalance;

   BalanceUI balanceUI;

   public int CurrentBalance {get {return currentBalance;} }

   void Awake() 
    {
        currentBalance = startingBalance;
    }

    void Start() 
    {
        balanceUI = FindObjectOfType<BalanceUI>();
    }
   
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(Mathf.RoundToInt(amount));
        balanceUI.UpdateBalanceDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(Mathf.RoundToInt(amount));
        balanceUI.UpdateBalanceDisplay();

        if (currentBalance <= 0)
        {
            //ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
