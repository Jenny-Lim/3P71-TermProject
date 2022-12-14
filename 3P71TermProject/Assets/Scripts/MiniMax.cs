using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    [SerializeField]
    private int depth = 3;


    private Node root;

    private int counter=0;


    // Start is called before the first frame update
    void Start()
    {
//        Debug.Log("startnum"+root.children.Count);
        root = CreateTree(depth);
        Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,root ,true, -1000000, 1000000));
        Debug.Log("alpha-beta counter: "+counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Node CreateTree(int depth)
    {
        Node tree = new Node();

        childrenNodes(depth, tree);
    
        return tree;
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

        public List<Node> children = new List<Node>();
        public int value;
        public int minValue;
        public int maxValue;
        public int alpha;
        public int beta;
        
    }