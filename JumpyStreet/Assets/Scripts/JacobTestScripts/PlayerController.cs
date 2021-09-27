using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] GameObject masterSpawner;
    [SerializeField] int backwardsMovementCount = 0;
    [SerializeField] GameObject playerCharacter;
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

    private void SpawnPlayer() {

    }

    private void PerformHop() {

    }

    private bool IsGround() {
        bool isGround = false;
        return isGround;
    }

    private bool IsPlatform() {
        bool isPlatform = false;
        return isPlatform;
    }
}
