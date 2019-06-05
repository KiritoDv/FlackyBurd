using UnityEngine;
using System.Collections;
using TMPro;
public class DrawFPS : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private float deltaTime;

    void Start(){
        this.fpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update () {
        this.deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        this.fpsText.text = "FPS "+Mathf.Ceil (fps).ToString();
    }
}
