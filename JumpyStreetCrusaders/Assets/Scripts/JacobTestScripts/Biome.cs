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
public class Biome
{
    public enum Type{ forest, road, water, desert, desertHazard, forestHazard };
    [SerializeField] Type _biomeType;
    [SerializeField] string _biomeName;
    [SerializeField] int _maxBiomeLength;
    [SerializeField] bool _isHazard;

    public Type BiomeType{
        get{ return _biomeType;}
        set{ _biomeType = value;}
    }
    public string BiomeName{
        get{ return _biomeName; }
        set{ _biomeName = value; }
    }
    public int MaxBiomeLength{
        get{ return _maxBiomeLength; }
        set{ _maxBiomeLength = value; }
    }
    public bool IsHazard {
        get { return _isHazard; }
        set { _isHazard = value; }
    }

}
