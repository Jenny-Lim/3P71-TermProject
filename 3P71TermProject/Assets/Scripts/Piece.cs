using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum pieceType
//{
//    pawn,
//    knight,
//    bishop,
//    rook,
//    queen,
//    king
//}

public class Piece : MonoBehaviour
{
    public int value; // value of the piece
    public bool isTaken; // if its taken

    public bool isBlack; // if its our (Ais)

    // set these in board script
    public int xPosition;
    public int yPosition;

    public string type;


    public Piece(bool isTaken, bool isBlack, int xPosition, int yPosition, string type)
    {
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.type = type;
    }

    // doesnt account for out of bounds yet
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
            // canMove[xPosition,yPosition+2] = true;

            if (yPosition < canMove.Length - 1)
            {
                canMove[xPosition, yPosition + 1] = true;
            }

            //capturing is toggled on when theres a piece in range (see below), have this in board script

            //if (Board.board[xPosition + 1][yPosition + 1]!=null)
            // then capture == true

            //if (capture)
            //{
                //canMove[xPosition + 1][yPosition + 1] = true;
            //}
        }

        if (type == "knight") // knights move in an 'L'
        {
            if (xPosition < canMove.Length - 1 && yPosition < canMove.Length)
            {
                canMove[xPosition + 2, yPosition + 1] = true;
            }

            if (xPosition < canMove.Length && yPosition < canMove.Length - 1) {
                canMove[xPosition + 1,yPosition + 2] = true;
                }

            //if(xPosition > ) i left off here
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

    void getValue()
    {
        if (type == "pawn")
        {
            value = 100;
        }
        if (type == "knight")
        {
            value = 300;
                }
        if (type == "bishop")
        {
            value = 300;
                }
        if (type == "rook")
        {
            value = 500;
                }
        if (type == "queen")
        {
            value = 900;
                }
        if (type == "king")
        {
            value = 0; // no point in assigning val to king ?
                }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if type==king && isTaken
        //then checkmate + game over
    }
}
