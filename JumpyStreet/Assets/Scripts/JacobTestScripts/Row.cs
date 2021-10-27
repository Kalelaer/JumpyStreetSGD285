using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Biome.Type rowType = new Biome.Type();
    public int rowValue;
    [SerializeField] GameObject node;
<<<<<<< Updated upstream
    [SerializeField] List<GameObject> nodeArray;
=======
    public List<GameObject> nodeArray;
    public  int hazardStart; //0 - Left, 1- Right
    [SerializeField] GameObject platform;
    [SerializeField] GameObject startingNode;
    [SerializeField] GameObject endNode;
    [SerializeField] public List<GameObject> activePlatforms;
>>>>>>> Stashed changes
    [SerializeField] int rowWidth;
    public void SpawnNode(int width, /*optional*/ bool isHazard = false, int rowVal = 30)
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
<<<<<<< Updated upstream
        else{
            for(int i = -width; i <= width; i++){
                GameObject newNode = Instantiate(node,new Vector3(i,this.transform.position.y+.5f,this.transform.position.z), Quaternion.identity);
                nodeArray.Add(newNode);
=======
        else {
            //new rows only
            if (isHazard) {
                GameObject nodeLeft, nodeRight;
                nodeLeft = Instantiate(node, new Vector3(-width, this.transform.position.y + .5f, this.transform.position.z), Quaternion.identity);
                nodeRight = Instantiate(node, new Vector3(width, this.transform.position.y + .5f, this.transform.position.z), Quaternion.identity);
                nodeLeft.GetComponent<Node>().targetNode = nodeRight;
                nodeRight.GetComponent<Node>().targetNode = nodeLeft;
                nodeArray.Add(nodeRight);
                nodeArray.Add(nodeLeft);
                if(rowType != Biome.Type.water)
                {

                    hazardStart = Random.Range(0, 2);
                }
                endNode = null;
                string side;
                if(hazardStart == 0)
                {
                    endNode = nodeArray[1];
                    side = "left";
                }
                else
                {
                    endNode = nodeArray[0];
                    side = "right";
                }
                startingNode = nodeArray[hazardStart];
                int delay = Random.Range(3,5);
                int speed = Random.Range(1,4);
                if(rowType == Biome.Type.water)
                {
                    speed = Random.Range(2, 5);
                }


                StartCoroutine(SendPlatforms(side,delay,speed));

            }
            else {
                for (int i = -width; i <= width; i++) {
                    GameObject newNode = Instantiate(node, new Vector3(i, this.transform.position.y + .5f, this.transform.position.z), Quaternion.identity);
                    bool isWall;
                    if (i < -12 || i > 12 || isBackWall) {
                        isWall = true;
                    }
                    else {
                        isWall = false;
                    }

                    newNode.GetComponent<Node>().SetNode(isWall, rowType);
                    nodeArray.Add(newNode);
                }
>>>>>>> Stashed changes
            }
        }

        foreach (GameObject node in nodeArray)
        {
            node.transform.parent = this.transform;
        }

    }
}
