using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    [SerializeField]
    private int depth = 3;


    private Node root;

    [SerializeField]
    private List<GameObject> blackPieces;

    [SerializeField]
    private List<GameObject> whitePieces;

    private int counter=0;


    // Start is called before the first frame update
    void Start()
    {
//        Debug.Log("startnum"+root.children.Count);
        root = CreateTree(depth);
        Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,root ,true, -1000000, 1000000));
        Debug.Log("alpha-beta counter: "+counter);
        boardArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,root ,true, -1000000, 1000000));
        }
        
    } 

    private Node CreateTree(int depth)
    {
        Node tree = new Node();

        childrenNodes(depth, tree);
    
        return tree;
    }

    private char[][] boardArray()
    {
        char[][] board = new char[8][];
        for(int i = 0;i<8;i++)
        {
            board[i] = new char[8];
        }

                for(int i = 0;i<8;i++)
                {
                    for(int j = 0;j<8;j++)
                    {
                        board[i][j] = '_';
                    }
                }

        foreach (var piece in whitePieces) 
        {
            board[(int)(piece.transform.position.y)*(-1)][(int)(piece.transform.position.x)] = 'P';
        }
        foreach (var piece in blackPieces) 
        {
            board[(int)(piece.transform.position.y)*(-1)][(int)(piece.transform.position.x)] = 'p';
        }

                for(int i = 0;i<8;i++)
                {
                    for(int j = 0;j<8;j++)
                    {
                        Debug.Log(board[i][j]);
                    }
                }

        whitePieces[6].transform.position = new Vector3(3f, -4f, 0f);
        
        return board;
    }

    private void childrenNodes(int depth, Node node)
    {
        if(depth>0)
        {
            for(int i =0;i<3;i++)
            {
                Node child = new Node();
                child.value = depth*10;
                node.children.Add(child);
            childrenNodes(depth-1, node.children[i]);

            }
        }
       // Debug.Log(depth+"DEPTHTEST");
    }

    private void BoardStates()
    {

    }

    //maximizig - true = choose higher
    //minimizing = xhoose lower

    private int MiniMaxAlgorithm(int depth, Node position, bool maximizingPlayer, int alpha, int beta)
    { 
        counter++;
        if (depth == 0)
        {
            return position.value;
        }
 
        if (maximizingPlayer)
        {
            position.maxValue = -100000000;
            position.alpha = alpha;
            position.beta = beta;
            foreach (var node in position.children) 
                {
                    int eval = MiniMaxAlgorithm(depth-1,node ,false, position.alpha, position.beta);
                    position.maxValue = Mathf.Max(position.maxValue, eval);
                    position.alpha = Mathf.Max(position.alpha, eval);

                    if(position.beta <= position.alpha)
                    {
                        break;
                    }
                }
            return position.maxValue;
        }
        else
        {
            position.minValue = 100000000;
            position.alpha = alpha;
            position.beta = beta;
            foreach (var node in position.children) 
                {
                    int eval = MiniMaxAlgorithm(depth-1,node ,false, position.alpha, position.beta);
                    position.minValue = Mathf.Min(position.minValue, eval);
                    position.beta = Mathf.Min(position.beta, eval);

                    if(position.beta <= position.alpha)
                    {
                        break;
                    }
                }
            return position.minValue;
        }

    }

}

public class Node
    {

        public char[][] board = new char[8][];
        public List<Node> children = new List<Node>();
        public int value;
        public int minValue;
        public int maxValue;
        public int alpha;
        public int beta;
        
    }