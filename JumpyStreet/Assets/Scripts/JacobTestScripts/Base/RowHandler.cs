using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowHandler : MonoBehaviour
{
    public enum Type{ forest, road, water, desert };
    public Type rowType = new Type();

    private int rowNumber;
    // Start is called before the first frame update
    void Awake()
    {
        print("waking up");
    }

    public void SetRow(int rowNum) 
    {
        rowNumber = rowNum;
    }
}
