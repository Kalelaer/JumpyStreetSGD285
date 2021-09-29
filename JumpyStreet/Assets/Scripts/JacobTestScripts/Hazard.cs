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
