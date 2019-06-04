using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0f;
    private int lastID = 0;
    private float gap = 0;
    private GameController ctrl;
    void Start()
    {

        Application.targetFrameRate = 9999;
        QualitySettings.vSyncCount = 0;

        Vector3 l = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 u = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        this.gap = Mathf.Abs(l.x - u.x);
    }

    void Update()
    {
        this.ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        transform.position = setToVector(transform.position, 0, 0);

        Vector3 lower = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upper = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        //transform.position = setToVector(transform.position, 0, Camera.main.transform.position.y+15);

        int[] order = { 1, 2, 0 };
        int[] order2 = { 2, 0, 1 };

        if (!this.ctrl.birdDie || !this.ctrl.gameObject)
        {
            if (transform.GetChild(order[this.lastID]).position.x <= lower.x)
            {
                transform.GetChild(this.lastID).position = setToVector(transform.GetChild(this.lastID).position, transform.GetChild(order2[this.lastID]).position.x + this.gap, 0);

                if (this.lastID >= order.Length - 1)
                {
                    this.lastID = 0;
                }
                else
                    this.lastID++;
            }

            transform.position -= new Vector3(this.speed * Time.deltaTime, 0, 0);
        }
        //transform.position = setToVector(transform.position, 0, 0);
    }

    Vector3 setToVector(Vector3 v, float x, float y)
    {
        return new Vector3(x == 0 ? v.x : x, y == 0 ? v.y : y, 0);
    }

    Vector3 addToVector(Vector3 v, float x, float y)
    {
        return new Vector3(v.x + x, v.y + y, 0);
    }

    Vector3 subFromVector(Vector3 v, float x, float y)
    {
        return new Vector3(v.x - x, v.y - y, 0);
    }
}
