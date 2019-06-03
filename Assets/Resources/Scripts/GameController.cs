using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool birdDie = false;
    public bool gameStarted = false;
    public BirdController bird;
    public SoundController sound;
    void Start(){
        this.bird = GameObject.FindWithTag("Player").GetComponent<BirdController>();
        this.sound = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();
    }
}
