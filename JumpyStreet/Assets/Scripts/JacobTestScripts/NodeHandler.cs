using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Group
{
    [SerializeField] float _offset;
    [SerializeField] GameObject _object;
    [SerializeField] string _tileType;

    public float Offset{
        get {return _offset;}
        set {_offset = value;}
    }
    public GameObject Object{
        get {return _object;}
        set {_object = value;}
    }
    public string TileType{
        get {return _tileType;}
        set {_tileType = value;}
    }

}


public class NodeHandler : MonoBehaviour
{
    private int objectSelector;
    [SerializeField] List<Group> spawnables;
    public Node node;
    public GameObject thisObject;
    private void Awake()
    {
        thisObject = this.gameObject;
        Group[] placeholderArray = spawnables.ToArray();
        int spawnablesLength = placeholderArray.Length;
        //print(spawnablesLength);
        int willSpawnObject = Random.Range(1, 101);
        print(willSpawnObject);
        if (willSpawnObject > 50) { 
            objectSelector = Random.Range(0, spawnablesLength);
            //print(objectSelector);
        }
        else
        {
            objectSelector = 0;
        }
    }
    public void CreateNode(int index){
        node = new Node(index, gameObject.GetComponent<NodeHandler>());
        node.SetVisual(spawnables[objectSelector]);
    }
    public void SetObject(GameObject item, float offset){
        Vector3 objectRotation = new Vector3(-90,0,0);
        Vector3 objectOffset = new Vector3(thisObject.transform.position.x, thisObject.transform.position.y + offset, thisObject.transform.position.z);
        GameObject childObject = Instantiate(item, objectOffset, Quaternion.Euler(objectRotation));
        childObject.transform.parent = thisObject.transform;
    }
}
