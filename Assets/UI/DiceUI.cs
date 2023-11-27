using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diceDisplay;

    Movement movement;

    void Start() 
    {
        movement = FindObjectOfType<Movement>();

        UpdateDiceDisplay();
    }

    public void UpdateDiceDisplay()
    {
        if (movement.Dice != 0)
        {
            diceDisplay.text = "Dice: " + movement.Dice;
        }
        else
        {
            diceDisplay.text = "Press 'r' to Roll";
        }
        
    }

}
