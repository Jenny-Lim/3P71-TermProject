using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    int value; // value of the piece
    bool isTaken; // if its taken
    bool isBlack; // if its our (Ais)
    int xPosition;
    int yPosition;
    string type;

    public Piece(int value, bool isTaken, bool isBlack; int xPosition; int yPosition)
    {
        this.value = value;
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosiiton = xPosition;
        this.yPosition = yPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
