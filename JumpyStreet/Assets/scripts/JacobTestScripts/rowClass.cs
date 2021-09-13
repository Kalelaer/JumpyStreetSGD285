using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class rowClass
{
    [SerializeField] int _rowNumber;
    [SerializeField] GameObject[] _nodes = new GameObject[30];
    
    public int RowNumber{
        get { return _rowNumber; }
        set { _rowNumber = value; }
    }
    public GameObject[] Nodes
    {
        get { return _nodes; }
        set { _nodes = value; }
    }



}
