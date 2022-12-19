using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    [SerializeField]
    private int depth;


    private Node root;

    [SerializeField]
    private GameObject blackPawn;
    [SerializeField]
    private GameObject blackRook;
    [SerializeField]
    private GameObject blackKnight;
    [SerializeField]
    private GameObject blackBishop;
    [SerializeField]
    private GameObject blackQueen;
    [SerializeField]
    private GameObject blackKing;
    [SerializeField]
    private GameObject whitePawn;
    [SerializeField]
    private GameObject whiteRook;
    [SerializeField]
    private GameObject whiteKnight;
    [SerializeField]
    private GameObject whiteBishop;
    [SerializeField]
    private GameObject whiteQueen;
    [SerializeField]
    private GameObject whiteKing;

    [SerializeField]
    private static BoardScript boardManager;

    //[SerializeField]
    //private List<GameObject> blackPieces;

    //[SerializeField]
    //private List<GameObject> whitePieces;

    private int counter=0;


    // Start is called before the first frame update
    void Start()
    {
            updateBoard(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) //testing running algo more than once
        {
        root = CreateTree(depth); //create tree of board states
        Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,depth ,root ,true, -1000000, 1000000)); //run minimax
        Debug.Log("alpha-beta counter: "+counter); //count number of 
        updateBoard(false);
        //boardArray();//get 2d array from board state - not necesary
        }
        //Debug.Log(boardManager.board[0,0].value);
    } 

    private Node CreateTree(int depth)//creates tree from node
    {
        Node tree = new Node(); //default node
        for(int i = 0; i<8;i++)
        {
            for(int j = 0;j<8;j++)
            {
                tree.boardState[i,j] = new Piece(boardManager.board[i,j].isTaken, boardManager.board[i,j].isBlack, boardManager.board[i,j].yPosition, boardManager.board[i,j].xPosition, boardManager.board[i,j].type);
            }
        }

        childrenNodes(depth, tree, true); //create children with depth specified
    
        return tree;//return built tree
    }

    public void updateBoard(bool playermove)
    {
        Debug.Log("BOARD UPDATED");
        //update manager with algorithm board
        if(!playermove)
        {
            for(int i = 0; i<8;i++)
            {
                for(int j = 0;j<8;j++)
                {
                    boardManager.board[i,j] = new Piece(root.boardState[i,j].isTaken, root.boardState[i,j].isBlack, root.boardState[i,j].yPosition, root.boardState[i,j].xPosition,root.boardState[i,j].type);
                }
            }
        }

        GameObject[] pieces = GameObject.FindGameObjectsWithTag("BoardPiece");
        foreach(GameObject piece in pieces)
        {
            Destroy(piece);
        }

        
        GameObject[] playerPieces = GameObject.FindGameObjectsWithTag("PlayerPiece");
        foreach(GameObject piece in playerPieces)
        {
            Destroy(piece);
        }


        for(int i = 0; i<8;i++)
        {
            for(int j = 0;j<8;j++)
            {
                if(boardManager.board[i,j].isBlack)
                {
                    if (boardManager.board[i,j].type == "pawn")
                    {
                        Instantiate(blackPawn, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "knight")
                    {
                        Instantiate(blackKnight, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "bishop")
                    {
                        Instantiate(blackBishop, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "rook")
                    {
                        Instantiate(blackRook, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "queen")
                    {
                        Instantiate(blackQueen, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "king")
                    {
                        Instantiate(blackKing, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                }
                else
                {
                    if (boardManager.board[i,j].type == "pawn")
                    {
                        Instantiate(whitePawn, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "knight")
                    {
                        Instantiate(whiteKnight, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "bishop")
                    {
                        Instantiate(whiteBishop, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "rook")
                    {
                        Instantiate(whiteRook, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "queen")
                    {
                        Instantiate(whiteQueen, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (boardManager.board[i,j].type == "king")
                    {
                        Instantiate(whiteKing, new Vector3(boardManager.board[i,j].xPosition, 0 - boardManager.board[i,j].yPosition, 0f), Quaternion.identity);
                    }

                }
            }
        }

    }

    private void childrenNodes(int depth, Node node, bool isAITurn) //create nodes
    {   
        int arrayCount = 0;
        if(depth>0)//if not at bottom depth
        {
            if(isAITurn) //if black makes moves
            {
                for(int i = 0;i<8;i++)//go through node piece array
                {
                    for(int j = 0;j<8;j++)
                    {
                        if(node.boardState[i,j].isBlack && node.boardState[i,j].type != "empty")//if a piece is found that is black
                        {
                            node.boardState[i, j].canMove = BlackMoveCheck(node.boardState[i, j]);
                //node.boardState[i,j].canMove = node.boardState[i,j].BlackMoveCheck();//get matrix of possible moves for piece
                            for(int p = 0;p<8;p++)//go through matrix of future moves - PL for move matrix - IJ for original spot
                            {
                                for(int l = 0;l<8;l++)
                                {
                                    if(node.boardState[i,j].canMove[p,l])//if spot found
                                    {
                                        Node child = new Node(); //create default node
                                        for(int z = 0;z<8;z++)//initialize child piece array
                                        {
                                            for(int x=0;x<8;x++)//copy board state to child
                                            {
                                                child.boardState[z,x] = new Piece(node.boardState[z,x].isTaken, node.boardState[z,x].isBlack, z, x, node.boardState[z,x].type);
                                            }
                                        }
                                        //move piece to spot
                                        child.boardState[p,l].updatePiece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack, p, l, child.boardState[i,j].type); //= new Piece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack, p, l, child.boardState[i,j].type);
                                        child.boardState[i,j].updatePiece(false, false, i, j, "empty");
                                        child.value = depth*10; //insert value into node - change to board state valueation
                                        node.children.Add(child);//insert node into child of this node
                                        childrenNodes(depth-1, node.children[arrayCount], !isAITurn);//create child node from child just made
                                        arrayCount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
           else //if white makes moves - gotta work on
            {
                for(int i = 0;i<8;i++)//go through node piece array
                {
                    for(int j = 0;j<8;j++)
                    {
                        if(node.boardState[i,j].isBlack && node.boardState[i,j].type != "empty")//if a piece is found that is black
                        {
                            node.boardState[i, j].canMove = BlackMoveCheck(node.boardState[i, j]);
                            //node.boardState[i,j].canMove = node.boardState[i,j].WhiteMoveCheck();//get matrix of possible moves for piece
                            for(int p = 0;p<8;p++)//go through matrix of future moves - PL for move matrix - IJ for original spot
                            {
                                for(int l = 0;l<8;l++)
                                {
                                    if(node.boardState[i,j].canMove[p,l])//if spot found
                                    {
                                        Node child = new Node();// = new Node(); //create default node
                                        for(int z = 0;z<8;z++)//initialize child piece array
                                        {
                                            for(int x=0;x<8;x++)
                                            {
                                                child.boardState[z,x] = new Piece(node.boardState[z,x].isTaken, node.boardState[z,x].isBlack, node.boardState[z,x].xPosition, node.boardState[z,x].yPosition, node.boardState[z,x].type);
                                            }
                                        }
                                        child.boardState[p,l].updatePiece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack, p, l, child.boardState[i,j].type);
                                        child.boardState[i,j].updatePiece(false, false, i, j, "empty");
                                        child.value = depth*10; //insert value into node - change to board state valueation
                                        node.children.Add(child);//insert node into child of this node
                                        childrenNodes(depth-1, node.children[arrayCount], !isAITurn);//create child node from child just made
                                        arrayCount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

        public bool[,] possibleMoves()
    {
return null;
    }

        public int BoardEval(Node node) // maybe we pick a better heuristic
    {
        int eval = 0;

        for(int i = 0; i<8; i++)
        {
            for (int j = 0; j<8; j++)
            {
                if (node.boardState[i, j].isBlack) // ai - maximizing score
                {
                    eval = eval + node.boardState[i, j].value;
                }
                if (!node.boardState[i, j].isBlack) // player - minimizing score
                {
                    eval = eval - node.boardState[i, j].value;
                }
            }
        }
        return eval;
    } // BoardEval

        // copied to minimax so can be used in algo with other piece arrays beside main

    //maximizig - true = choose higher
    //minimizing = xhoose lower

    private int MiniMaxAlgorithm(int depth,int trueDepth, Node position, bool maximizingPlayer, int alpha, int beta)//minimax algo
    { 
        int stateTracker = 0;
        counter++;//tracks for alpha beta pruning
        if (depth == 0)//if reach bottom depth
        {
            return BoardEval(position);//return value of node
        }
 
        if (maximizingPlayer)//best value for AI
        {
            position.maxValue = -100000000;//set max to negative infinity
            //position.alpha = alpha;//set alpha to stored alpha
            //position.beta = beta;//set beta to stored beta
            for (int i = 0;i<position.children.Count;i++) //for each child that node has
                {
                    int eval = MiniMaxAlgorithm(depth-1, trueDepth, position.children[i] ,false, position.alpha, position.beta); //get value by running algo on childrens children
                    if(eval>position.maxValue)
                    {
                        position.maxValue = eval;
                        stateTracker = i;
                    }
                    if(eval>position.alpha)
                    {
                        position.alpha = eval;
                    }
                   // position.maxValue = Mathf.Max(position.maxValue, eval); //get best value by comparing max value to child value
                    //position.alpha = Mathf.Max(position.alpha, eval);//get alpha by comparing stored alpha to value

                    if(position.beta <= position.alpha) //if beta is less or equal then loop can stop and will prune
                    {
                        break;
                    }
                }

            if(depth == trueDepth)
            {
                //Debug.Log("MOVE DATA: XPOS: "+position.children[stateTracker].xPosition+" YPOS: "+position.children[stateTracker].yPosition+" TYPE: "+position.children[stateTracker].type);
                for(int i = 0; i<8;i++)
                {
                    for(int j = 0;j<8;j++)
                    {
                        //root.boardState[i,j].updatePiece(position.boardState[i,j].isTaken, position.boardState[i,j].isBlack, position.boardState[i,j].yPosition, position.boardState[i,j].xPosition,position.boardState[i,j].type);

                        root.boardState[i,j].updatePiece(position.children[stateTracker].boardState[i,j].isTaken, position.children[stateTracker].boardState[i,j].isBlack, position.children[stateTracker].boardState[i,j].yPosition, position.children[stateTracker].boardState[i,j].xPosition,position.children[stateTracker].boardState[i,j].type);
                    }
                }
            }

            return position.maxValue; //return best value
        }
        else //best value for Player
        {
            position.minValue = 100000000;//set min to infinity, worst value for min as default
            //position.alpha = alpha;//set alpha and beta to store values
            //position.beta = beta;
            for(int i = 0;i<position.children.Count;i++) //each child of node
                {
                    int eval = MiniMaxAlgorithm(depth-1, trueDepth, position.children[i] ,true, position.alpha, position.beta);//children children until depth 0
                    if(eval<position.minValue)
                    {
                        position.minValue = eval;
                        stateTracker = i;
                    }
                    if(eval<position.beta)
                    {
                        position.beta = eval;
                    }
                    //position.minValue = Mathf.Min(position.minValue, eval);
                    //position.beta = Mathf.Min(position.beta, eval);

                    if(position.beta <= position.alpha)
                    {
                        break;
                    }
                }

            if(depth == trueDepth)
            {
                //Debug.Log("MOVE DATA: XPOS: "+position.children[stateTracker].xPosition+" YPOS: "+position.children[stateTracker].yPosition+" TYPE: "+position.children[stateTracker].type);
                for(int i = 0; i<8;i++)
                {
                    for(int j = 0;j<8;j++)
                    {
                        root.boardState[i,j].updatePiece(position.children[stateTracker].boardState[i,j].isTaken, position.children[stateTracker].boardState[i,j].isBlack, position.children[stateTracker].boardState[i,j].yPosition, position.children[stateTracker].boardState[i,j].xPosition,position.children[stateTracker].boardState[i,j].type);
                    }
                }
            }

            return position.minValue; //return best value
        }

    }

    public static bool[,] BlackMoveCheck(Piece p) // stop setting canmove to be true once an enemy piece is in the way --  if a board spot isnt null, then you can't move to the spots above it -- need reference to board, or move this into board and have pieces refer to this
    {
        bool[,] canMove = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                canMove[i, j] = false;
            }
        }

        if (p.type == "pawn") // pawns only move forward one tile at a time
        {

            // check if its the pawn's first move
            if (p.yPosition == 6)
            {
                canMove[p.yPosition - 2, p.xPosition] = true;
            }

            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }

            if (boardManager.board[p.xPosition + 1, p.yPosition + 1] != null)
            {
                canMove[p.xPosition + 1, p.yPosition + 1] = true; // can only move like this to capture
            }

            p.pawnHasMoved = true;

        } // pawn


        if (p.type == "knight") // knights move in an 'L'
        {
            if (p.xPosition + 2 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 2] = true;
            }

            if (p.xPosition + 1 < 8 && p.yPosition + 2 < 8)
            {
                canMove[p.yPosition + 2, p.xPosition + 1] = true;
            }

            if (p.xPosition - 2 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 2] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 2 > -1)
            {
                canMove[p.yPosition - 2, p.xPosition - 1] = true;
            }
        } // knight


        if (p.type == "bishop") // bishops move in diagonals
        {
            for (int i = 0; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition + i] = true;
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition - i] = true;
                }
            }
        } // bishop


        if (p.type == "rook") // rooks move in a cross
        {
            for (int i = 0; i < 8; i++)
            {
                if (p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition] = true;
                }
                if (p.xPosition + i < 8)
                {
                    canMove[p.yPosition, p.xPosition + i] = true;
                }

                if (p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition] = true;
                }
                if (p.xPosition - i > -1)
                {
                    canMove[p.yPosition, p.xPosition - i] = true;
                }
            }
        } // rook


        if (p.type == "queen") // can probably condense this
        {
            // does what king does
            if (p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8)
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1)
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }


            for (int i = 0; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition + i] = true;
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition - i] = true;
                } // also does what bishop does


                if (p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition] = true;
                }
                if (p.xPosition + i < 8)
                {
                    canMove[p.yPosition, p.xPosition + i] = true;
                }

                if (p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition] = true;
                }
                if (p.xPosition - i > -1)
                {
                    canMove[p.yPosition, p.xPosition - i] = true;
                } // alsoalso does what rook does
            }
        } // queen


        if (p.type == "king") // king can only move around them
        {
            Debug.Log("king: YPOS: " + p.xPosition + " XPOS: " + p.yPosition);
            if (p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8)
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1)
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }
        } // king


        // 2d for loop for every spot on the board
        // if board spot isnt null
        // for loop through yPos' for that spot and above
        // canMove = false;

        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (boardManager.board[i, j] != null)
                {
                    for (int y = boardManager.board[i, j].yPosition + 1; y < 8; y++)
                    {
                        canMove[boardManager.board[i, j].xPosition, y] = false;
                    }
                }
            }
        }

        return canMove;
    } // MoveCheck

    public static bool[,] WhiteMoveCheck(Piece p)
    {
        bool[,] canMove = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                canMove[i, j] = false;
            }
        }

        if (p.type == "pawn") // pawns only move forward one tile at a time
        {

            // check if its the pawn's first move
            if (p.yPosition == 6)
            {
                canMove[p.yPosition - 2, p.xPosition] = true;
            }

            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }

             if (boardManager.board[p.xPosition + 1, p.yPosition + 1]!=null){
             canMove[p.xPosition + 1, p.yPosition + 1] = true; // can only move like this to capture
                }

            p.pawnHasMoved = true;

        } // pawn


        if (p.type == "knight") // knights move in an 'L'
        {
            if (p.xPosition + 2 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 2] = true;
            }

            if (p.xPosition + 1 < 8 && p.yPosition + 2 < 8)
            {
                canMove[p.yPosition + 2, p.xPosition + 1] = true;
            }

            if (p.xPosition - 2 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 2] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 2 > -1)
            {
                canMove[p.yPosition - 2, p.xPosition - 1] = true;
            }
        } // knight


        if (p.type == "bishop") // bishops move in diagonals
        {
            for (int i = 0; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition + i] = true;
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition - i] = true;
                }
            }
        } // bishop


        if (p.type == "rook") // rooks move in a cross
        {
            for (int i = 0; i < 8; i++)
            {
                if (p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition] = true;
                }
                if (p.xPosition + i < 8)
                {
                    canMove[p.yPosition, p.xPosition + i] = true;
                }

                if (p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition] = true;
                }
                if (p.xPosition - i > -1)
                {
                    canMove[p.yPosition, p.xPosition - i] = true;
                }
            }
        } // rook


        if (p.type == "queen") // can probably condense this
        {
            // does what king does
            if (p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8)
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1)
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }


            for (int i = 0; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition + i] = true;
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition - i] = true;
                } // also does what bishop does


                if (p.yPosition + i < 8)
                {
                    canMove[p.yPosition + i, p.xPosition] = true;
                }
                if (p.xPosition + i < 8)
                {
                    canMove[p.yPosition, p.xPosition + i] = true;
                }

                if (p.yPosition - i > -1)
                {
                    canMove[p.yPosition - i, p.xPosition] = true;
                }
                if (p.xPosition - i > -1)
                {
                    canMove[p.yPosition, p.xPosition - i] = true;
                } // alsoalso does what rook does
            }
        } // queen


        if (p.type == "king") // king can only move around them
        {
            Debug.Log("king: YPOS: " + p.xPosition + " XPOS: " + p.yPosition);
            if (p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8)
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8)
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1)
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1)
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }
        } // king

        // 2d for loop for every spot on the board
        // if board spot isnt null
        // for loop through yPos' for that spot and above
        // canMove = false;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(boardManager.board[i, j] != null){
                    for (int y = boardManager.board[i, j].yPosition + 1; y < 8; y++)
                    {
                        canMove[boardManager.board[i, j].xPosition, y] = false;
                    }
                }
            }
        }

        return canMove;
    } // MoveCheck

}



public class Node //data stored in each node in tree
    {

        public Piece[,] boardState = new Piece[8,8];
        public List<Node> children = new List<Node>();//children nodes
        public int value;//value - retrieved from analyzing board state
        public int minValue;//minimum value - best value for AI
        public int maxValue;//maximum value - best value for player
        public int alpha = -1000000;//alpha value - for pruning
        public int beta = 1000000;//beta value - for pruning
        public string test;
        
    }