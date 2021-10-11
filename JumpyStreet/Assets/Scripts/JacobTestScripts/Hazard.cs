using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject endNode;
    public float speed;
    public string startingSide;
    private void Awake()
    {
        Debug.Log("New Platform created");
    }

    public void SetInfo(GameObject node, float MoveSpeed, string side = "")
    {
        speed = MoveSpeed;
        endNode = node;
        startingSide = side;
        if(side == "left"){
            Vector3 newRotation = new Vector3(0,90,90);
            this.gameObject.transform.rotation = Quaternion.Euler(0,90,90);

        }
        else if (side == "right"){
            Vector3 newRotation = new Vector3(0,90,-90);
            this.gameObject.transform.rotation = Quaternion.Euler(0,90,-90);
        }
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, endNode.transform.position, speed * Time.deltaTime);
    }
    private void Update()
    {
        Vector3 distance = (this.transform.position - endNode.transform.position);

        if (distance.magnitude <= .5f)
        {
            Destroy(this.gameObject);
        }
    }
}
