using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject endNode;
    public float speed;
    private void Awake()
    {
        Debug.Log("New Platform created");
    }

    public void SetInfo(GameObject node, float MoveSpeed)
    {
        speed = MoveSpeed;
        endNode = node;
    }

    void FixedUpdate()
    {
        Vector3.MoveTowards(this.transform.position, endNode.transform.position, speed);
    }
    private void Update()
    {
        if (this.gameObject.transform.position == endNode.transform.position)
        {
            Destroy(this);
        }
    }
}
