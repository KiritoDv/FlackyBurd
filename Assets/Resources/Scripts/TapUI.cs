using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUI : MonoBehaviour
{
    public float fadeSpeed = 0.1f;    
    private float currentOpacity = 1f;
    private GameController ctrl;
    void Start()
    {
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(this.ctrl.gameStarted){            
            if(currentOpacity > 0){
                currentOpacity -= fadeSpeed * Time.deltaTime;
                GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, currentOpacity);
            }
        }
    }
}
