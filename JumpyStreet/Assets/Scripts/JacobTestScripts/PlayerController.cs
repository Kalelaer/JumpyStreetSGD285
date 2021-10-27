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
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] GameObject menuController;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] int backwardsMovementCount = 0;
    [SerializeField] GameObject playerCharacterPrefab;
    public float modelYOffest;
    [SerializeField] GameObject playerModel;
    private GameObject playerCharacterModel;
    [SerializeField] float moveDelay;
    private float timeSinceMove;
    private GameObject newPlatform;
    private GameObject newNode;
    [SerializeField] int currentRow;
    [SerializeField] int currentNode;
    private bool onPlatform;


    // Start is called before the first frame update
    void Awake()
    {
        spawner = masterSpawner.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameMenuController.gMC.paused)
        {
            if (timeSinceMove > moveDelay)
            {
                if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    StartCoroutine(PerformHop());
                    if (spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.desert)
                    {
                        if (onPlatform)
                        {
                            SetCurrentNodeFromX();
                        }
                        if (spawner.activeRows[currentRow + 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
                        {
                            if (onPlatform)
                            {
                                this.transform.parent = null;
                                newPlatform.GetComponent<Hazard>().isChild = false;
                                onPlatform = false;
                            }
                            MoveForward();

                        }
                    }
                    else if (spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.water)
                    {
                        if (IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)))
                        {
                            onPlatform = true;
                            transform.SetParent(newPlatform.transform);
                            MoveForward();
                        }
                        else
                        {
                            //fall and die?
                            Death();
                        }
                    }
                    else
                    {
                        if (onPlatform)
                        {
                            this.transform.parent = null;
                            onPlatform = false;
                            SetCurrentNodeFromX();
                        }
                        MoveForward();
                    }
                    timeSinceMove = 0;
                }
                if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    StartCoroutine(PerformHop());
                    if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desert)
                    {

                        if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode - 1].GetComponent<Node>().child == null)
                        {
                            MoveLeft();
                        }
                    }
                    else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.water)
                    {
                        if (IsPlatform(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)))
                        {
                            onPlatform = true;
                            if (newPlatform.transform != this.transform.parent)
                            {
                                transform.SetParent(newPlatform.transform);
                            }
                            else
                            {
                                Debug.Log("Moving on current platform.");
                            }
                            MoveLeft();
                        }
                        else
                        {
                            //fall and die?
                            Death();
                        }
                    }
                    else
                    {
                        MoveLeft();
                    }
                    timeSinceMove = 0;
                }
                if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(PerformHop());
                    if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desert)
                    {

                        if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode + 1].GetComponent<Node>().child == null)
                        {
                            MoveRight();
                        }
                    }
                    else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.water)
                    {
                        if (IsPlatform(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)))
                        {
                            onPlatform = true;
                            if (newPlatform.transform != this.transform.parent)
                            {
                                transform.SetParent(newPlatform.transform);
                            }
                            else
                            {
                                Debug.Log("Moving on current platform.");
                            }
                            MoveRight();
                        }
                        else
                        {
                            //fall and die?
                            Death();
                        }
                    }
                    else
                    {
                        MoveRight();
                    }
                    timeSinceMove = 0;
                }
                if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(PerformHop());
                    if (spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.desert)
                    {
                        if (onPlatform)
                        {
                            SetCurrentNodeFromX(); ;
                        }
                        if (spawner.activeRows[currentRow - 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
                        {
                            if (onPlatform)
                            {
                                this.transform.parent = null;
                                newPlatform.GetComponent<Hazard>().isChild = false;
                                onPlatform = false;
                                SetCurrentNodeFromX();
                            }
                            MoveBackwards();
                        }
                    }
                    else if (spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.water)
                    {
                        if (IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)))
                        {
                            onPlatform = true;
                            transform.SetParent(newPlatform.transform);
                            MoveBackwards();
                        }
                        else
                        {
                            //fall and die?
                            Death();
                        }
                    }
                    else
                    {
                        if (onPlatform)
                        {
                            this.transform.parent = null;
                            onPlatform = false;
                            SetCurrentNodeFromX();
                        }
                        MoveBackwards();
                    }
                    timeSinceMove = 0;
                }
            }
        }
        timeSinceMove += Time.deltaTime;    }

    public void SpawnPlayerModel() {
        backwardsMovementCount = 0;
        if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
        {
            if (spawner.activeRows[currentRow + 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child != null && spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode - 1].GetComponent<Node>().child != null && spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode + 1].GetComponent<Node>().child != null && spawner.activeRows[currentRow - 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child != null && spawner.activeRows[currentRow + 2].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
            {
                currentRow++;
                currentRow++;
            }
        }
        else if (spawner.activeRows[currentRow + 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
        {
            currentRow++;
        }
        else if(spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode - 1].GetComponent<Node>().child == null)
        {
            currentNode--;
        }
        else if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode + 1].GetComponent<Node>().child == null)
        {
            currentNode++;
        }
        else if (spawner.activeRows[currentRow - 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null)
        {
            currentRow--;
        }
        else
        {

        }
        playerCharacterPrefab = Resources.Load<GameObject>($"Characters/{PlayerPrefs.GetString("Character")}");
        print(Resources.Load<GameObject>($"Characters/{PlayerPrefs.GetString("Character")}"));
        print(playerCharacterPrefab);
        this.transform.position = new Vector3(spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.x, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.y + modelYOffest, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.z);

        playerCharacterModel = Instantiate(playerCharacterPrefab, this.transform.position, Quaternion.identity, playerModel.transform);
        playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    private IEnumerator PerformHop() { // probably just needs to be an animation
        
        Animator anim = playerModel.GetComponent<Animator>();
        anim.SetBool("isHopping", true);
        menuController.GetComponent<GameMenuController>().soundPlayer.PlayOneShot(menuController.GetComponent<GameMenuController>().jumpSound);
        yield return new WaitForSeconds(moveDelay);
        anim.SetBool("isHopping", false);
        yield return null;

    }

    private void MoveForward() {
        Vector3 forwardPosition;
        StartCoroutine(FaceForward());
        if (backwardsMovementCount == 0) {
            forwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            if (onPlatform) {
                forwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
            }
            spawner.MoveRows();
            menuController.GetComponent<GameMenuController>().playerScore++;
        }
        else {
            forwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
            backwardsMovementCount--;
            currentRow++;
        }
        this.transform.position = forwardPosition;
        Invoke("MoveToCurrentPosition", 0.1f);

    }

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
}
