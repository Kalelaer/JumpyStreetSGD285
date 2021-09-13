using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrennanGridController : MonoBehaviour
{
    public static BrennanGridController gridController;
    [SerializeField] private GameObject horizontalRowHolder;
    [SerializeField] private GameObject horizontalRowPrefab;
    [SerializeField] private GameObject gridPiecePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> horizontalRows = new List<GameObject>();
    [SerializeField] private int rowsCreated;

    [SerializeField] private int rowsInFrontOfPlayer;
    [SerializeField] private int rowsBehindPlayer;
    [SerializeField] private int playerVLoc;


    private void Awake()
    {
        
        gridController = this;
        playerVLoc = 0;
        rowsCreated = 0;
        CreateStart();
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            OnPlayerMoveForward(playerVLoc);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            OnPlaerMoveBack(playerVLoc);
        }
    }

    private void CreateStart() {
        
        for (int numX = -3; numX < 4; numX++)
        {
            Vector3 rowSpot = new Vector3(numX, 0, 0);
            horizontalRows.Add(Instantiate(horizontalRowPrefab, rowSpot, Quaternion.identity, horizontalRowHolder.transform));
            rowsCreated++;
            horizontalRows[horizontalRows.Count - 1].GetComponent<BrennanHorizontalRow>().rowNumber = rowsCreated;

        }
    }

    public void OnPlayerMoveForward(int playerXLocation)
    {

        Vector3 rowSpot = new Vector3(playerXLocation + 4, 0, 0);
        horizontalRows.Add(Instantiate(horizontalRowPrefab, rowSpot, Quaternion.identity, horizontalRowHolder.transform));
        rowsCreated++;
        horizontalRows[horizontalRows.Count - 1].GetComponent<BrennanHorizontalRow>().rowNumber = rowsCreated;
        foreach (GameObject row in horizontalRows)
        {
            Vector3 rowNewSpot = row.transform.position;
            row.transform.position = new Vector3(rowNewSpot.x - 1, rowNewSpot.y, rowNewSpot.z);
        }
        GameObject rowToDelete = horizontalRows[0];
        horizontalRows.RemoveAt(0);
        Destroy(rowToDelete);
        playerVLoc++;
    }

    public void OnPlaerMoveBack(int playerXLocation)
    {
        Vector3 rowSpot = new Vector3(playerXLocation - 4, 0, 0);
        horizontalRows.Add(Instantiate(horizontalRowPrefab, rowSpot, Quaternion.identity, horizontalRowHolder.transform));
        rowsCreated--;
        foreach (GameObject row in horizontalRows)
        {
            Vector3 rowNewSpot = row.transform.position;
            row.transform.position = new Vector3(rowNewSpot.x + 1, rowNewSpot.y, rowNewSpot.z);
        }
        GameObject rowToDelete = horizontalRows[horizontalRows.Count - 1];
        horizontalRows.RemoveAt(horizontalRows.Count - 1);
        rowToDelete.SetActive(false);
        playerVLoc--;
    }
}
