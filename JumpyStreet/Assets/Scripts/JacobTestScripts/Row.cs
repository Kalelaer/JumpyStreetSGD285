using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Biome.Type rowType = new Biome.Type();
    public int rowValue;
    [SerializeField] GameObject node;
    [SerializeField] public List<GameObject> nodeArray;
    [SerializeField] int hazardStart; //0 - Left, 1- Right
    [SerializeField] GameObject platform;
    [SerializeField] GameObject startingNode;
    [SerializeField] GameObject endNode;
    [SerializeField] List<GameObject> activePlatforms;
    [SerializeField] int rowWidth;
    public Vector3 targetPos;
    public bool canMove = false;
    public bool isBack;

    private void FixedUpdate()
    {
        
    }
    public void SpawnNode(int width, /*optional*/ bool isHazard = false, int rowVal = 30, bool isBackWall = false)
    {
        rowValue = rowVal;
        rowWidth = width;
        if (nodeArray.ToArray().Length >= 31) {
            int nodeLength = nodeArray.ToArray().Length;

            for (int i = 0; i<nodeLength; i++) {
                //bool wall = nodeArray[i].GetComponent<Node>().isWall;

                /*if (i < -12 || i > 12 || isBackWall) {
                    nodeArray[i].GetComponent<Node>().isWall = true;
                }
                else {
                    nodeArray[i].GetComponent<Node>().isWall = false;
                }*/
                //nodeArray[i].GetComponent<Node>().SetNode(wall, rowType);
                nodeArray[i].GetComponent<Node>().SelectAndSpawnObject();            
            }

        }
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
                hazardStart = Random.Range(0, 2);
                if(hazardStart == 0)
                {
                    endNode = nodeArray[1];
                }
                else
                {
                    endNode = nodeArray[0];
                }
                startingNode = nodeArray[hazardStart];
                StartCoroutine(SendPlatforms());

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
            }
        }

        foreach (GameObject node in nodeArray)
        {
            node.transform.parent = this.transform;
        }

    }
    private IEnumerator SendPlatforms()
    {
        while (true)
        {
            if (rowType == Biome.Type.water)
            {
                activePlatforms.Add(Instantiate(platform, startingNode.transform.position, Quaternion.identity));
                int index = activePlatforms.Count - 1;
                activePlatforms[index].GetComponent<Hazard>().SetInfo(endNode, 2f);
            }
            yield return new WaitForSeconds(2f);
        }

    }


}