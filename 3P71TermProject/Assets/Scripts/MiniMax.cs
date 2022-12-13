using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    [SerializeField]
    private int depth = 3;


    private Node root;


    // Start is called before the first frame update
    void Start()
    {
//        Debug.Log("startnum"+root.children.Count);
        root = CreateTree(depth);
        Debug.Log("AI CHOOSES: "+MiniMaxAlgorithm(depth,root ,true));
        Debug.Log(root.children[1].children[1].value);
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
            //node.Attach(node, depth*2);
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

    private int MiniMaxAlgorithm(int depth, Node position, bool maximizingPlayer)
    { 
        if (depth == 0)
        {
            return position.value;
        }
 
        if (maximizingPlayer)
        {
            position.maxValue = -100000000;
            foreach (var node in position.children) 
                {
                    int eval = MiniMaxAlgorithm(depth-1,node ,false);
                    position.maxValue = Mathf.Max(position.maxValue, eval);
                }
            return position.maxValue;
        }
        else
        {
            position.minValue = 100000000;
            foreach (var node in position.children) 
                {
                    int eval = MiniMaxAlgorithm(depth-1,node ,false);
                    position.minValue = Mathf.Min(position.minValue, eval);
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
        
        //public Node()
        //{
        //    SetChildren(children);
        //}

        public void Attach(Node child, int value)
        {
            children.Add(child);
            child.value = value;
            //Debug.Log("added 54");
           // child._parent = this;
        }


    }