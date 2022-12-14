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

        //makeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeBoard() // bruh
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

        board[7, 0] = new Piece(false, false, 0, 0, "rook");
        board[7, 1] = new Piece(false, false, 0, 1, "knight");
        board[7, 2] = new Piece(false, false, 0, 2, "bishop");
        board[7, 3] = new Piece(false, false, 0, 3, "queen");
        board[7, 4] = new Piece(false, false, 0, 4, "king");
        board[7, 2] = new Piece(false, false, 0, 2, "bishop");
        board[7, 1] = new Piece(false, false, 0, 1, "knight");
        board[7, 0] = new Piece(false, false, 0, 0, "rook");

    }
}
