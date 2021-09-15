using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowHandler : MonoBehaviour
{
    public enum Type{ forest, road, water, desert };
    public Type rowType = new Type();

    public RowClass row;
    private int rowNumber;
    // Start is called before the first frame update
    void Awake()
    {
        row = new RowClass(rowNumber, this.gameObject);
    }

    public void SetRow(int rowNum) 
    {
        rowNumber = rowNum;
        row.RowNumber = rowNum;
    }
}
