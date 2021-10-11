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
    [SerializeField] public List<GameObject> activePlatforms;
    [SerializeField] int rowWidth;
    [SerializeField] List<GameObject> carHazards;
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
            }
        }

        foreach (GameObject node in nodeArray)
        {
            node.transform.parent = this.transform;
        }

    }
    public IEnumerator SendPlatforms(string side, /*optional*/ float timeDelay = 2f, float speed = 2f)
    {
        GameObject car = null;
         if (rowType == Biome.Type.road){
            int carHazardsLength = carHazards.Count;
            int selector = Random.Range(0,carHazardsLength);
            print(selector);
            Debug.Log("Choosing Car");
            car = carHazards[selector];
        }
        while (rowValue > 0)
        {
            if (rowType == Biome.Type.water)
            {
                activePlatforms.Add(Instantiate(platform, startingNode.transform.position, Quaternion.identity));
                int index = activePlatforms.Count - 1;
                activePlatforms[index].transform.parent = this.transform;
                activePlatforms[index].GetComponent<Hazard>().SetInfo(endNode, speed);
            }

            if (rowType == Biome.Type.road)
            {
                Vector3 newRotation = new Vector3(90,0,45);
                Vector3 newScale = new Vector3(0,0,0);
                if(side == "left"){
                    newRotation = new Vector3(-90,180,90);
                    newScale = new Vector3(0.6f,0.025f,0.66f);
                    //this.gameObject.transform.rotation = Quaternion.Euler(0,90,90);
                }
                else if (side == "right"){
                    newRotation = new Vector3(90,45,0);
                    //this.gameObject.transform.rotation = Quaternion.Euler(0,90,-90);
                }
                //activePlatforms.Add(Instantiate(car, startingNode.transform.position, Quaternion.Euler(newRotation)));
                activePlatforms.Add(Instantiate(car, startingNode.transform.position, Quaternion.identity));
                int index = activePlatforms.Count - 1;
                activePlatforms[index].transform.parent = this.transform;
                //activePlatforms[index].transform.rotation = Quaternion.Euler(newRotation);
                activePlatforms[index].transform.localScale = new Vector3(1.5f,0.75f,0.2f);//resets the scales, 0.03125f
                activePlatforms[index].GetComponent<Hazard>().SetInfo(endNode, speed, side);
            }

            yield return new WaitForSeconds(timeDelay);
        }
        yield return null;
    }


}