using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This implements the Minimax algorithm
 *
 * @author Patrick Leonard (7008113), Jenny Lim (6978118)
 * @version 1.0 (2022-19-12)
 */
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

    //[SerializeField]
    //private static BoardScript boardManager;

    //[SerializeField]
    //private List<GameObject> blackPieces;

    //[SerializeField]
    //private List<GameObject> whitePieces;

    private int counter=0;


    // Start is called before the first frame update
    void Start()
    {
            updateBoard(true);
    } // Start

    void Update()
    {
        if (Input.GetKeyDown("space")) //testing running algo more than once
        {
        //boardArray();//get 2d array from board state - not necesary
        }
        //Debug.Log(boardManager.board[0,0].value);
    } // Update


    /**
     * Starts AI's turn.
     */
    public void AITurn()
    {
        root = CreateTree(depth); //create tree of board states
        Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,depth ,root ,true, -1000000, 1000000)); //run minimax
        Debug.Log("alpha-beta counter: "+counter); //count number of 
        updateBoard(false);
    } // AITurn


    /**
     * Creates Tree.
     *
     * @param depth The depth of the tree.
     * @return Node The root node.
     */
    private Node CreateTree(int depth)//creates tree from node
    {
        Node tree = new Node(); //default node
        for(int i = 0; i<8;i++)
        {
            for(int j = 0;j<8;j++)
            {
                tree.boardState[i,j] = new Piece(BoardScript.Instance.board[i,j].isTaken, BoardScript.Instance.board[i,j].isBlack,BoardScript.Instance.board[i,j].isWhite, BoardScript.Instance.board[i,j].yPosition, BoardScript.Instance.board[i,j].xPosition, BoardScript.Instance.board[i,j].type);
            }
        }

        childrenNodes(depth, tree, true); //create children with depth specified
    
        return tree;//return built tree
    } // CreateTree


    /**
     * Updates the board in code and graphically.
     *
     * @param playermove If it's the player's move or not.
     */
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
                    BoardScript.Instance.board[i,j] = new Piece(root.boardState[i,j].isTaken, root.boardState[i,j].isBlack,root.boardState[i,j].isWhite, root.boardState[i,j].yPosition, root.boardState[i,j].xPosition,root.boardState[i,j].type);
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
                if(BoardScript.Instance.board[i,j].isBlack)
                {
                    if (BoardScript.Instance.board[i,j].type == "pawn")
                    {
                        Instantiate(blackPawn, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "knight")
                    {
                        Instantiate(blackKnight, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "bishop")
                    {
                        Instantiate(blackBishop, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "rook")
                    {
                        Instantiate(blackRook, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "queen")
                    {
                        Instantiate(blackQueen, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "king")
                    {
                        Instantiate(blackKing, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                }
                else
                {
                    if (BoardScript.Instance.board[i,j].type == "pawn")
                    {
                        Instantiate(whitePawn, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "knight")
                    {
                        Instantiate(whiteKnight, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "bishop")
                    {
                        Instantiate(whiteBishop, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "rook")
                    {
                        Instantiate(whiteRook, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "queen")
                    {
                        Instantiate(whiteQueen, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }
                    if (BoardScript.Instance.board[i,j].type == "king")
                    {
                        Instantiate(whiteKing, new Vector3(BoardScript.Instance.board[i,j].xPosition, 0 - BoardScript.Instance.board[i,j].yPosition, 0f), Quaternion.identity);
                    }

                }
            }
        }

    } // updateBoard


    /**
     * Gets children nodes of a node.
     *
     * @param depth The depth of the tree.
     * @param node The parent node in question.
     * @param isAITurn If its the A.I's turn.
     */
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
                            node.boardState[i, j].canMove = BlackMoveCheck(node.boardState, node.boardState[i, j]);
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
                                                child.boardState[z,x] = new Piece(node.boardState[z,x].isTaken, node.boardState[z,x].isBlack,node.boardState[z,x].isWhite, z, x, node.boardState[z,x].type);
                                            }
                                        }
                                        //move piece to spot
                                        child.boardState[p,l].updatePiece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack,child.boardState[i,j].isWhite, p, l, child.boardState[i,j].type); //= new Piece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack, p, l, child.boardState[i,j].type);
                                        child.boardState[i,j].updatePiece(false, false, false, i, j, "empty");
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
                            node.boardState[i, j].canMove = BlackMoveCheck(node.boardState, node.boardState[i, j]);
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
                                                child.boardState[z,x] = new Piece(node.boardState[z,x].isTaken, node.boardState[z,x].isBlack,node.boardState[z,x].isWhite, node.boardState[z,x].xPosition, node.boardState[z,x].yPosition, node.boardState[z,x].type);
                                            }
                                        }
                                        child.boardState[p,l].updatePiece(child.boardState[i,j].isTaken, child.boardState[i,j].isBlack,child.boardState[i,j].isWhite, p, l, child.boardState[i,j].type);
                                        child.boardState[i,j].updatePiece(false, false,false, i, j, "empty");
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
    } // childrenNodes


    /**
     * Assigns the board a score.
     *
     * @param node The node that holds the current board state.
     * @return int The evaluation.
     */
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


    /**
     * The Minimax algorithm.
     *
     * @param depth The depth of the tree.
     * @param trueDepth The end of the tree.
     * @param position Possible move.
     * @param maximizingPlayer If the A.I is to (hypothetically) be the maximizing player.
     * @oaram alpha The alpha value for pruning.
     * @param beta The beta value for pruning.
     * @return int The best value.
     */
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

                        root.boardState[i,j].updatePiece(position.children[stateTracker].boardState[i,j].isTaken, position.children[stateTracker].boardState[i,j].isBlack, position.children[stateTracker].boardState[i,j].isWhite, position.children[stateTracker].boardState[i,j].yPosition, position.children[stateTracker].boardState[i,j].xPosition,position.children[stateTracker].boardState[i,j].type);
                        if (root.boardState[i, j].type == "pawn" && root.boardState[i, j].yPosition == 7)
                        {
                            //root.boardState[i, j].Promote();

                            //just made the ai pick a queen for now lol
                            root.boardState[i, j].updatePiece(position.children[stateTracker].boardState[i, j].isTaken, position.children[stateTracker].boardState[i, j].isBlack, position.children[stateTracker].boardState[i,j].isWhite, position.children[stateTracker].boardState[i, j].yPosition, position.children[stateTracker].boardState[i, j].xPosition, "queen");
                        }
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
                    int eval = MiniMaxAlgorithm(depth-1, trueDepth, position.children[i] ,false, position.alpha, position.beta);//children children until depth 0
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
                        root.boardState[i,j].updatePiece(position.children[stateTracker].boardState[i,j].isTaken, position.children[stateTracker].boardState[i,j].isBlack,position.children[stateTracker].boardState[i,j].isWhite, position.children[stateTracker].boardState[i,j].yPosition, position.children[stateTracker].boardState[i,j].xPosition,position.children[stateTracker].boardState[i,j].type);
                    }
                }
            }

            return position.minValue; //return best value
        }

    } // MiniMaxAlgorithm


    /**
     * Checks where a black piece can move.
     *
     * @param boardArray The board state.
     * @param p The piece to be moved.
     * @return bool[,] The result.
     */
    public static bool[,] BlackMoveCheck(Piece[,] boardArray, Piece p)
    {
        bool north = false;
        bool northEast = false;
        bool east = false;
        bool southEast = false;
        bool south = false;
        bool southWest = false;
        bool west = false;
        bool northWest = false;
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

            if (p.yPosition + 1 < 8 && !boardArray[p.yPosition+1,p.xPosition].isBlack && !boardArray[p.yPosition+1,p.xPosition].isWhite)
            {
                canMove[p.yPosition + 1, p.xPosition] = true;

                if (p.yPosition == 1 && !boardArray[p.yPosition+2,p.xPosition].isBlack && !boardArray[p.yPosition+2,p.xPosition].isWhite)
                {
                    canMove[p.yPosition + 2, p.xPosition] = true;
                }
            }
            if(p.yPosition + 1 < 8 && p.xPosition +1 < 8) 
            {
                if (boardArray[p.yPosition+1,p.xPosition+1].isWhite)
                {
                    canMove[p.yPosition + 1, p.xPosition+1] = true;
                }
            }

            if(p.yPosition + 1 < 8 && p.xPosition-1 > -1)
            {
                if (boardArray[p.yPosition+1,p.xPosition-1].isWhite)
                {
                    canMove[p.yPosition + 1, p.xPosition - 1] = true;
                }
            }

            // check if its the pawn's first move

            //if diagonal proximity has enemy piece

            //if (BoardScript.Instance.board[p.xPosition + 1, p.yPosition + 1] != null)
            //{
                //canMove[p.xPosition + 1, p.yPosition + 1] = true; // can only move like this to capture
            //}

            p.pawnHasMoved = true;

        } // pawn


        if (p.type == "knight") // knights move in an 'L'
        {
            if (p.xPosition + 2 < 8 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1,p.xPosition+2].isBlack)
            {
                canMove[p.yPosition + 1, p.xPosition + 2] = true;
            }
            if (p.xPosition + 2 < 8 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1,p.xPosition+2].isBlack)
            {
                canMove[p.yPosition - 1, p.xPosition + 2] = true;
            }

            if (p.xPosition + 1 < 8 && p.yPosition + 2 < 8 && !boardArray[p.yPosition+2,p.xPosition+1].isBlack)
            {
                canMove[p.yPosition + 2, p.xPosition + 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition + 2 < 8 && !boardArray[p.yPosition+2,p.xPosition-1].isBlack)
            {
                canMove[p.yPosition + 2, p.xPosition - 1] = true;
            }

            if (p.xPosition - 2 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1,p.xPosition-2].isBlack)
            {
                canMove[p.yPosition - 1, p.xPosition - 2] = true;
            }
            if (p.xPosition - 2 > -1 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1,p.xPosition-2].isBlack)
            {
                canMove[p.yPosition + 1, p.xPosition - 2] = true;
            }

            if (p.xPosition - 1 > -1 && p.yPosition - 2 > -1 && !boardArray[p.yPosition-2,p.xPosition-1].isBlack)
            {
                canMove[p.yPosition - 2, p.xPosition - 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition - 2 > -1 && !boardArray[p.yPosition-2,p.xPosition+1].isBlack)
            {
                canMove[p.yPosition - 2, p.xPosition + 1] = true;
            }
        } // knight


        if (p.type == "bishop") // bishops move in diagonals
        {
            for (int i = 1; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)//southEast
                {
                    if(boardArray[p.yPosition+i, p.xPosition+i].isBlack)
                    {
                        southEast = true;
                    }
                    if(!southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition+i].isWhite && !southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                        southEast = true;
                    }
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)//northWest
                {
                    if(boardArray[p.yPosition-i, p.xPosition-i].isBlack)
                    {
                        northWest = true;
                    }
                    if(!northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition-i].isWhite && !northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                        northWest = true;
                    }                
                }
                if (p.xPosition - i > -1 && p.yPosition + i < 8)//southWest
                {
                    if(boardArray[p.yPosition+i, p.xPosition-i].isBlack)
                    {
                        southWest = true;
                    }
                    if(!southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                    }     
                    if(boardArray[p.yPosition+i, p.xPosition-i].isWhite && !southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                        southWest = true;
                    }           
                }
                if (p.xPosition + i < 8 && p.yPosition - i > -1)//northEast
                {
                    if(boardArray[p.yPosition-i, p.xPosition+i].isBlack)
                    {
                        northEast = true;
                    }
                    if(!northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition+i].isWhite && !northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                        northEast = true;
                    }                
                }
            }
        } // bishop


        if (p.type == "rook") // rooks move in a cross
        {
            for (int i = 1; i < 8; i++)
            {
                if (p.yPosition + i < 8)//south
                {
                    if(boardArray[p.yPosition+i, p.xPosition].isBlack)
                    {
                        south = true;
                    }
                    if(!south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                    }
                   if(boardArray[p.yPosition+i, p.xPosition].isWhite && !south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                        south = true;
                    }      
                    
                }
                if (p.xPosition + i < 8)//east
                {
                    if(boardArray[p.yPosition, p.xPosition+i].isBlack)
                    {
                        east = true;
                    }
                    if(!east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                    }
                   if(boardArray[p.yPosition, p.xPosition+i].isWhite && !east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                        east = true;
                    } 
                    
                }

                if (p.yPosition - i > -1)//north
                {
                    if(boardArray[p.yPosition-i, p.xPosition].isBlack)
                    {
                        north = true;
                    }
                    if(!north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition].isWhite && !north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                        north = true;
                    }                   
                }
                if (p.xPosition - i > -1)//west
                {
                    if(boardArray[p.yPosition, p.xPosition-i].isBlack)
                    {
                        west = true;
                    }
                    if(!west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition-i].isWhite && !west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                        west = true;
                    }                   
                } // alsoalso does what rook does
            }
        } // rook


        if (p.type == "queen") // can probably condense this
        {
            // does what king does
            /*
            if (p.yPosition + 1 < 8 && !boardArray[p.yPosition + 1, p.xPosition].isBlack)
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
            */


            for (int i = 1; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)//southEast
                {
                    if(boardArray[p.yPosition+i, p.xPosition+i].isBlack)
                    {
                        southEast = true;
                    }
                    if(!southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition+i].isWhite && !southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                        southEast = true;
                    }
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)//northWest
                {
                    if(boardArray[p.yPosition-i, p.xPosition-i].isBlack)
                    {
                        northWest = true;
                    }
                    if(!northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition-i].isWhite && !northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                        northWest = true;
                    }                
                }
                if (p.xPosition - i > -1 && p.yPosition + i < 8)//southWest
                {
                    if(boardArray[p.yPosition+i, p.xPosition-i].isBlack)
                    {
                        southWest = true;
                    }
                    if(!southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                    }     
                    if(boardArray[p.yPosition+i, p.xPosition-i].isWhite && !southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                        southWest = true;
                    }           
                }
                if (p.xPosition + i < 8 && p.yPosition - i > -1)//northEast
                {
                    if(boardArray[p.yPosition-i, p.xPosition+i].isBlack)
                    {
                        northEast = true;
                    }
                    if(!northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition+i].isWhite && !northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                        northEast = true;
                    }                
                }
                 // also does what bishop does

                if (p.yPosition + i < 8)//south
                {
                    if(boardArray[p.yPosition+i, p.xPosition].isBlack)
                    {
                        south = true;
                    }
                    if(!south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                    }
                   if(boardArray[p.yPosition+i, p.xPosition].isWhite && !south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                        south = true;
                    }      
                    
                }
                if (p.xPosition + i < 8)//east
                {
                    if(boardArray[p.yPosition, p.xPosition+i].isBlack)
                    {
                        east = true;
                    }
                    if(!east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                    }
                   if(boardArray[p.yPosition, p.xPosition+i].isWhite && !east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                        east = true;
                    } 
                    
                }

                if (p.yPosition - i > -1)//north
                {
                    if(boardArray[p.yPosition-i, p.xPosition].isBlack)
                    {
                        north = true;
                    }
                    if(!north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition].isWhite && !north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                        north = true;
                    }                   
                }
                if (p.xPosition - i > -1)//west
                {
                    if(boardArray[p.yPosition, p.xPosition-i].isBlack)
                    {
                        west = true;
                    }
                    if(!west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition-i].isWhite && !west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                        west = true;
                    }                   
                } // alsoalso does what rook does
            }
        } // queen


        if (p.type == "king") // king can only move around them
        {
//            Debug.Log("king: YPOS: " + p.xPosition + " XPOS: " + p.yPosition);
            if (p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition].isBlack)//south
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8 && !boardArray[p.yPosition, p.xPosition+1].isBlack)//east
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition+1].isBlack)//southeast
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition].isBlack)//north
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1 && !boardArray[p.yPosition, p.xPosition-1].isBlack)//west
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition-1].isBlack)//northwest
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition-1].isBlack)//southwest
            {
                canMove[p.yPosition + 1, p.xPosition - 1] = true;
            }
                        if (p.xPosition + 1 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition+1].isBlack)//northeast
            {
                canMove[p.yPosition - 1, p.xPosition + 1] = true;
            }
            //add southwest AND northeast
        } // king


        // 2d for loop for every spot on the board
        // if board spot isnt null
        // for loop through yPos' for that spot and above
        // canMove = false;

        //put in new system for finding spots due to some pieces not being able to jump over and thi
        /*
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (BoardScript.Instance.board[i, j] != null)
                {
                    for (int y = BoardScript.Instance.board[i, j].yPosition + 1; y < 8; y++)
                    {
                        canMove[BoardScript.Instance.board[i, j].xPosition, y] = false;
                    }
                }
            }
        }
        */

        return canMove;
    } // BlackMoveCheck


    /**
     * Checks where a white piece can move.
     *
     * @param boardArray The board state.
     * @param p The piece to be moved.
     * @return bool[,] The result.
     */
    public static bool[,] WhiteMoveCheck(Piece[,] boardArray, Piece p) // stop setting canmove to be true once an enemy piece is in the way --  if a board spot isnt null, then you can't move to the spots above it -- need reference to board, or move this into board and have pieces refer to this
    {
        bool north = false;
        bool northEast = false;
        bool east = false;
        bool southEast = false;
        bool south = false;
        bool southWest = false;
        bool west = false;
        bool northWest = false;
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

            if (p.yPosition - 1 > -1 && !boardArray[p.yPosition-1,p.xPosition].isWhite && !boardArray[p.yPosition-1,p.xPosition].isBlack)
            {
                canMove[p.yPosition-1, p.xPosition] = true;

                if (p.yPosition == 6 && !boardArray[p.yPosition-2,p.xPosition].isWhite)
                {
                    canMove[p.yPosition -2, p.xPosition] = true;
                }
            }
            if(p.yPosition -1 > -1 && p.xPosition - 1 > -1)
            {
                if (boardArray[p.yPosition-1,p.xPosition-1].isBlack)
                {
                    canMove[p.yPosition - 1, p.xPosition-1] = true;
                }
            }

            if(p.yPosition - 1 > -1 && p.xPosition + 1 < 8)
            {
                if (boardArray[p.yPosition-1,p.xPosition+1].isBlack)
                {
                    canMove[p.yPosition - 1, p.xPosition+1] = true;
                }
            }

            

            // check if its the pawn's first move

            //if diagonal proximity has enemy piece

            //if (BoardScript.Instance.board[p.xPosition + 1, p.yPosition + 1] != null)
            //{
                //canMove[p.xPosition + 1, p.yPosition + 1] = true; // can only move like this to capture
            //}

            p.pawnHasMoved = true;

        } // pawn


        if (p.type == "knight") // knights move in an 'L'
        {
            if (p.xPosition + 2 < 8 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1,p.xPosition+2].isWhite)
            {
                canMove[p.yPosition + 1, p.xPosition + 2] = true;
            }
            if (p.xPosition + 2 < 8 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1,p.xPosition+2].isWhite)
            {
                canMove[p.yPosition - 1, p.xPosition + 2] = true;
            }

            if (p.xPosition + 1 < 8 && p.yPosition + 2 < 8 && !boardArray[p.yPosition+2,p.xPosition+1].isWhite)
            {
                canMove[p.yPosition + 2, p.xPosition + 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition + 2 < 8 && !boardArray[p.yPosition+2,p.xPosition-1].isWhite)
            {
                canMove[p.yPosition + 2, p.xPosition - 1] = true;
            }

            if (p.xPosition - 2 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1,p.xPosition-2].isWhite)
            {
                canMove[p.yPosition - 1, p.xPosition - 2] = true;
            }
            if (p.xPosition - 2 > -1 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1,p.xPosition-2].isWhite)
            {
                canMove[p.yPosition + 1, p.xPosition - 2] = true;
            }

            if (p.xPosition - 1 > -1 && p.yPosition - 2 > -1 && !boardArray[p.yPosition-2,p.xPosition-1].isWhite)
            {
                canMove[p.yPosition - 2, p.xPosition - 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition - 2 > -1 && !boardArray[p.yPosition-2,p.xPosition+1].isWhite)
            {
                canMove[p.yPosition - 2, p.xPosition + 1] = true;
            }
        } // knight


        if (p.type == "bishop") // bishops move in diagonals
        {
            for (int i = 1; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)//southEast
                {
                    if(boardArray[p.yPosition+i, p.xPosition+i].isWhite)
                    {
                        southEast = true;
                    }
                    if(!southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition+i].isBlack)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                        southEast = true;
                    }
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)//northWest
                {
                    if(boardArray[p.yPosition-i, p.xPosition-i].isWhite)
                    {
                        northWest = true;
                    }
                    if(!northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                    }  
                    if(boardArray[p.yPosition-i, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                        northWest = true;
                    }              
                }
                if (p.xPosition - i > -1 && p.yPosition + i < 8)//southWest
                {
                    if(boardArray[p.yPosition+i, p.xPosition-i].isWhite)
                    {
                        southWest = true;
                    }
                    if(!southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                    }    
                    if(boardArray[p.yPosition+i, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                        southWest = true;
                    }            
                }
                if (p.xPosition + i < 8 && p.yPosition - i > -1)//northEast
                {
                    if(boardArray[p.yPosition-i, p.xPosition+i].isWhite)
                    {
                        northEast = true;
                    }
                    if(!northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                    }   
                    if(boardArray[p.yPosition-i, p.xPosition+i].isBlack)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                        northEast = true;
                    }             
                }
            }
        } // bishop


        if (p.type == "rook") // rooks move in a cross
        {
            for (int i = 1; i < 8; i++)
            {
                if (p.yPosition + i < 8)//south
                {
                    if(boardArray[p.yPosition+i, p.xPosition].isWhite)
                    {
                        south = true;
                    }
                    if(!south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition].isBlack)
                    {
                       // canMove[p.yPosition + i, p.xPosition] = true;
                        south = true;
                    }
                                       }
                if (p.xPosition + i < 8)//east
                {
                    if(boardArray[p.yPosition, p.xPosition+i].isWhite)
                    {
                        east = true;
                    }
                    if(!east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition+i].isBlack)
                    {
                       // canMove[p.yPosition, p.xPosition + i] = true;
                        east = true;
                    }
                }

                if (p.yPosition - i > -1)//north
                {
                    if(boardArray[p.yPosition-i, p.xPosition].isWhite)
                    {
                        north = true;
                    }
                    if(!north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition].isBlack)
                    {
                      //  canMove[p.yPosition - i, p.xPosition] = true;
                        north = true;
                    }
               }
                if (p.xPosition - i > -1)//west
                {
                    if(boardArray[p.yPosition, p.xPosition-i].isWhite)
                    {
                        west = true;
                    }
                    if(!west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                        west = true;
                    }     
                } // alsoalso does what rook does
            }
        } // rook


        if (p.type == "queen") // can probably condense this
        {
            for (int i = 1; i < 8; i++)
            {
                if (p.xPosition + i < 8 && p.yPosition + i < 8)//southEast
                {
                    if(boardArray[p.yPosition+i, p.xPosition+i].isWhite)
                    {
                        southEast = true;
                    }
                    if(!southEast)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition+i].isBlack)
                    {
                        canMove[p.yPosition + i, p.xPosition + i] = true;
                        southEast = true;
                    }
                }
                if (p.xPosition - i > -1 && p.yPosition - i > -1)//northWest
                {
                    if(boardArray[p.yPosition-i, p.xPosition-i].isWhite)
                    {
                        northWest = true;
                    }
                    if(!northWest)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                    }  
                    if(boardArray[p.yPosition-i, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition - i, p.xPosition - i] = true;
                        northWest = true;
                    }              
                }
                if (p.xPosition - i > -1 && p.yPosition + i < 8)//southWest
                {
                    if(boardArray[p.yPosition+i, p.xPosition-i].isWhite)
                    {
                        southWest = true;
                    }
                    if(!southWest)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                    }    
                    if(boardArray[p.yPosition+i, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition + i, p.xPosition - i] = true;
                        southWest = true;
                    }            
                }
                if (p.xPosition + i < 8 && p.yPosition - i > -1)//northEast
                {
                    if(boardArray[p.yPosition-i, p.xPosition+i].isWhite)
                    {
                        northEast = true;
                    }
                    if(!northEast)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                    }   
                    if(boardArray[p.yPosition-i, p.xPosition+i].isBlack)
                    {
                        canMove[p.yPosition - i, p.xPosition + i] = true;
                        northEast = true;
                    }             
                }
                 // also does what bishop does


                if (p.yPosition + i < 8)//south
                {
                    if(boardArray[p.yPosition+i, p.xPosition].isWhite)
                    {
                        south = true;
                    }
                    if(!south)
                    {
                        canMove[p.yPosition + i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition+i, p.xPosition].isBlack)
                    {
                       // canMove[p.yPosition + i, p.xPosition] = true;
                        south = true;
                    }
                                       }
                if (p.xPosition + i < 8)//east
                {
                    if(boardArray[p.yPosition, p.xPosition+i].isWhite)
                    {
                        east = true;
                    }
                    if(!east)
                    {
                        canMove[p.yPosition, p.xPosition + i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition+i].isBlack)
                    {
                       // canMove[p.yPosition, p.xPosition + i] = true;
                        east = true;
                    }
                }

                if (p.yPosition - i > -1)//north
                {
                    if(boardArray[p.yPosition-i, p.xPosition].isWhite)
                    {
                        north = true;
                    }
                    if(!north)
                    {
                        canMove[p.yPosition - i, p.xPosition] = true;
                    }
                    if(boardArray[p.yPosition-i, p.xPosition].isBlack)
                    {
                      //  canMove[p.yPosition - i, p.xPosition] = true;
                        north = true;
                    }
               }
                if (p.xPosition - i > -1)//west
                {
                    if(boardArray[p.yPosition, p.xPosition-i].isWhite)
                    {
                        west = true;
                    }
                    if(!west)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                    }
                    if(boardArray[p.yPosition, p.xPosition-i].isBlack)
                    {
                        canMove[p.yPosition, p.xPosition - i] = true;
                        west = true;
                    }     
                } // alsoalso does what rook does
            }
        } // queen


        if (p.type == "king") // king can only move around them
        {
//            Debug.Log("king: YPOS: " + p.xPosition + " XPOS: " + p.yPosition);
            if (p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition].isWhite)//south
            {
                canMove[p.yPosition + 1, p.xPosition] = true;
            }
            if (p.xPosition + 1 < 8 && !boardArray[p.yPosition, p.xPosition+1].isWhite)//east
            {
                canMove[p.yPosition, p.xPosition + 1] = true;
            }
            if (p.xPosition + 1 < 8 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition+1].isWhite)//southeast
            {
                canMove[p.yPosition + 1, p.xPosition + 1] = true;
            }
            if (p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition].isWhite)//north
            {
                canMove[p.yPosition - 1, p.xPosition] = true;
            }
            if (p.xPosition - 1 > -1 && !boardArray[p.yPosition, p.xPosition-1].isWhite)//west
            {
                canMove[p.yPosition, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition-1].isWhite)//northwest
            {
                canMove[p.yPosition - 1, p.xPosition - 1] = true;
            }
            if (p.xPosition - 1 > -1 && p.yPosition + 1 < 8 && !boardArray[p.yPosition+1, p.xPosition-1].isWhite)//southwest
            {
                canMove[p.yPosition + 1, p.xPosition - 1] = true;
            }
                        if (p.xPosition + 1 > -1 && p.yPosition - 1 > -1 && !boardArray[p.yPosition-1, p.xPosition+1].isWhite)//northeast
            {
                canMove[p.yPosition - 1, p.xPosition + 1] = true;
            }
            //add southwest AND northeast
        } // king


        // 2d for loop for every spot on the board
        // if board spot isnt null
        // for loop through yPos' for that spot and above
        // canMove = false;

        //put in new system for finding spots due to some pieces not being able to jump over and thi
        /*
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (BoardScript.Instance.board[i, j] != null)
                {
                    for (int y = BoardScript.Instance.board[i, j].yPosition + 1; y < 8; y++)
                    {
                        canMove[BoardScript.Instance.board[i, j].xPosition, y] = false;
                    }
                }
            }
        }
        */

        return canMove;
    } // WhiteMoveCheck

} // MiniMax


/**
 * This is a Node.
 *
 * @author Patrick Leonard (7008113), Jenny Lim (6978118)
 * @version 1.0 (2022-19-12)
 */
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
        
    } // Node