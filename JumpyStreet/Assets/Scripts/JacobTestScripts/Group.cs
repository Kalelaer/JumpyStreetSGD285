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
