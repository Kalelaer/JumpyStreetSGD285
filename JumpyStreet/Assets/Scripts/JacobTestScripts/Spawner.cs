using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Base Objects")]
    [SerializeField] GameObject[] mainRowsObjects = new GameObject[4];//0-Forest 1-Water 2-Sand 3-Road
    [SerializeField] GameObject StartingZone;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] GameObject node;
    [SerializeField] int rowWidth;

    [Header("Biome Info")]
    [SerializeField] List<Biome> biomeList;
    [SerializeField] string previousBiome;
    [SerializeField] private bool previousBiomeHazard;
    [SerializeField] string currentBiome;
    [SerializeField] private bool currentBiomeHazard;
    [SerializeField] int remainingBiome;

    [Header("Active Rows")]
    public List<GameObject> activeRows;//main rows in play
    
    [Header("Inactive Rows")]
    [SerializeField] int maxForest;
    [SerializeField] List<GameObject> forestRows;
    [SerializeField] int maxRiver;
    [SerializeField] List<GameObject> riverRows;
    [SerializeField] int maxRoad;
    [SerializeField] List<GameObject> roadRows;
    [SerializeField] int maxDesert;
    [SerializeField] List<GameObject> desertRows;

    [Header("Row Spawn/Move Parameters")]
    [SerializeField] private float rowMoveSpeed;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        Vector3 startingZoneLocation = new Vector3(0,1,-12);
        //CreateStartingZone();
        //Instantiate(StartingZone,startingZoneLocation,Quaternion.identity);
        SpawnInitial();
    }


    public void MoveRows(){
        List<GameObject> placeholderList = new List<GameObject>{};
        foreach(GameObject row in activeRows){
            //moves the rows
            if(row.GetComponent<Row>().canMove == true) {
                row.GetComponent<Row>().targetPos = new Vector3(row.transform.position.x, row.transform.position.y, row.transform.position.z - 1f);
                row.GetComponent<Row>().canMove = false;
                //row.transform.position = new Vector3(row.transform.position.x, row.transform.position.y, row.transform.position.z-1f);
                StartCoroutine(MoveRowTowards(row));
            }
            
            row.GetComponent<Row>().rowValue--;

            //Row Removal
            if(row.GetComponent<Row>().rowValue <= 0){
                int nodeCount = row.GetComponent<Row>().nodeArray.Count;
                foreach(GameObject node in row.GetComponent<Row>().nodeArray) {
                    Debug.Log("Destroyin all the children");
                    node.GetComponent<Node>().percentToSpawn = 0;
                    Destroy(node.GetComponent<Node>().child);
                }
                if (row.GetComponent<Row>().isBack) {
                    Destroy(row);
                }
                else {
                    row.SetActive(false);
                    if (row.GetComponent<Row>().rowType == Biome.Type.forest) {
                        if (forestRows.ToArray().Length < maxForest) {
                            forestRows.Add(row);
                            
                        }
                        else {
                            Destroy(row);

                        }
                    }
                    else if (row.GetComponent<Row>().rowType == Biome.Type.desert) {
                        if (desertRows.ToArray().Length < maxDesert) {
                            desertRows.Add(row);
                        }
                        else {
                            Destroy(row);
                        }
                    }
                    else if (row.GetComponent<Row>().rowType == Biome.Type.water) {
                        if (riverRows.ToArray().Length < maxRiver) {
                            riverRows.Add(row);
                            foreach (GameObject platform in row.GetComponent<Row>().activePlatforms) {
                                Destroy(platform);
                            }
                        }
                        else {
                            Destroy(row);
                        }
                    }
                    else if (row.GetComponent<Row>().rowType == Biome.Type.road) {
                        if (roadRows.ToArray().Length < maxRoad) {
                            roadRows.Add(row);
                            foreach (GameObject platform in row.GetComponent<Row>().activePlatforms) {
                                Destroy(platform);
                            }
                        }
                        else {
                            Destroy(row);
                        }
                    }
                }
                
            }
            else{
                placeholderList.Add(row);
            }
        }
        activeRows.Clear();
        activeRows = placeholderList;
        SpawnRow(30);
    }

    private void ChooseBiome(){
        int biomeSelector = Random.Range(0,4);
        previousBiome = currentBiome;
        bool tryAgain = false;
        previousBiomeHazard = currentBiomeHazard;
        //first try
        currentBiome = biomeList[biomeSelector].BiomeName;
        remainingBiome = Random.Range(1,biomeList[biomeSelector].MaxBiomeLength+1);
        currentBiomeHazard = biomeList[biomeSelector].IsHazard;

        if(currentBiome == previousBiome || previousBiomeHazard == currentBiomeHazard){
            if(!tryAgain){//try once
                tryAgain = true;
                ChooseBiome();
                print("Trying again");
            }
            else{//if it happens again then dont try again
                tryAgain = false;
                print("failed to try again. moving on with the same biome");
            }
        }
        else{
            tryAgain = false;
        }
        print($" current biome {currentBiome}, Biome Length {remainingBiome}");
    }
    public void SpawnRow( int rowVal, /*optional*/ float offsetOverride = 1f){
        //increment player score here





        /////////////////////////////
        //When to chose a new biome
        if(remainingBiome == 0){
            ChooseBiome();
        }
        remainingBiome--;

        //row offset from spawner placement
        float zOffset = offsetOverride;  

        //Sets the object that needs to spawn
        GameObject objectToSpawn = null;
        switch(currentBiome){
            case "forest":
                objectToSpawn = mainRowsObjects[0];
                break;
            case "desert":
                objectToSpawn = mainRowsObjects[2];
                break;
            case "water":
                objectToSpawn = mainRowsObjects[1];
                break;
            case "road":
                objectToSpawn = mainRowsObjects[3];
                break;
        }

        //Spawns the Row //This can probably be combined with the function above
        Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
        GameObject newRow = null;
        bool oldRowInArray = false;
        switch (currentBiome) {
            case "forest":
                if(forestRows.Count > 0) {
                    newRow = forestRows[0];
                    forestRows.RemoveAt(0);
                    newRow.transform.position = rowSpawn;
                    oldRowInArray = true;
                }
                break;
            case "desert":
                if (desertRows.Count > 0) {
                    newRow = desertRows[0];
                    desertRows.RemoveAt(0);
                    newRow.transform.position = rowSpawn;
                    oldRowInArray = true;
                }
                break;
            case "water":
                if (riverRows.Count > 0) {
                    newRow = riverRows[0];
                    riverRows.RemoveAt(0);
                    newRow.transform.position = rowSpawn;
                    oldRowInArray = true;
                }
                break;
            case "road":
                if (roadRows.Count > 0) {
                    newRow = roadRows[0];
                    roadRows.RemoveAt(0);
                    newRow.transform.position = rowSpawn;
                    oldRowInArray = true;
                }
                break;
        }
        if (!oldRowInArray) {
            newRow = Instantiate(objectToSpawn, rowSpawn, Quaternion.identity);
        }

        newRow.GetComponent<Row>().targetPos = new Vector3(newRow.transform.position.x, newRow.transform.position.y, newRow.transform.position.z);
        newRow.SetActive(true);


        //spawns nodes
        if (currentBiome == "road" || currentBiome == "water" ){
            newRow.GetComponent<Row>().SpawnNode(rowWidth,true);
        }
        else{
            newRow.GetComponent<Row>().SpawnNode(rowWidth);
        }
        newRow.GetComponent<Row>().rowValue = rowVal;
        activeRows.Add(newRow);
        newRow.GetComponent<Row>().canMove = true;
    }


    private void SpawnInitial(){
        int rowValue = 11;
        for(int i=19;i>0;i--){
            SpawnRow(rowValue,i);
            rowValue++;
        }
        CreateStartingZone();
    }

    //Use this to grab a prefab of the starting zone
    private void CreateStartingZone(){
        masterSpawner = this.gameObject;
        float zOffset = 20f;
        //Spawn Starting zone
        int rowVal = 10;
        for(int i = 0; i < 10; i++){
            //Spawns the rows    
            Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
            GameObject newRow = Instantiate(mainRowsObjects[0], rowSpawn, Quaternion.identity);
            newRow.GetComponent<Row>().rowValue = rowVal;
            activeRows.Add(newRow);


            if (i > 6)
            {//spawn back wall
                newRow.GetComponent<Row>().SpawnNode(rowWidth, false, rowVal, true);
                newRow.GetComponent<Row>().isBack = true;
            }
            else
            {
                newRow.GetComponent<Row>().SpawnNode(rowWidth, false, rowVal, false);
            }
            rowVal--;
            zOffset++;
            newRow.GetComponent<Row>().canMove = true;
            newRow.GetComponent<Row>().targetPos = new Vector3(newRow.transform.position.x, newRow.transform.position.y, newRow.transform.position.z);
        }
        playerController.SpawnPlayer();
    }

    private IEnumerator MoveRowTowards(GameObject row) {
        float step = rowMoveSpeed * Time.deltaTime;
        bool rowIsMoved = false;
        while (!rowIsMoved) {
            if(row == null) {
                yield return null;
            }else if(row.transform.position != row.GetComponent<Row>().targetPos) {
                row.transform.position = Vector3.MoveTowards(row.transform.position, row.GetComponent<Row>().targetPos, step);
                if (Vector3.Distance(row.transform.position, row.GetComponent<Row>().targetPos) < 0.001f) {
                    row.transform.position = row.GetComponent<Row>().targetPos;
                    rowIsMoved = true;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        if(row != null) {
            row.GetComponent<Row>().canMove = true;
        }
        yield return null;
    }
}
