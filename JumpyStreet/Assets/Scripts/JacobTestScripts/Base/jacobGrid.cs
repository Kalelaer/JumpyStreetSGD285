using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jacobGrid : MonoBehaviour
{

    private NodeHandler nh;

    [SerializeField] GameObject masterSpawner;
    [SerializeField] GameObject rowLayout;
    [SerializeField] GameObject node;
    [SerializeField] GameObject[] rowSelector;
    [SerializeField] List<GameObject> nodeArray;
    [SerializeField] List<GameObject> rowList;
    [SerializeField] int basePercentToSpawn;
    [SerializeField] int percentToSpawn;
    [SerializeField] int rowIndex = 0;
    [SerializeField] int maxRows;
    [SerializeField] int startingNodeX = -15;
    [SerializeField] int endNodeX = 15;
    [SerializeField] bool isPlaying = false;
    public int difOffset;


    // Start is called before the first frame update
    void Start()
    {
        if(percentToSpawn == 0)
        {
            percentToSpawn = basePercentToSpawn;
        }
        isPlaying = true;
        masterSpawner = this.gameObject;
        StartCoroutine( SpawnRow());
    }

    IEnumerator SpawnRow()
    {

        while (isPlaying)
        {
            float zOffset = 1f;
            
            for (int i = 0; i < maxRows; i++)
            {
                int rowRandom = Random.Range(1, 7);
                if (rowRandom <= 2)
                {
                    rowLayout = rowSelector[1];
                }
                else
                {
                    rowLayout = rowSelector[0];
                }

                //Spawns the rows    
                Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
                GameObject newRow = Instantiate(rowLayout, rowSpawn, Quaternion.identity);
                //sets the row number
                newRow.GetComponent<RowHandler>().SetRow(i);
                rowList.Add(newRow);
                GameObject[] rowArray = rowList.ToArray();

                for (float x = startingNodeX; x <= endNodeX; x += 1f)
                {
                    if (rowRandom > 2)
                    {
                        if (x < -10 || x > 10)
                        {
                            percentToSpawn = 110;
                        }
                        else
                        {
                            percentToSpawn = basePercentToSpawn + difOffset;
                            print(percentToSpawn);
                        }

                        //spawns the nodes
                        Vector3 spawner = new Vector3(x, newRow.transform.position.y + .5f, (newRow.transform.position.z));
                        GameObject newNode = Instantiate(node, spawner, Quaternion.identity);
                        //sets the node as a child of the row
                        newNode.transform.parent = rowArray[rowIndex].transform;
                        //spawns the object on the node
                        newNode.GetComponent<NodeHandler>().SpawnObject(percentToSpawn);
                        nodeArray.Add(newNode);
                        int nodeLength = nodeArray.ToArray().Length;
                        //creates node class attached to he object
                        newNode.GetComponent<NodeHandler>().CreateNode(nodeLength);
                    }
                }
                if (i > 10)
                {
                    difOffset += 3;
                }

                rowIndex++;
                zOffset += 1;
                yield return new WaitForSeconds(.5f);
            }
            isPlaying = false;
        }
    }
}
