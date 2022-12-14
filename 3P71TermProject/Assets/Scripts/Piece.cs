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

    public Piece(int value, bool isTaken, bool isBlack, int xPosition, int yPosition)
    {
        this.value = value;
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }

    public bool[,] MoveCheck() // must also check if there's a piece on the board, need reference to the actual board
    {
        bool[,] canMove = new bool[8, 8];
    for (int i = 0; i<canMove.Length; i++)
        {
            for (int j = 0; j<canMove.Length; j++) {
                canMove[i,j] = false;
            }
        }

        if (type == "pawn") // pawns only move forward one tile at a time
        {

            // some check if its the pawn's first move

            canMove[xPosition,yPosition + 1] = true;

            //capturing is toggled on when theres a piece in range
            //if (capture)
            //{
                //canMove[xPosition + 1][yPosition + 1] = true;
            //}
        }

        if (type == "knight") // knights move in an 'L'
        {
            canMove[xPosition + 2,yPosition + 1] = true;
            canMove[xPosition + 1,yPosition + 2] = true;

            canMove[xPosition - 2,yPosition - 1] = true;
            canMove[xPosition - 1,yPosition - 2] = true;
        }
        if (type == "bishop") // bishops move in diagonals
        {
            for (int i = 0; i < canMove.Length; i++) {
                canMove[xPosition + i,yPosition + i] = true;
                canMove[xPosition - i,yPosition - i] = true;
            }
        }
        if (type == "rook") // rooks move in a cross
        {
            for (int i = 0; i < canMove.Length; i++)
            {
                canMove[xPosition,yPosition + i] = true;
                canMove[xPosition + i,yPosition] = true;

                canMove[xPosition,yPosition - i] = true;
                canMove[xPosition - i,yPosition] = true;
            }
        }
        if (type == "queen") // can probably condense this
        {
            // does what king does
            canMove[xPosition,yPosition + 1] = true;
            canMove[xPosition + 1,yPosition] = true;
            canMove[xPosition + 1,yPosition + 1] = true;

            canMove[xPosition,yPosition - 1] = true;
            canMove[xPosition - 1,yPosition] = true;
            canMove[xPosition - 1,yPosition - 1] = true;

            for (int i = 0; i < canMove.Length; i++)
            {
                canMove[xPosition + i,yPosition + i] = true;
                canMove[xPosition - i,yPosition - i] = true; // also does what bishop does

                canMove[xPosition,yPosition + i] = true;
                canMove[xPosition + i,yPosition] = true;

                canMove[xPosition,yPosition - i] = true;
                canMove[xPosition - i,yPosition] = true; // alsoalso does what rook does
            }
        }
        if (type == "king") // king can only move around them
        {
            canMove[xPosition,yPosition + 1] = true;
            canMove[xPosition + 1,yPosition] = true;
            canMove[xPosition + 1,yPosition + 1] = true;

            canMove[xPosition,yPosition - 1] = true;
            canMove[xPosition - 1,yPosition] = true;
            canMove[xPosition - 1,yPosition - 1] = true;
        }
        return canMove;
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
