using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{

    public Piece[,] board = new Piece[8,8];

    private Ray cameraRay;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private MiniMax miniMaxScript;

    private RaycastHit hit;

    private int playerYPos;
    private int playerXPos;
    private bool pieceChosen = false;


    // Start is called before the first frame update

    void Start()
    {
        // pogchamp
        //test line 2 

        MakeBoard();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition; 
        Vector3 worldPos=Camera.main.ScreenToWorldPoint(mousePos);  
        //Debug.Log(worldPos);   
        cameraRay = cam.ScreenPointToRay(mousePos); 

        if (Physics.Raycast (cameraRay, out hit)) 
        {
            if (hit.collider) 
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if(hit.collider.tag == "PlayerPiece")
                    {
                    Debug.Log(hit.collider.gameObject);
                    Debug.Log("XPOS: "+hit.collider.gameObject.transform.position.x);
                    playerXPos = (int)hit.collider.gameObject.transform.position.x;
                    Debug.Log("YPOS: "+hit.collider.gameObject.transform.position.y);
                    playerYPos = -1 * (int)hit.collider.gameObject.transform.position.y;
                    pieceChosen = true;
                    }
                    else if(hit.collider.tag != "PlayerPiece" && pieceChosen)
                    {
                        int moveX = (int)hit.collider.gameObject.transform.position.x;
                        int moveY = -1 * (int)hit.collider.gameObject.transform.position.y;
                        Debug.Log("MOVE TO X: "+moveX+" Y: "+moveY);
                        board[moveY, moveX].updatePiece(board[playerYPos,playerXPos].isTaken, board[playerYPos,playerXPos].isBlack, moveY, moveX, board[playerYPos,playerXPos].type);
                        board[playerYPos, playerXPos].updatePiece(false, false, playerYPos, playerXPos, "Empty");
                        pieceChosen = false;
                        miniMaxScript.updateBoard(true);
                    }

                }
            }
        }

    }

    void MakeBoard() // bruh
    {

        // ai pieces - black

        for(int i = 0;i<8;i++)
        {
            for(int j = 0;j<8;j++)
            {
                board[i,j] = new Piece(false, false, i, j, "empty");
            }
        }

        board[0, 0] = new Piece(false, true, 0, 0, "rook");
        board[0, 1] = new Piece(false, true, 0, 1, "knight");
        board[0, 2] = new Piece(false, true, 0, 2, "bishop");
        board[0, 3] = new Piece(false, true, 0, 3, "queen");
        board[0, 4] = new Piece(false, true, 0, 4, "king");
        board[0, 5] = new Piece(false, true, 0, 5, "bishop");
        board[0, 6] = new Piece(false, true, 0, 6, "knight");
        board[0, 7] = new Piece(false, true, 0, 7, "rook");

        for (int i = 0; i < 8; i++)
        {
            board[1, i] = new Piece(false, true, 1, i, "pawn");
        }


        // player pieces

        for (int i = 0; i < 8; i++)
        {
            board[6, i] = new Piece(false, false, 6, i, "pawn");
        }

        board[7, 0] = new Piece(false, false, 7, 0, "rook");
        board[7, 1] = new Piece(false, false, 7, 1, "knight");
        board[7, 2] = new Piece(false, false, 7, 2, "bishop");
        board[7, 3] = new Piece(false, false, 7, 3, "queen");
        board[7, 4] = new Piece(false, false, 7, 4, "king");
        board[7, 5] = new Piece(false, false, 7, 5, "bishop");
        board[7, 6] = new Piece(false, false, 7, 6, "knight");
        board[7, 7] = new Piece(false, false, 7, 7, "rook");

    }// MakeBoard

    void MovePiece() // board can have reference to the pieces,
    {
        // get input from user = pieceselected

        // get input from user = someOtherSpot -- chosen spot (compare with canMove, set new coordinates on pieceselected)
        //if board[someOtherSpot.xPosition, someOtherSpot.yPosition] != null
        // board[someOtherSpot.xPosition, someOtherSpot.yPosition].isTaken = true;

        //if (pieceSelected.isBlack && piece.Selected.type == "pawn" && pieceSelected.yPosition == board.Length-1){
        //get input
        //pieceselected.promote(input)

        //if (!pieceSelected.isBlack && piece.Selected.type == "pawn" && pieceSelected.yPosition == 0){
        //get input
        //pieceselected.promote(input)

        //go through all opposing pieces, and within the piece go through all moves
        //if king is in any index of canMove that == true of opposing piece
        //check

        //if spaces between king and rook is clear, king can move two spaces closer to rook and rook can jump 1 spot over king // castling
    } // MovePiece




    int BoardEval() // maybe we pick a better heuristic
    {
        int eval = 0;

        for(int i = 0; i<8; i++)
        {
            for (int j = 0; j<8; j++)
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
        return eval;
    } // BoardEval
}
