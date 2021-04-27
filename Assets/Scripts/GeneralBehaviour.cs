using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float RoundToNearest(float n, float x) {
        return Mathf.Round(n / x) * x;
    } 
}
