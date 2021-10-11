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
                MoveForward();
                timeSinceMove = 0;
            }
        }
        
    }

    public void SpawnPlayerModel() {
        this.transform.SetParent(spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform);
        this.transform.position = new Vector3(spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform.position.x, spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform.position.y + modelYOffest, spawner.activeRows[21].GetComponent<Row>().nodeArray[15].transform.position.z);

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
        if (backwardsMovementCount == 0) {
            spawner.MoveRows();
        }
    }

    private bool IsNode(Transform castPoint) {
        bool isGround = false;
        return isGround;
    }

    private bool IsPlatform(Transform castPoint) {
        bool isPlatform = false;
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
}
