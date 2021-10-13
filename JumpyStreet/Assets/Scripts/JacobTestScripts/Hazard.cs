using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject endNode;
    [SerializeField] List<GameObject> nodeList;
    public float speed;
    public string startingSide;
    public Vector3 endPos;
    public float offset;
    [SerializeField] float destroyDistance;
    private void Awake()
    {
        //Debug.Log("New Platform created");
    }

    public void SetInfo(GameObject node, float MoveSpeed, string side = "", float Offset = 0, float destroyDis = 0.5f)
    {
        speed = MoveSpeed;
        endNode = node;
        startingSide = side;
        offset = Offset;
    }

    void FixedUpdate()
    {
        endPos = new Vector3( endNode.transform.position.x, endNode.transform.position.y+offset,endNode.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPos, speed * Time.deltaTime);
    }
    private void Update()
    {
        Vector3 distance = (this.transform.position - endPos);

        if (distance.magnitude <= destroyDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
