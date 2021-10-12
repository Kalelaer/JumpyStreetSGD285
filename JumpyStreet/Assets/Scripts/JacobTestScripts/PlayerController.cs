using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Spawner spawner;
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
    private string raycastReturn;
    [SerializeField] int currentRow;
    [SerializeField] int currentNode;
    private bool onPlatform;
    

    // Start is called before the first frame update
    void Start()
    {
        spawner = masterSpawner.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceMove += Time.deltaTime;
        if(timeSinceMove > moveDelay) {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) {
                StartCoroutine(PerformHop());
                if(spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.desert) {
                    if (onPlatform) {
                        SetCurrentNodeFromX(); ;
                    }
                    if(spawner.activeRows[currentRow + 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null) {
                        if (onPlatform) {
                            this.transform.parent = null;
                            onPlatform = false;
                        }
                        MoveForward();
                        
                    }
                }else if (spawner.activeRows[currentRow + 1].GetComponent<Row>().rowType == Biome.Type.water) {
                    if(IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1))) {
                        onPlatform = true;
                        transform.SetParent(newPlatform.transform);
                        MoveForward();
                    }
                    else {
                        //fall and die?
                    }
                }
                else {
                    if (onPlatform) {
                        this.transform.parent = null;
                        onPlatform = false;
                        SetCurrentNodeFromX();
                    }
                    MoveForward();
                }
                timeSinceMove = 0;
            }
            if(Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)) {
                StartCoroutine(PerformHop());
                if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desert) {

                    if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode - 1].GetComponent<Node>().child == null) {
                        MoveLeft();
                    }
                }
                else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.water) {
                    if (IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1))) {
                        onPlatform = true;
                        if (newPlatform.transform != this.transform.parent) {
                            transform.SetParent(newPlatform.transform);
                        }
                        MoveLeft();
                    }
                    else {
                        //fall and die?
                    }
                }
                else {
                    MoveLeft();
                }
                timeSinceMove = 0;
            }
            if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)) {
                StartCoroutine(PerformHop());
                if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.desert) {

                    if (spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode + 1].GetComponent<Node>().child == null) {
                        MoveRight();
                    }
                }
                else if (spawner.activeRows[currentRow].GetComponent<Row>().rowType == Biome.Type.water) {
                    if (IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1))) {
                        onPlatform = true;
                        if (newPlatform.transform != this.transform.parent) {
                            transform.SetParent(newPlatform.transform);
                        }
                        MoveRight();
                    }
                    else {
                        //fall and die?
                    }
                }
                else {
                    MoveRight();
                }
                timeSinceMove = 0;
            }
            if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) {
                StartCoroutine(PerformHop());
                if (spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.forest || spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.desert) {
                    if (onPlatform) {
                        SetCurrentNodeFromX(); ;
                    }
                    if (spawner.activeRows[currentRow - 1].GetComponent<Row>().nodeArray[currentNode].GetComponent<Node>().child == null) {
                        if (onPlatform) {
                            this.transform.parent = null;
                            onPlatform = false;
                            SetCurrentNodeFromX();
                        }
                        MoveBackwards();
                    }
                }
                else if (spawner.activeRows[currentRow - 1].GetComponent<Row>().rowType == Biome.Type.water) {
                    if (IsPlatform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1))) {
                        onPlatform = true;
                        transform.SetParent(newPlatform.transform);
                        MoveBackwards();
                    }
                    else {
                        //fall and die?
                    }
                }
                else {
                    if (onPlatform) {
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

    public void SpawnPlayerModel() {
        this.transform.position = new Vector3(spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.x, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.y + modelYOffest, spawner.activeRows[currentRow].GetComponent<Row>().nodeArray[currentNode].transform.position.z);

        playerCharacterModel = Instantiate(playerCharacterPrefab, this.transform.position, Quaternion.identity, this.transform);
        playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    private IEnumerator PerformHop() { // probably just needs to be an animation
        Animator anim = playerModel.GetComponent<Animator>();
        anim.SetBool("isHopping", true);
        yield return new WaitForSeconds(moveDelay);
        anim.SetBool("isHopping", false);
        yield return null;
    }

    private void MoveForward() {
        Vector3 forwardPosition;
        if (backwardsMovementCount == 0) {
            forwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            if (onPlatform) {
                forwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
            }
            spawner.MoveRows();
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
        StartCoroutine(MoveTowardsPosition(leftPosition));
    }

    private void MoveRight() {
        Vector3 rightPosition = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
        currentNode++;
        StartCoroutine(MoveTowardsPosition(rightPosition));
    }

    private void MoveBackwards() {
        Vector3 backwardPosition;
        if (backwardsMovementCount < 3) {
            backwardPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
            backwardsMovementCount++;
            currentRow--;
            StartCoroutine(MoveTowardsPosition(backwardPosition));
        }
        else {
            //death??
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

    private bool IsNode(Vector3 castPoint) {
        bool isGround = false;
        //RaycastHit hit;
        Ray ray = new Ray(castPoint, -transform.up);
        
        return isGround;
    }

    private bool IsPlatform(Vector3 castPoint) {
        bool isPlatform = false;
        RaycastHit hit;
        Ray ray = new Ray(castPoint, -transform.up);
        if(Physics.Raycast(ray, out hit, 3f)) {
            if (hit.collider.gameObject.CompareTag("platform")) {
                isPlatform = true;
                newPlatform = hit.collider.gameObject;
            }
        }
        return isPlatform;
    }

    private IEnumerator FaceForward() {
        while (playerCharacterModel.transform.rotation != Quaternion.Euler(-90f, 0f, 0f)) {
            playerCharacterModel.transform.rotation = Quaternion.Euler(-90f, playerCharacterModel.transform.rotation.y + 1, 0f);
            yield return new WaitForFixedUpdate();
        }
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
            this.transform.position = new Vector3(transform.position.x, transform.position.y + modelYOffest, spawner.activeRows[currentRow].GetComponent<Row>().transform.position.z);
        }
    }
}
