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

    public bool[,] canMove;

    bool pawnHasMoved = false;


    public Piece(bool isTaken, bool isBlack, int xPosition, int yPosition, string type)
    {
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.type = type;
        GetValue();
    }


    public bool[,] MoveCheck() // must also check if there's a piece on the board
    {
        bool[,] canMove = new bool[8, 8];
    for (int i = 0; i<8; i++)
        {
            for (int j = 0; j<8; j++) {
                canMove[i,j] = false;
            }
        }

        if (type == "pawn") // pawns only move forward one tile at a time
        {

            // check if its the pawn's first move
            if (!pawnHasMoved)
            {
                canMove[xPosition+2, yPosition] = true;
            }

            if (yPosition+1 < 8)
            {
                canMove[xPosition+1, yPosition] = true;
            }

            pawnHasMoved = true;

        } // pawn


        if (type == "knight") // knights move in an 'L'
        {
            if (xPosition+2 < 8 && yPosition+1 < 8)
            {
                canMove[xPosition + 2, yPosition + 1] = true;
            }

            if (xPosition+1 < 8 && yPosition+2 < 8) {
                canMove[xPosition + 1,yPosition + 2] = true;
                }

            if (xPosition - 2 > -1 && yPosition - 1 > -1)
            {
                canMove[xPosition - 2, yPosition - 1] = true;
            }
            if (xPosition - 1 > -1 && yPosition - 2 > -1)
            {
                canMove[xPosition - 1, yPosition - 2] = true;
            }
        } // knight


        if (type == "bishop") // bishops move in diagonals
        {
            for (int i = 0; i < 8; i++) {
                if (xPosition + i < 8 && yPosition + i < 8)
                {
                    canMove[xPosition + i, yPosition + i] = true;
                }
                if (xPosition - i > -1 && yPosition - i > -1)
                {
                    canMove[xPosition - i, yPosition - i] = true;
                }
            }
        } // bishop


        if (type == "rook") // rooks move in a cross
        {
            for (int i = 0; i < 8; i++)
            {
                if (yPosition + i < 8)
                {
                    canMove[xPosition, yPosition + i] = true;
                }
                if (xPosition + i < 8) {
                    canMove[xPosition + i, yPosition] = true;
                }

                if (yPosition - i > -1) {
                    canMove[xPosition, yPosition - i] = true;
                }
                if (xPosition - i > -1) {
                    canMove[xPosition - i, yPosition] = true;
                }
            }
        } // rook


        if (type == "queen") // can probably condense this
        {
            // does what king does
            if (yPosition + 1 < 8)
            {
                canMove[xPosition, yPosition + 1] = true;
            }
            if (xPosition + 1 < 8)
            {
                canMove[xPosition + 1, yPosition] = true;
            }
            if (xPosition + 1 < 8 && yPosition + 1 < 8)
            {
                canMove[xPosition + 1, yPosition + 1] = true;
            }
            if (yPosition - 1 > -1)
            {
                canMove[xPosition, yPosition - 1] = true;
            }
            if (xPosition - 1 > -1)
            {
                canMove[xPosition - 1, yPosition] = true;
            }
            if (xPosition - 1 > -1 && yPosition - 1 > -1)
            {
                canMove[xPosition - 1, yPosition - 1] = true;
            }


            for (int i = 0; i < 8; i++)
            {
                if (xPosition + i < 8 && yPosition + i < 8)
                {
                    canMove[xPosition + i, yPosition + i] = true;
                }
                if (xPosition - i > -1 && yPosition - i > -1)
                {
                    canMove[xPosition - i, yPosition - i] = true;
                } // also does what bishop does


                if (yPosition + i < 8)
                {
                    canMove[xPosition, yPosition + i] = true;
                }
                if (xPosition + i < 8)
                {
                    canMove[xPosition + i, yPosition] = true;
                }

                if (yPosition - i > -1)
                {
                    canMove[xPosition, yPosition - i] = true;
                }
                if (xPosition - i > -1)
                {
                    canMove[xPosition - i, yPosition] = true;
                } // alsoalso does what rook does
            }
        } // queen


        if (type == "king") // king can only move around them
        {
            Debug.Log("king: YPOS: "+xPosition+ " XPOS: "+yPosition);
            if (yPosition + 1 < 8) {
                canMove[xPosition, yPosition + 1] = true;
            }
            if (xPosition + 1 < 8)
            {
                canMove[xPosition + 1,yPosition] = true;
            }
            if (xPosition + 1 < 8 && yPosition + 1 < 8)
            {
                canMove[xPosition + 1,yPosition + 1] = true;
            }
            if (yPosition - 1 > -1)
            {
                canMove[xPosition, yPosition - 1] = true;
            }
            if (xPosition - 1 > -1)
            {
                canMove[xPosition - 1, yPosition] = true;
            }
            if (xPosition - 1 > -1 && yPosition - 1 > -1)
            {
                canMove[xPosition - 1, yPosition - 1] = true;
            }
        } // king


        return canMove;
    } // MoveCheck


    void GetValue()
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
    } // GetValue

    void Promote()
    {
        //get user to choose new piece type
        //type = "newType";
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
