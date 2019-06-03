using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    private GameController ctrl;
    public TextMeshProUGUI sec;

    public TextMeshProUGUI points;
    public TextMeshProUGUI go;
    public TextMeshProUGUI pt;

    public TextMeshProUGUI tap;
    void Start()
    {
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();        

        this.tap.enabled = false;
        this.points.enabled = false;
        this.go.enabled = false;
        this.pt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        this.points.text = ""+this.ctrl.bird.seconds;
        this.points.enabled = this.ctrl.birdDie;
        this.go.enabled = this.ctrl.birdDie;
        this.tap.enabled = this.ctrl.birdDie;
        this.pt.enabled = this.ctrl.birdDie;         

        this.sec.enabled = this.ctrl.gameStarted && !this.ctrl.birdDie;
        this.sec.text = ""+this.ctrl.bird.seconds;
    }
}
