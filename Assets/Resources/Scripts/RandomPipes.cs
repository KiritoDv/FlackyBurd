using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPipes : MonoBehaviour
{   
    public Sprite alternativePipe;
    public GameObject pipePrefab;
    public GameObject pipePrefabMin;    
    public float genDelay = 1;        

    public float speed = 1;
    private GameController ctrl;

    void Start()
    {        
        StartCoroutine(genPipe());
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if(!this.ctrl.birdDie && this.ctrl.gameStarted){
            foreach(Transform child in transform){
                GameObject pipe = child.gameObject;
                pipe.transform.position -= new Vector3(this.speed * Time.deltaTime, 0, 0);
                Vector3 lower = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
                if(pipe.transform.position.x+2 < lower.x){
                    Destroy(pipe);
                }
            }
        }
    }

    IEnumerator genPipe(){
        while(true){
            yield return new WaitForSeconds(this.genDelay);
            if(!this.ctrl.birdDie && this.ctrl.gameStarted){
                Vector3 upper = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
                GameObject[] pipes = {pipePrefab, pipePrefabMin};
                GameObject tmp = Instantiate(pipes[Random.Range(0, 2)], new Vector3(upper.x+2, Random.Range(-2.1f, -5.25f), 0), Quaternion.identity);
                foreach(Transform child in tmp.transform){                
                    if(Random.Range(0, 2) == 1){
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = alternativePipe;   
                    }
                }
                tmp.transform.parent = transform;
            }
        }                
    }
}
