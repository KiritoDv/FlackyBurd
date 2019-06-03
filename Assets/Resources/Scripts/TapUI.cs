using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUI : MonoBehaviour
{
    public float fadeSpeed = 0.1f;    
    public float currentOpacity = 1f;
    private GameController ctrl;
    public bool fadeIn = false;
    void Start()
    {
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();        
    }

    // Update is called once per frame
    void Update()
    {        
        if(this.ctrl.gameStarted){            
            if(currentOpacity > 0){
                if(this.fadeIn)
                    currentOpacity += fadeSpeed * Time.deltaTime;
                else
                    currentOpacity -= fadeSpeed * Time.deltaTime;                
                    if(GetComponent<SpriteRenderer>() != null)
                        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, currentOpacity);
                    else
                        GetComponent<CanvasRenderer>().SetAlpha(currentOpacity);
            }
        }
    }
}
