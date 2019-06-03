using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource dieSound;
    public AudioSource hitSound;
    public AudioSource pointSound;
    public AudioSource wingSound;
    
    public float volume = 0.01f;

    void Start(){
        this.dieSound.volume = volume;
        this.hitSound.volume = volume;
        this.pointSound.volume = volume-0.05f;
        this.wingSound.volume = volume;
    }

}
