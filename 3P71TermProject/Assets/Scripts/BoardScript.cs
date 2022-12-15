using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{

    private Piece[,] board = new Piece[8,8];

    // Start is called before the first frame update

    void Start()
    {
        // pogchamp
        //test line 2 

        //MakeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBoard() // bruh
    {

        // ai pieces

        board[0, 0] = new Piece(false, true, 0, 0, "rook");
        board[0, 1] = new Piece(false, true, 0, 1, "knight");
        board[0, 2] = new Piece(false, true, 0, 2, "bishop");
        board[0, 3] = new Piece(false, true, 0, 3, "queen");
        board[0, 4] = new Piece(false, true, 0, 4, "king");
        board[0, 2] = new Piece(false, true, 0, 2, "bishop");
        board[0, 1] = new Piece(false, true, 0, 1, "knight");
        board[0, 0] = new Piece(false, true, 0, 0, "rook");

        for (int i = 0; i < board.Length; i++)
        {
            board[1, i] = new Piece(false, true, 1, i, "pawn");
        }


        // player pieces

        for (int i = 0; i < board.Length; i++)
        {
            board[6, i] = new Piece(false, false, 6, i, "pawn");
        }

        board[7, 0] = new Piece(false, false, 7, 0, "rook");
        board[7, 1] = new Piece(false, false, 7, 1, "knight");
        board[7, 2] = new Piece(false, false, 7, 2, "bishop");
        board[7, 3] = new Piece(false, false, 7, 3, "queen");
        board[7, 4] = new Piece(false, false, 7, 4, "king");
        board[7, 2] = new Piece(false, false, 7, 2, "bishop");
        board[7, 1] = new Piece(false, false, 7, 1, "knight");
        board[7, 0] = new Piece(false, false, 7, 0, "rook");

    } // MakeBoard

    void MovePiece() // board can have reference to the pieces,
    {
        // get input from user = pieceselected

        // if the piece is a pawn and if board[xPosition + 1][yPosition + 1]!=null
        // pieceselected.canMove[xPosition + 1][yPosition + 1] = true;

        // get input from user = someOtherSpot -- chosen spot (compare with canMove, set new coordinates on pieceselected)
        //if board[someOtherSpot.xPosition, someOtherSpot.yPosition] != null
        // board[someOtherSpot.xPosition, someOtherSpot.yPosition].isTaken = true;

        //if (pieceSelected.isBlack && piece.Selected.type == "pawn" && pieceSelected.yPosition == board.Length-1){
        //pieceselected.promote()

        //if (!pieceSelected.isBlack && piece.Selected.type == "pawn" && pieceSelected.yPosition == 0){
        //pieceselected.promote()

        //go through all opposing pieces, and within the piece go through all moves
        //if king is in any index of canMove that == true of opposing piece
        //check

        //if spaces between king and rook is clear, king can move two spaces closer to rook and rook can jump 1 spot over king // castling
    } // MovePiece



    void BoardEval() // maybe we pick a better heuristic
    {
        int eval = 0;

        for(int i = 0; i<board.Length; i++)
        {
            for (int j = 0; j<board.Length; j++)
            {
                if (board[i, j].isBlack) // ai - maximizing score
                {
                    eval = eval + board[i, j].value;
                }
                if (!board[i, j].isBlack) // player - minimizing score
                {
                    eval = eval - board[i, j].value;
                }
            }
        }
    } // BoardEval
}
