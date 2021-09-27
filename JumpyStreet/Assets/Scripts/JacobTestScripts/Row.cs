using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Biome.Type rowType = new Biome.Type();
    public int rowValue;
    [SerializeField] GameObject node;
    [SerializeField] public List<GameObject> nodeArray;
    [SerializeField] int rowWidth;
    public Vector3 targetPos;
    public bool canMove = false;

    private void FixedUpdate()
    {
        
    }
    public void SpawnNode(int width, /*optional*/ bool isHazard = false, int rowVal = 30, bool isBackWall = false)
    {
        rowValue = rowVal;
        rowWidth = width;
        if(isHazard){
            GameObject nodeLeft,nodeRight;
            nodeLeft = Instantiate(node,new Vector3(-width,this.transform.position.y+.5f,this.transform.position.z), Quaternion.identity);
            nodeRight = Instantiate(node,new Vector3(width,this.transform.position.y+.5f,this.transform.position.z), Quaternion.identity);
            nodeLeft.GetComponent<Node>().targetNode = nodeRight;
            nodeRight.GetComponent<Node>().targetNode = nodeLeft;
            nodeArray.Add(nodeRight);
            nodeArray.Add(nodeLeft);
        }
        else{
            for(int i = -width; i <= width; i++){
                GameObject newNode = Instantiate(node,new Vector3(i,this.transform.position.y+.5f,this.transform.position.z), Quaternion.identity);
                bool isWall;
                if (i<-12 || i > 12 || isBackWall)
                {
                    isWall = true;
                }
                else
                {
                    isWall = false;
                }

                newNode.GetComponent<Node>().SetNode(isWall,rowType);
                nodeArray.Add(newNode);
            }
        }

        foreach (GameObject node in nodeArray)
        {
            node.transform.parent = this.transform;
        }

    }


}