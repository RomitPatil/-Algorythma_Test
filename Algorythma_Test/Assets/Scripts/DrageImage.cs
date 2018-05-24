using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrageImage : MonoBehaviour{


    public void Drag()
    {
        transform.localPosition = Input.mousePosition;
    }

 
}
