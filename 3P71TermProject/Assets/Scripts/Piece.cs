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


/**
 * This is a Chess piece.
 *
 * @author Patrick Leonard (7008113), Jenny Lim (6978118)
 * @version 1.0 (2022-19-12)
 */
public class Piece : MonoBehaviour
{
    public int value; // value of the piece
    public bool isTaken; // if its taken

    public bool isBlack; // if its our (Ais)
    public bool isWhite;

    // set these in board script
    public int xPosition;
    public int yPosition;

    public string type;

    public bool[,] canMove;

    public bool pawnHasMoved = false;

    /**
     * This constructor is a Chess piece.
     */
    public Piece(bool isTaken, bool isBlack, bool isWhite, int yPosition, int xPosition, string type)
    {
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.type = type;
        this.isWhite = isWhite;
        GetValue();
    }

    /**
     * Updates piece.
     *
     * @param isTaken If the piece is taken.
     * @param isBlack If the piece is black.
     * @param isWhite If the piece is white.
     * @param yPosition The piece's y coordinate on the board.
     * @param xPosition The piece's x coordinate on the board.
     * @param type The type of piece.
     */
    public void updatePiece(bool isTaken, bool isBlack, bool isWhite, int yPosition, int xPosition, string type)
    {
        this.isTaken = isTaken;
        this.isBlack = isBlack;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.type = type;
        this.isWhite = isWhite;
        GetValue();
    } // updatePiece


    //   public bool[,] BlackMoveCheck() // stop setting canmove to be true once an enemy piece is in the way --  if a board spot isnt null, then you can't move to the spots above it -- need reference to board, or move this into board and have pieces refer to this
    //   {
    //       bool[,] canMove = new bool[8, 8];
    //   for (int i = 0; i<8; i++)
    //       {
    //           for (int j = 0; j<8; j++) {
    //               canMove[i,j] = false;
    //           }
    //       }

    //       if (type == "pawn") // pawns only move forward one tile at a time
    //       {

    //           // check if its the pawn's first move
    //           if (yPosition == 6)
    //           {
    //               canMove[yPosition-2, xPosition] = true;
    //           }

    //           if (yPosition-1 > -1)
    //           {
    //               canMove[yPosition-1, xPosition] = true;
    //           }

    //           pawnHasMoved = true;

    //       } // pawn


    //       if (type == "knight") // knights move in an 'L'
    //       {
    //           if (xPosition+2 < 8 && yPosition+1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition + 2] = true;
    //           }

    //           if (xPosition+1 < 8 && yPosition+2 < 8) 
    //           {
    //               canMove[yPosition + 2,xPosition + 1] = true;
    //           }

    //           if (xPosition - 2 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 2] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 2 > -1)
    //           {
    //               canMove[yPosition - 2, xPosition - 1] = true;
    //           }
    //       } // knight


    //       if (type == "bishop") // bishops move in diagonals
    //       {
    //           for (int i = 0; i < 8; i++) {
    //               if (xPosition + i < 8 && yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition + i] = true;
    //               }
    //               if (xPosition - i > -1 && yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition - i] = true;
    //               }
    //           }
    //       } // bishop


    //       if (type == "rook") // rooks move in a cross
    //       {
    //           for (int i = 0; i < 8; i++)
    //           {
    //               if (yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition] = true;
    //               }
    //               if (xPosition + i < 8) {
    //                   canMove[yPosition, xPosition + i] = true;
    //               }

    //               if (yPosition - i > -1) {
    //                   canMove[yPosition - i, xPosition] = true;
    //               }
    //               if (xPosition - i > -1) {
    //                   canMove[yPosition, xPosition - i] = true;
    //               }
    //           }
    //       } // rook


    //       if (type == "queen") // can probably condense this
    //       {
    //           // does what king does
    //           if (yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition] = true;
    //           }
    //           if (xPosition + 1 < 8)
    //           {
    //               canMove[yPosition, xPosition + 1] = true;
    //           }
    //           if (xPosition + 1 < 8 && yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition + 1] = true;
    //           }
    //           if (yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition] = true;
    //           }
    //           if (xPosition - 1 > -1)
    //           {
    //               canMove[yPosition, xPosition-1] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 1] = true;
    //           }


    //           for (int i = 0; i < 8; i++)
    //           {
    //               if (xPosition + i < 8 && yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition + i] = true;
    //               }
    //               if (xPosition - i > -1 && yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition - i] = true;
    //               } // also does what bishop does


    //               if (yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition] = true;
    //               }
    //               if (xPosition + i < 8)
    //               {
    //                   canMove[yPosition, xPosition + i] = true;
    //               }

    //               if (yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition] = true;
    //               }
    //               if (xPosition - i > -1)
    //               {
    //                   canMove[yPosition, xPosition - i] = true;
    //               } // alsoalso does what rook does
    //           }
    //       } // queen


    //       if (type == "king") // king can only move around them
    //       {
    //           Debug.Log("king: YPOS: "+xPosition+ " XPOS: "+yPosition);
    //           if (yPosition + 1 < 8) {
    //               canMove[yPosition + 1, xPosition] = true;
    //           }
    //           if (xPosition + 1 < 8)
    //           {
    //               canMove[yPosition,xPosition + 1] = true;
    //           }
    //           if (xPosition + 1 < 8 && yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1,xPosition + 1] = true;
    //           }
    //           if (yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition] = true;
    //           }
    //           if (xPosition - 1 > -1)
    //           {
    //               canMove[yPosition, xPosition - 1] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 1] = true;
    //           }
    //       } // king

    //       // 2d for loop for every spot on the board
    //       // if board spot isnt null
    //       // for loop through yPos' for that spot and above
    //       // canMove = false;

    //       return canMove;
    //   } // MoveCheck

    //   public bool[,] WhiteMoveCheck()
    //{
    //       bool[,] canMove = new bool[8, 8];
    //   for (int i = 0; i<8; i++)
    //       {
    //           for (int j = 0; j<8; j++) {
    //               canMove[i,j] = false;
    //           }
    //       }

    //       if (type == "pawn") // pawns only move forward one tile at a time
    //       {

    //           // check if its the pawn's first move
    //           if (yPosition == 1)
    //           {
    //               canMove[yPosition+2, xPosition] = true;
    //           }

    //           if (yPosition+1 < 8)
    //           {
    //               canMove[yPosition+1, xPosition] = true;
    //           }

    //           pawnHasMoved = true;

    //       } // pawn


    //       if (type == "knight") // knights move in an 'L'
    //       {
    //           if (xPosition+2 < 8 && yPosition+1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition + 2] = true;
    //           }

    //           if (xPosition+1 < 8 && yPosition+2 < 8) 
    //           {
    //               canMove[yPosition + 2,xPosition + 1] = true;
    //           }

    //           if (xPosition - 2 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 2] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 2 > -1)
    //           {
    //               canMove[yPosition - 2, xPosition - 1] = true;
    //           }
    //       } // knight


    //       if (type == "bishop") // bishops move in diagonals
    //       {
    //           for (int i = 0; i < 8; i++) {
    //               if (xPosition + i < 8 && yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition + i] = true;
    //               }
    //               if (xPosition - i > -1 && yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition - i] = true;
    //               }
    //           }
    //       } // bishop


    //       if (type == "rook") // rooks move in a cross
    //       {
    //           for (int i = 0; i < 8; i++)
    //           {
    //               if (yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition] = true;
    //               }
    //               if (xPosition + i < 8) {
    //                   canMove[yPosition, xPosition + i] = true;
    //               }

    //               if (yPosition - i > -1) {
    //                   canMove[yPosition - i, xPosition] = true;
    //               }
    //               if (xPosition - i > -1) {
    //                   canMove[yPosition, xPosition - i] = true;
    //               }
    //           }
    //       } // rook


    //       if (type == "queen") // can probably condense this
    //       {
    //           // does what king does
    //           if (yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition] = true;
    //           }
    //           if (xPosition + 1 < 8)
    //           {
    //               canMove[yPosition, xPosition + 1] = true;
    //           }
    //           if (xPosition + 1 < 8 && yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1, xPosition + 1] = true;
    //           }
    //           if (yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition] = true;
    //           }
    //           if (xPosition - 1 > -1)
    //           {
    //               canMove[yPosition, xPosition-1] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 1] = true;
    //           }


    //           for (int i = 0; i < 8; i++)
    //           {
    //               if (xPosition + i < 8 && yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition + i] = true;
    //               }
    //               if (xPosition - i > -1 && yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition - i] = true;
    //               } // also does what bishop does


    //               if (yPosition + i < 8)
    //               {
    //                   canMove[yPosition + i, xPosition] = true;
    //               }
    //               if (xPosition + i < 8)
    //               {
    //                   canMove[yPosition, xPosition + i] = true;
    //               }

    //               if (yPosition - i > -1)
    //               {
    //                   canMove[yPosition - i, xPosition] = true;
    //               }
    //               if (xPosition - i > -1)
    //               {
    //                   canMove[yPosition, xPosition - i] = true;
    //               } // alsoalso does what rook does
    //           }
    //       } // queen


    //       if (type == "king") // king can only move around them
    //       {
    //           Debug.Log("king: YPOS: "+xPosition+ " XPOS: "+yPosition);
    //           if (yPosition + 1 < 8) {
    //               canMove[yPosition + 1, xPosition] = true;
    //           }
    //           if (xPosition + 1 < 8)
    //           {
    //               canMove[yPosition,xPosition + 1] = true;
    //           }
    //           if (xPosition + 1 < 8 && yPosition + 1 < 8)
    //           {
    //               canMove[yPosition + 1,xPosition + 1] = true;
    //           }
    //           if (yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition] = true;
    //           }
    //           if (xPosition - 1 > -1)
    //           {
    //               canMove[yPosition, xPosition - 1] = true;
    //           }
    //           if (xPosition - 1 > -1 && yPosition - 1 > -1)
    //           {
    //               canMove[yPosition - 1, xPosition - 1] = true;
    //           }
    //       } // king


    //       return canMove;
    //   } // MoveCheck


    /**
     * Assigns value to the piece based on what type it is.
     */
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
            value = 1800; // no point in assigning val to king ? -- wait actually it does matter, because pieces do want to aim for the king
                }
    } // GetValue


    /**
     * Promotes piece.
     */
    public void Promote()
    {
        type = DropDownHandler.newType;
    } // Promote

    

    // Update is called once per frame
    void Update()
    {
        if (isTaken && type == "king")
        {
            // disable the piece -- accounted for

                if (isBlack)
                {
                    // end the game
                    Time.timeScale = 0;
                    Debug.Log("You win!");
                    GameObject winScreen;
                    winScreen = GameObject.FindWithTag("VictoryScreen");
                    winScreen.SetActive(true);
                }
                else
                {
                    // end the game
                    Time.timeScale = 0;
                    Debug.Log("You lost!");
                    GameObject loseScreen;
                    loseScreen = GameObject.FindWithTag("LostScreen");
                    loseScreen.SetActive(true);

                }
                //then checkmate + game over
        }
    } // Update

} // Piece
