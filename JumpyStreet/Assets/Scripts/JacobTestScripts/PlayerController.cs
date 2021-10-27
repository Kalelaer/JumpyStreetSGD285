using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] int backwardsMovementCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawner = masterSpawner.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w")){
            if(backwardsMovementCount == 0){
                spawner.MoveRows();
            }
        }
    }

<<<<<<< Updated upstream
=======
    private void MoveLeft() {
        
        Vector3 leftPosition = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
        currentNode--;
        StartCoroutine(FaceLeft());
        StartCoroutine(MoveTowardsPosition(leftPosition));
    }

    private void MoveRight() {
        Vector3 rightPosition = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
        currentNode++;
        StartCoroutine(FaceRight());
        StartCoroutine(MoveTowardsPosition(rightPosition));
    }

    private void MoveBackwards() {
        Vector3 backwardPosition;
        if (backwardsMovementCount < 3) {
            backwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
            backwardsMovementCount++;
            currentRow--;
            StartCoroutine(FaceBackward());
            StartCoroutine(MoveTowardsPosition(backwardPosition));
        }
        else {
            //death??
            Death();
        }
    }
    
    private IEnumerator MoveTowardsPosition(Vector3 newPos) {
        bool traveling = true;
        while (traveling) {
            transform.position = Vector3.MoveTowards(transform.position, newPos, 8 * Time.deltaTime);
            if(Vector3.Distance(transform.position, newPos) < .1) {
                traveling = false;
                transform.position = newPos;
                
            }
            yield return new WaitForEndOfFrame();
        }
        MoveToCurrentPosition();
        yield return null;
    }

    /*private bool IsNode(Vector3 castPoint) {
        bool isGround = false;
        RaycastHit hit;
        Ray ray = new Ray(castPoint, -transform.up);
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.collider.gameObject.CompareTag("node"))
            {
                isGround = true;
                newNode = hit.collider.gameObject;
            }
        }
        return isGround;
    }*/

    private bool IsPlatform(Vector3 castPoint) {
        bool isPlatform = false;
        if(newPlatform != null)
        {
            newPlatform.GetComponent<Hazard>().isChild = false;
        }
        RaycastHit hit;
        Ray ray = new Ray(castPoint, -transform.up);
        if(Physics.Raycast(ray, out hit, 8f)) {
            if (hit.collider.gameObject.CompareTag("platform")) {
                isPlatform = true;
                newPlatform = hit.collider.gameObject;
                newPlatform.GetComponent<Hazard>().isChild = true;
            }
        }
        return isPlatform;
    }

    private IEnumerator FaceForward() {
        while (playerCharacterModel.transform.rotation != Quaternion.Euler(-90f, 0f, 0f)) {
            //Debug.Log("FaceForward");
            playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            yield return new WaitForFixedUpdate();
            
        }
        
        yield return null;
    }

    private IEnumerator FaceBackward() {
        
        while (playerCharacterModel.transform.rotation != Quaternion.Euler(-90f, 180f, 0f))
        {
            //Debug.Log("FaceBackwards");
            playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
            yield return new WaitForFixedUpdate();
        }
        
        yield return null;
    }

    private IEnumerator FaceLeft() {
        
        while (playerCharacterModel.transform.rotation != Quaternion.Euler(-90f, 270f, 0f))
        {
            //Debug.Log("FaceLeft");
            playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 270f, 0f);
            yield return new WaitForFixedUpdate();
            
        }
        
        yield return null;
    }

    private IEnumerator FaceRight() {
        
        while (playerCharacterModel.transform.rotation != Quaternion.Euler(-90f, 90f, 0f))
        {
            //Debug.Log("FaceRight");
            playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
        }
        
        yield return null;
    }

    private void SetCurrentNodeFromX() {
        int roundedX = Mathf.RoundToInt(this.transform.position.x);
        switch (roundedX) {
            case -14:
                currentNode = 1;
                break;
            case -13:
                currentNode = 2;
                break;
            case -12:
                currentNode = 3;
                break;
            case -11:
                currentNode = 4;
                break;
            case -10:
                currentNode = 5;
                break;
            case -9:
                currentNode = 6;
                break;
            case -8:
                currentNode = 7;
                break;
            case -7:
                currentNode = 8;
                break;
            case -6:
                currentNode = 9;
                break;
            case -5:
                currentNode = 10;
                break;
            case -4:
                currentNode = 11;
                break;
            case -3:
                currentNode = 12;
                break;
            case -2:
                currentNode = 13;
                break;
            case -1:
                currentNode = 14;
                break;
            case 0:
                currentNode = 15;
                break;
            case 1:
                currentNode = 16;
                break;
            case 2:
                currentNode = 17;
                break;
            case 3:
                currentNode = 18;
                break;
            case 4:
                currentNode = 19;
                break;
            case 5:
                currentNode = 20;
                break;
            case 6:
                currentNode = 21;
                break;
            case 7:
                currentNode = 22;
                break;
            case 8:
                currentNode = 23;
                break;
            case 9:
                currentNode = 24;
                break;
            case 10:
                currentNode = 25;
                break;
            case 11:
                currentNode = 26;
                break;
            case 12:
                currentNode = 27;
                break;
            case 13:
                currentNode = 28;
                break;
            case 14:
                currentNode = 29;
                break;
        }
    }

    private void MoveToCurrentPosition() {
        if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desert) {
            this.transform.position = new Vector3(spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.x, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.y + modelYOffest, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.z);
        }
        else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.water && onPlatform) {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, spawner.activeRows[currentRow].GetComponent<Row>().transform.position.z);
        }
        else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.road || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desertHazard || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forestHazard)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, spawner.activeRows[currentRow].GetComponent<Row>().transform.position.z);
        }
    }

    public void Death()
    {
        List<GameObject> rowList = new List<GameObject>();
        int score = menuController.GetComponent<GameMenuController>().playerScore;
        int highscore = PlayerPrefs.GetInt("highscore");
        PlayerPrefs.SetInt("score",score);
        if (score > highscore) {
            PlayerPrefs.SetInt("highscore",score);
        }
        Debug.Log($"Score: {score}, Highscore: {highscore}");

        rowList.AddRange( GameObject.FindGameObjectsWithTag("row"));
        foreach(GameObject row in rowList)
        {
            row.GetComponent<Row>().StopPlatformSpawn();
            if(row.GetComponent<Row>().rowType == Biome.Type.road || row.GetComponent<Row>().rowType == Biome.Type.water || row.GetComponent<Row>().rowType == Biome.Type.forestHazard || row.GetComponent<Row>().rowType == Biome.Type.desertHazard)
            {
                foreach( GameObject platform  in row.GetComponent<Row>().activePlatforms)
                {
                    if (platform != null)
                    {
                        platform.SetActive(false);
                    }
                }
            }
        }
        menuController.GetComponent<GameMenuController>().soundPlayer.PlayOneShot(menuController.GetComponent<GameMenuController>().deathSound);
        menuController.GetComponent<GameMenuController>().LoseGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hazard"))
        {
            Death();
        }
    }

    private void SetUpSounds() {

    }
>>>>>>> Stashed changes
}
