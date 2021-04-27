using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CellAlignment : GeneralBehaviour
{
    public float cellSize = 1;
    void Awake(){
        
    }
    void OnEnable(){
        float positionX = RoundToNearest(transform.position.x, cellSize);
        float positionY = RoundToNearest(transform.position.y, cellSize);
        transform.position = new Vector3(
            positionX,
            positionY,
            transform.position.z
        );
    }
}
