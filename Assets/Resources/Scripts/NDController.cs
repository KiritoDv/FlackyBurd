using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NDController : MonoBehaviour
{
    public GameObject nprefab;
    private Sprite[] nums;
    private GameObject[] numRow;
    private int currentChilds;

    public string spritePath;
    private float currentSize;
    private GameController ctrl;
    public float spacing = 0.4f;
    public int layer = 99;

    public TextMeshProUGUI text;

    void Start()
    {
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void initNDController()
    {                
        numRow = new GameObject[9999];
        nums = Resources.LoadAll<Sprite>(this.spritePath);
        GameObject tmp = Instantiate(nprefab, transform.position, Quaternion.identity);
        tmp.GetComponent<SpriteRenderer>().sortingOrder = layer;
        tmp.transform.parent = transform;
        numRow[0] = tmp;
        this.currentChilds = 1;
    }
    
    void Update()
    {                
        Vector3 pos = transform.position;
        int sec = GameObject.FindWithTag("Player").GetComponent<BirdController>().seconds;
        char[] num = (sec+"").ToCharArray();

        if(this.currentChilds < num.Length && this.ctrl.gameStarted){
            this.currentChilds++;                        
            GameObject tmp = Instantiate(nprefab, new Vector3(transform.position.x+((this.currentChilds-1)*spacing), transform.position.y, 0), Quaternion.identity);
            tmp.GetComponent<SpriteRenderer>().sortingOrder = layer;
            tmp.transform.parent = transform;
            numRow[this.currentChilds-1] = tmp;
            this.recalculatePosition();
        }            
    
        for(int i = 0; i < this.currentChilds; i++){
            int a = int.Parse(num[i].ToString());
            if(nums[a] != null)
                numRow[i].GetComponent<SpriteRenderer>().sprite = nums[a];
            //float t = nums[a].bounds.size.x-0.044f;
            //numRow[i].transform.position = new Vector3((i*t), transform.position.y, 0);            
            
        }

        text.text = ""+sec;
        
    }

    private void recalculatePosition(){
         Bounds bounds = new Bounds(this.transform.position, Vector3.zero);
 
         foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
         {
             bounds.Encapsulate(renderer.bounds);
         }
 
         Vector3 localCenter = bounds.center - this.transform.position;
         bounds.center = localCenter;
         transform.position = new Vector3(-bounds.center.x, transform.position.y, 0);
     }
}
