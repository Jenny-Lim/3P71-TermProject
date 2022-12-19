using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This is a Chess board.
 *
 * @author Patrick Leonard (7008113), Jenny Lim (6978118)
 * @version 1.0 (2022-19-12)
 */
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

    private bool[,] playerMoves = new bool[8,8];

    [SerializeField]
    public GameObject dropDownMenu;

        private static BoardScript instance;

    public static BoardScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoardScript>();
            }

            return instance;

        }
        set {instance = value;}
    } // Instance


    // Start is called before the first frame update
    void Start()
    {

        MakeBoard();
        dropDownMenu.SetActive(false);
    } // Start

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
//                    Debug.Log("XPOS: "+hit.collider.gameObject.transform.position.x);
                    playerXPos = (int)hit.collider.gameObject.transform.position.x;
         //           Debug.Log("YPOS: "+hit.collider.gameObject.transform.position.y);
                    playerYPos = -1 * (int)hit.collider.gameObject.transform.position.y;
                    playerMoves = MiniMax.WhiteMoveCheck(board, board[playerYPos, playerXPos]);
                    pieceChosen = true;
                    }
                    else if(hit.collider.tag != "PlayerPiece" && pieceChosen)
                    {
                        int moveX = (int)hit.collider.gameObject.transform.position.x;
                        int moveY = -1 * (int)hit.collider.gameObject.transform.position.y;
                        Debug.Log(moveX+" "+moveY+" "+playerMoves[moveY,moveX]);
                        if(playerMoves[moveY,moveX])//if bool matrix spot is true
                        {
                            Debug.Log("MOVE TO X: "+moveX+" Y: "+moveY);
                            board[moveY, moveX].updatePiece(board[playerYPos,playerXPos].isTaken, board[playerYPos,playerXPos].isBlack,board[playerYPos,playerXPos].isWhite, moveY, moveX, board[playerYPos,playerXPos].type);
                            board[playerYPos, playerXPos].updatePiece(true, false,false, playerYPos, playerXPos, "Empty");

                            
                                if (!board[moveY, moveX].isBlack && board[moveY, moveX].type == "pawn" && board[moveY, moveX].yPosition == 0)
                                {
                                dropDownMenu.SetActive(true);
                                    board[moveY, moveX].Promote();
                                dropDownMenu.SetActive(false);
                                }

                            pieceChosen = false;
                            miniMaxScript.updateBoard(true);
                            miniMaxScript.AITurn();
                        }


                    }

                }
            }
        }


        CheckCheck();

    } // Update


    /**
     * Initialises the Chess board in code.
     */
    void MakeBoard()
    {

        // ai pieces - black

        for(int i = 0;i<8;i++)
        {
            for(int j = 0;j<8;j++)
            {
                board[i,j] = new Piece(false, false,false, i, j, "empty");
            }
        }

        board[0, 0] = new Piece(false, true, false, 0, 0, "rook");
        board[0, 1] = new Piece(false, true, false, 0, 1, "knight");
        board[0, 2] = new Piece(false, true, false, 0, 2, "bishop");
        board[0, 3] = new Piece(false, true, false, 0, 3, "queen");
        board[0, 4] = new Piece(false, true, false, 0, 4, "king");
        board[0, 5] = new Piece(false, true, false, 0, 5, "bishop");
        board[0, 6] = new Piece(false, true, false, 0, 6, "knight");
        board[0, 7] = new Piece(false, true, false, 0, 7, "rook");

        for (int i = 0; i < 8; i++)
        {
            board[1, i] = new Piece(false, true, false, 1, i, "pawn");
        }


        // player pieces

        for (int i = 0; i < 8; i++)
        {
            board[6, i] = new Piece(false, false, true, 6, i, "pawn");
        }

        board[7, 0] = new Piece(false, false, true, 7, 0, "rook");
        board[7, 1] = new Piece(false, false, true, 7, 1, "knight");
        board[7, 2] = new Piece(false, false, true, 7, 2, "bishop");
        board[7, 3] = new Piece(false, false, true, 7, 3, "queen");
        board[7, 4] = new Piece(false, false, true, 7, 4, "king");
        board[7, 5] = new Piece(false, false, true, 7, 5, "bishop");
        board[7, 6] = new Piece(false, false, true, 7, 6, "knight");
        board[7, 7] = new Piece(false, false, true, 7, 7, "rook");

    } // MakeBoard


    /**
     * Checks if either player is in check.
     */
    void CheckCheck()
    {
        //go through all opposing pieces, and within the piece go through all moves
        //if king is in any index of canMove that == true of opposing piece
        //check

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j].isBlack) // for ai
                {
                    bool[,] canMove = MiniMax.BlackMoveCheck(board, board[i, j]);
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            if (canMove[k, l]==true && !board[k,l].isBlack && board[k,l].type == "king")
                            {
                                // check
                                Debug.Log("Check.");
                            }
                        }
                    }
                } // ai


                if (!board[i, j].isBlack) // for player
                {
                    bool[,] canMove = MiniMax.WhiteMoveCheck(board, board[i, j]);
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            if (canMove[k, l] == true && board[k, l].isBlack && board[k, l].type == "king")
                            {
                                // check
                                Debug.Log("Check.");
                            }
                        }
                    }
                } // player

            }
        } // end for
        
    } // CheckCheck




    //int BoardEval() // maybe we pick a better heuristic
    //{
    //    int eval = 0;

    //    for(int i = 0; i<8; i++)
    //    {
    //        for (int j = 0; j<8; j++)
    //        {
    //            if (board[i, j].isBlack) // ai - maximizing score
    //            {
    //                eval = eval + board[i, j].value;
    //            }
    //            if (!board[i, j].isBlack) // player - minimizing score
    //            {
    //                eval = eval - board[i, j].value;
    //            }
    //        }
    //    }
    //    return eval;
    //} // BoardEval

} // BoardScript
