using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{

    private Piece[,] board = new Piece[8,8];

        // initializing AI pieces

    //Piece pawn = new Piece(100, false, true, i, j, pawn);
    //Piece rook = new Piece(500, false, true, i, j, rook);
    //Piece knight = new Piece(300, false, true, i, j, knight);
    //Piece bishop = new Piece(300, false, true, i, j, bishop);
    //Piece queen = new Piece(900, false, true, i, j, queen);
    //Piece king = new Piece(int.MaxValue, false, true, i, j, king);

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

    void makeBoard() // have a list of pieces to start with ?
    {
        for (int i = 0; i < board.Length; i++)
        {
            //if (piece.type = "pawn")
            //{
            //    if (piece.isBlack)
            //    {
            //        board[1, i] = piece;
            //    }
            //    if (!piece.isBlack)
            //    {
            //        board[6, i] = piece;
            //    }
            //}

            //etc
        }
    }
}
