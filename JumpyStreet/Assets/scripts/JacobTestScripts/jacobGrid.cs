using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class jacobGrid : MonoBehaviour
{

    [SerializeField] GameObject masterSpawner;
    [SerializeField] GameObject rowLayout;
    [SerializeField] GameObject node;
    [SerializeField] List<GameObject> nodeArray;
    [SerializeField] List<GameObject> rowList;
    [SerializeField] int rowIndex = 0;
    [SerializeField] int maxRows;
    [SerializeField] float startingNodeX = -14.5f;
    [SerializeField] float endNodeX = 14.5f;
    [SerializeField] bool isPlaying = false;
    [SerializeField] rowClass[] rc;


    // Start is called before the first frame update
    void Start()
    {
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
                
                Vector3 rowSpawn = new Vector3(masterSpawner.transform.position.x, masterSpawner.transform.position.y - 0.25f, masterSpawner.transform.position.z - zOffset);
                GameObject newRow = Instantiate(rowLayout, rowSpawn, Quaternion.identity);
                rowList.Add(newRow);
                GameObject[] rowArray = rowList.ToArray();

                for (float x = startingNodeX; x <= endNodeX; x += 1f)
                {

                    Vector3 spawner = new Vector3(x, newRow.transform.position.y + .5f, (newRow.transform.position.z));
                    GameObject newNode = Instantiate(node, spawner, Quaternion.identity);
                    newNode.transform.parent = rowArray[rowIndex].transform;
                    nodeArray.Add(newNode);
                    Debug.Log("spawned a new node");

                }
                rowIndex++;
                zOffset += 1;
                yield return new WaitForSeconds(.5f);
            }
            isPlaying = false;
        }
    }
}
