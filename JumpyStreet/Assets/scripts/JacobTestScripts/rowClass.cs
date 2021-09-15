using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RowClass
{

    [SerializeField] int _rowNumber;
    [SerializeField] GameObject rowObject;

    public int RowNumber
    {
        get { return _rowNumber; }
        set { _rowNumber = value; }
    }
    public GameObject RowObject
    {
        get { return rowObject; }
        set { rowObject = value; }
    }

    //initializer
    public RowClass( int rowNum, GameObject go)
    {
        RowNumber = rowNum;
        RowObject = go;
    }

}
