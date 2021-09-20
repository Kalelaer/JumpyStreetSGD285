using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Base Objects")]
    [SerializeField] GameObject[] mainRowsObjects = new GameObject[4];//0-Forest 1-Water 2-Sand 3-Road
    [SerializeField] GameObject StartingZone;
    [SerializeField] GameObject masterSpawner;

    [Header("Biome Info")]
    [SerializeField] string currentBiome;
    [SerializeField] int remainingBiome;

    [Header("Active Rows")]
    [SerializeField] List<GameObject> activeRows;//main rows in play
    
    [Header("Inactive Rows")]
    [SerializeField] int maxForest;
    [SerializeField] List<GameObject> forestRows;
    [SerializeField] int maxRiver;
    [SerializeField] List<GameObject> riverRows;
    [SerializeField] int maxRoad;
    [SerializeField] List<GameObject> roadRows;
    [SerializeField] int maxDesert;
    [SerializeField] List<GameObject> desertRows;

    private void Start()
    {
        Vector3 startingZoneLocation = new Vector3(0,1,-12);
        CreateStartingZone();
        //Instantiate(StartingZone,startingZoneLocation,Quaternion.identity);
        
    }

    public void SpawnRow(){
        activeRows.Add(Instantiate(row)
    }

    //Use this to grab a prefab of the starting zone
    public void CreateStartingZone(){
        masterSpawner = this.gameObject;
        int percentToSpawn;
        float zOffset = 1f;
        //Spawn Starting zone
        for(int i = 0; i < 10; i++){
                //Spawns the rows    
                Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
                GameObject newRow = Instantiate(mainRowsObjects[0], rowSpawn, Quaternion.identity);
                
            for (float x = -rowWidth; x <= rowWidth; x += 1f){

                if (x < -10 || x > 10 || i >=5)
                {   //Spawn Walls
                    percentToSpawn = 110;                        
                }
                else
                {   //be empty
                    percentToSpawn = -1;
                }

                //spawns the nodes
                Vector3 spawner = new Vector3(x, newRow.transform.position.y + .5f, (newRow.transform.position.z));
                GameObject newNode = Instantiate(node, spawner, Quaternion.identity);
                //sets the node as a child of the row
                newNode.transform.parent = newRow.transform;
                //spawns the object on the node
                newNode.GetComponent<NodeHandler>().SpawnObject(percentToSpawn);
                nodeArray.Add(newNode);
                int nodeLength = nodeArray.ToArray().Length;
                //creates node class attached to he object
                newNode.GetComponent<NodeHandler>().CreateNode(nodeLength);
            
            }
            zOffset++;
        }
    }
}
