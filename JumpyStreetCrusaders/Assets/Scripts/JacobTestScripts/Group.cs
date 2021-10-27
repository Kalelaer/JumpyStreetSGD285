//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Brennan Sullivan & Jacob Coleman
//Section: 2021FA.SGD.285.2141
//Instructor: Aurore Wold
//Date: 9/13/2021
/////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Group
{
    [SerializeField] Biome.Type _biomeType;
    [SerializeField] string _tileType;
    [SerializeField] float _offset;
    [SerializeField] float _rotationOffset;
    [SerializeField] GameObject _object;


    public float Offset{
        get { return _offset; }
        set { _offset = value; }
    }
    public float RotationOffset
    {
        get { return _rotationOffset; }
        set { _rotationOffset = value; }
    }
    public GameObject Object{
        get { return _object; }
        set { _object = value; }
    }
    public string TileType{
        get { return _tileType; }
        set { _tileType = value; }
    }
    public Biome.Type BiomeType
    {
        get { return _biomeType; }
        set { _biomeType = value; }
    }

}
