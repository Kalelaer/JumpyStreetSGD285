using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NodeClass
{
    

    //private string leftNodeType, rightNodeType;
    [SerializeField] string type;
    [SerializeField] int _arrayLocation;
    [SerializeField] GameObject visual;
    private NodeHandler NH;

    public GameObject Visual{
        get{return visual;}
        set{visual = value;}
    }

    public NodeClass(int index, NodeHandler handler){
        _arrayLocation = index;
        NH = handler;
    }
    public void SetVisual(Group item){
        Visual = item.Object;
        type = item.TileType;
        NH.SetObject(Visual,item.Offset);
    }

}
