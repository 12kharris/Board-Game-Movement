using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
   [SerializeField] float[] squareXPositions;
   [SerializeField] float[] squareZPositions;
   [SerializeField] [Range(0f,5f)] float speed = 0.1f;
   [SerializeField] Vector3 currentPosition;
   [SerializeField] int squareNumber;
   //[SerializeField] int startingIndex = 0;
   [SerializeField] Vector3 startingPosition;
   Vector3 endingLookAt;
   [SerializeField] int dice;
   bool needToRoll;

   Balance balance;
   DiceUI diceUI;
   [SerializeField] public SquarePrice[] prices;
   public int Dice {get {return dice;}}
   

    // Start is called before the first frame update
    void Start()
    {

        //startingPosition = new Vector3 (squareXPositions[0], 1, squareZPositions[0]);
        //transform.position = startingPosition;
        balance = FindObjectOfType<Balance>();
        diceUI = FindObjectOfType<DiceUI>();
        prices = FindObjectsOfType<SquarePrice>();

        squareNumber = 0;
        dice = 0;
        endingLookAt = new Vector3 (0,0.5f,-999999999);
        needToRoll = true;

        squareXPositions = new float[] {GameObject.Find("Square 0 Variant").transform.position.x,
        GameObject.Find("Square 1").transform.position.x, GameObject.Find("Square 2").transform.position.x, 
        GameObject.Find("Square 3").transform.position.x};
        
        squareZPositions = new float[] {GameObject.Find("Square 0 Variant").transform.position.z,
        GameObject.Find("Square 1").transform.position.z, GameObject.Find("Square 2").transform.position.z, 
        GameObject.Find("Square 3").transform.position.z};

    }

    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        currentPosition = transform.position;
        Vector3 lookAtAfterRoll = new Vector3 (transform.position.x, 0.5f, transform.position.z);
        if (dice == 0) 
        {
            DiceRoller();
            needToRoll = false;
            transform.LookAt(endingLookAt);
        }
        else
        {
            DiceMovementLogic();
        }
        
    }

    void DiceRoller()
    {
        if (needToRoll == true)
        {
            diceUI.UpdateDiceDisplay();
        }
        
        if (Input.GetKeyDown("r"))
        {
            dice = Random.Range(1,7);
            diceUI.UpdateDiceDisplay();
        }

        
    }

    void DiceMovementLogic()
    {
        if (Input.GetKeyDown("d"))
        {
            squareNumber += 1;
            BoardMovementLogic();
            dice -= 1;
            
            if (dice == 0)
            {
                needToRoll = true;
                if (balance == null)
                {
                    return;
                }
                balance.Withdraw(prices[squareNumber].SquareCharge); //Withdraw the amount on the square
            }   
            currentPosition = transform.position;
            //Debug.Log("Dice = " + dice);
            diceUI.UpdateDiceDisplay();
        }
    }

    void BoardMovementLogic()
    {
        if (squareNumber - squareXPositions.Length == 0)
        {
            squareNumber = 0; // Allows for looping the board by resetting squareNumber
        }
        
        StartCoroutine(AnimateToNewPosition());
        
    }

    IEnumerator AnimateToNewPosition()
        {
            Vector3 newPosition = new Vector3(squareXPositions[squareNumber],
                transform.position.y, squareZPositions[squareNumber]);

            transform.LookAt(newPosition);
            float travelPercent = 0f;
            
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(currentPosition, newPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
}
