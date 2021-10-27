//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Brennan Sullivan & Jacob Coleman
//Section: 2021FA.SGD.285.2141
//Instructor: Aurore Wold
//Date: 9/13/2021
/////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Other Hazard Node")]
    public GameObject targetNode = null;

    [Header("Obstical List")]
    [SerializeField] List<Group> selectorGroup;
    [SerializeField] GameObject currentObject;
    public int percentToSpawn;
    [SerializeField] int targetSpawn;

    [Header("Node Information")]
    [SerializeField] Biome.Type nodeBiome;
    public bool isWall;
    public GameObject child;

    private bool freshNode = true;
    private void Start()
    {
        
    }
    private void Awake()
    {

        //sets percent to spawn
        if (!freshNode)
        {
            if (isWall)
            {
                percentToSpawn = 110; //always spawn wall
            }
            else
            {
                percentToSpawn = Random.Range(1, 101); // used to spawn an obstical later
            }

            SelectAndSpawnObject();
        }
    }

    public void SetNode(bool wallNode, Biome.Type biome)
    {
        isWall = wallNode;
        nodeBiome = biome;
        freshNode = false; // Stops percent to spawn from being set twice
        //sets when spawned and again when awake
        if (isWall)
        {
            percentToSpawn = 110;
        }
        else
        {
            percentToSpawn = Random.Range(1, 101); // used to spawn an obstical later
        }
        SelectAndSpawnObject();
    }

    public void SelectAndSpawnObject()
    {
        percentToSpawn = 0;
        if (child != null) {
            print("killing the child");
            Destroy(child); 
        }

        if (isWall) {
            percentToSpawn = 110; //always spawn wall
        }
        else {
            percentToSpawn = Random.Range(1, 101); // used to spawn an obstical later
        }

        int objectListLength = selectorGroup.ToArray().Length;
        Group[] objectList = selectorGroup.ToArray();
        int objectSelector;

        //will it spawn an object
        if(percentToSpawn > targetSpawn)
        {
            objectSelector = Random.Range(0, objectListLength);
            currentObject = objectList[objectSelector].Object;
            //checks for biome specific objects
            while (objectList[objectSelector].BiomeType != nodeBiome)
            {
                objectSelector = Random.Range(0, objectListLength);
                currentObject = objectList[objectSelector].Object;
            }
            //spawns object

            int randomRotation = Random.Range(1, 5);
            float rotVal = 0;
            switch(randomRotation){
                case 1:
                    rotVal = 0f;
                    break;
                case 2:
                    rotVal = 90f;
                    break;
                case 3:
                    rotVal = 180f;
                    break;
                case 4:
                    rotVal = 270f;
                    break;
            }




            Vector3 spawnLocation = new Vector3(this.transform.position.x, this.transform.position.y + objectList[objectSelector].Offset, this.transform.position.z);
            Vector3 spawnRotation = new Vector3(this.transform.rotation.x + objectList[objectSelector].RotationOffset, this.transform.rotation.y + rotVal, this.transform.rotation.z);
            child = Instantiate(currentObject, spawnLocation, Quaternion.Euler(spawnRotation));
            child.transform.parent = this.transform;
        }
        else
        {
            //spawn empty;
            currentObject = null;
        }
    }
}
