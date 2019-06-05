using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BirdController : MonoBehaviour
{

    public float upForce;
    private Rigidbody2D rb2d;
    private Animator anim;

    public Sprite baseSprite;
    public int seconds;
    private bool par = false;

    private float parMod;
    public float downVel = 0.5f;
    public float minVel = 0.5f;
    public float maxVel = 0.8f;
    private GameController ctrl;
    private bool randomColor = false;

    public float restartDelay = 1;
    private bool canRestart;
    void Start()
    {
        this.canRestart = false;

        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        

        this.randomColor = Random.Range(0, 10) == 5;

        foreach (Transform item in transform)
        {
            item.gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(((Time.time*1000) % 1200L) / 1200.0F, 0.75F, 1.0F);
        }
    }

    // Update is called once per frame
    void Update()
    {    
        if(this.randomColor){
            foreach (Transform item in transform){
                item.gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(((Time.time*1000) % 1200L) / 1200.0F, 0.75F, 1.0F);
            }
        }    
        if (this.ctrl.gameStarted)
        {
            if (!this.ctrl.birdDie)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 75);
                    this.rb2d.velocity = Vector2.zero;
                    this.rb2d.AddForce(new Vector2(0, upForce));
                    this.ctrl.sound.wingSound.PlayDelayed(0);
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), downVel * Time.deltaTime);
            }
            else
            {
                StartCoroutine(canRestartDelay());
                
                if(this.canRestart){
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
        else
        {
            this.rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            transform.position = new Vector3(transform.position.x, Mathf.Lerp(minVel, maxVel, parMod), 0);

            this.parMod += this.downVel * Time.deltaTime;

            if (this.parMod > 1.0f){
                float temp = maxVel;
                maxVel = minVel;
                minVel = temp;
                this.parMod = 0.0f;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                this.ctrl.gameStarted = true;
                this.rb2d.constraints = RigidbodyConstraints2D.None;
                //GameObject.FindWithTag("NDisplay").GetComponent<NDController>().initNDController();
            }
        }
        
        Vector3 upper = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if(transform.position.y > upper.y+1){
            if(!this.ctrl.birdDie){
                this.ctrl.sound.hitSound.PlayDelayed(0);
                this.ctrl.sound.dieSound.PlayDelayed(0.13f);
            }
            this.ctrl.birdDie = true;
            this.anim.enabled = false;
            GetComponent<SpriteRenderer>().sprite = this.baseSprite;
        }
    }

    IEnumerator canRestartDelay()
    {        
        yield return new WaitForSeconds(this.restartDelay);
        this.canRestart = true;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name.Contains("Grass"))
        {
            if(!this.ctrl.birdDie){
                this.ctrl.sound.hitSound.PlayDelayed(0);
                this.ctrl.sound.dieSound.PlayDelayed(0.13f);
            }
            this.ctrl.birdDie = true;            
            this.anim.enabled = false;
            GetComponent<SpriteRenderer>().sprite = this.baseSprite;            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Dummy"){
            this.ctrl.sound.pointSound.PlayDelayed(0);
            if (!this.ctrl.birdDie && this.ctrl.gameStarted)
                this.seconds++;
        }else{
            if(!this.ctrl.birdDie){
                this.ctrl.sound.hitSound.PlayDelayed(0);
                this.ctrl.sound.dieSound.PlayDelayed(0.13f);
            }
            this.ctrl.birdDie = true;
            this.anim.enabled = false;
            GetComponent<SpriteRenderer>().sprite = this.baseSprite;
        }     
    }

    public float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}