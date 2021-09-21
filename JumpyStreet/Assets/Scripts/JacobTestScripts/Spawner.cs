using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Biome : Spawner{
    [SerializeField] string _biomeName;
    [SerializeField] int _maxBiomeLength;

    public string BiomeName{
        get{ return _biomeName; }
        set{ _biomeName = value; }
    }
    public int MaxBiomeLength{
        get{ return _maxBiomeLength; }
        set{ _maxBiomeLength = value; }
    }

    public Biome( string biomeName, int biomeLength){
        BiomeName = biomeName;
        MaxBiomeLength = biomeLength;
    }

}



public class Spawner : MonoBehaviour
{
    public enum Type{ forest, road, water, desert };

    [Header("Base Objects")]
    [SerializeField] GameObject[] mainRowsObjects = new GameObject[4];//0-Forest 1-Water 2-Sand 3-Road
    [SerializeField] GameObject StartingZone;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] GameObject node;
    [SerializeField] int rowWidth;

    [Header("Biome Info")]
    [SerializeField] List<Biome> biomeList = new List<Biome>{new Biome("forest",0),
                                                             new Biome("desert",0), 
                                                             new Biome("water",0), 
                                                             new Biome("road",0)};
    [SerializeField] string previousBiome;
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
        //CreateStartingZone();
        //Instantiate(StartingZone,startingZoneLocation,Quaternion.identity);
        
    }

    private void ChooseBiome(){
        int biomeSelector = Random.Range(1,5);
        previousBiome = currentBiome;
        switch(biomeSelector){
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }




    }


    public void SpawnRow(float offsetOverride = 0){
        if(remainingBiome == 0){
            ChooseBiome();
        }
        float zOffset;  
        if(offsetOverride == 0){
            zOffset  = 1f;
        }
        else{
            zOffset = offsetOverride;
        }
        int x=1;
        Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
        GameObject newRow = Instantiate(mainRowsObjects[x], rowSpawn, Quaternion.identity);
        activeRows.Add(newRow);
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
                //nodeArray.Add(newNode);
                //int nodeLength = nodeArray.ToArray().Length;
                //creates node class attached to he object
                //newNode.GetComponent<NodeHandler>().CreateNode(nodeLength);
            
            }
            zOffset++;
        }
    }
}
