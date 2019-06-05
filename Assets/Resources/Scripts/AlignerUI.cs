using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignerUI : MonoBehaviour
{    
    public void ResetFade()
    {
        foreach (Transform t in GameObject.FindWithTag("AlignerUI").transform){
            t.GetComponent<FadeIn>().InitFade();   
        }        
    }
}
