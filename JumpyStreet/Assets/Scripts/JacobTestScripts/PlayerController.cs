using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] int backwardsMovementCount = 0;
    [SerializeField] GameObject playerCharacterPrefab;
    [SerializeField] GameObject playerCharacter;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        spawner = masterSpawner.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)){
            if(backwardsMovementCount == 0){
                spawner.MoveRows();
            }
        }
    }

    public void SpawnPlayer() {
        Vector3 spawnLocation = new Vector3(spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform.position.x, 1.7f, spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform.position.z);
        playerCharacter = Instantiate(playerCharacterPrefab, spawnLocation, Quaternion.identity);
        playerCharacter.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    private void PerformHop() {

    }

    private bool IsNode() {
        bool isGround = false;
        return isGround;
    }

    private bool IsPlatform() {
        bool isPlatform = false;
        return isPlatform;
    }

    private IEnumerator FaceForward() {
        yield return null;
    }

    private IEnumerator FaceBackwarcd() {
        yield return null;
    }

    private IEnumerator FaceLeft() {
        yield return null;
    }

    private IEnumerator FaceRight() {
        yield return null;
    }
}
