using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeIn : MonoBehaviour
{    
    public float fadeSpeed = 0.1f;
    private Vector3 originalPos;
    private TextMeshProUGUI text;
    private RectTransform pos;
    private GameController ctrl;
    public float fadeDelay;
    
    public float opacitySpeed = 0.4f;
    public float currentOpacity = 0;

    void Start(){        
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.text = GetComponent<TextMeshProUGUI>();
        this.pos = GetComponent<RectTransform>();

        this.originalPos = this.pos.position;
        this.pos.position -= new Vector3(0, this.pos.position.y/2, 0);

        text.alpha = this.currentOpacity;
    }
        
    public void InitFade(){
        StartCoroutine(initDelay());
    }

    IEnumerator initDelay(){
        yield return new WaitForSeconds(this.fadeDelay);
        if(this.currentOpacity < 1)
            this.currentOpacity+=this.opacitySpeed * Time.deltaTime;

        text.alpha = this.currentOpacity;

        transform.position = Vector3.Lerp(this.pos.position, new Vector3(this.pos.position.x, this.originalPos.y, 0), Time.deltaTime * fadeSpeed);
    }
}