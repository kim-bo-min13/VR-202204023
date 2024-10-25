using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject apple;
    public GameObject bomb;
    float span = 0.2f;
    float delta = 0;
    int ratio = 2;
    float speed = -0.01f;
    // Start is called before the first frame update
    public void SetParameter(float span,float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;
            int dice = Random.Range(1,11);
            if(dice <= this.ratio)
            {
                item = Instantiate(bomb);
            }
            else
            {
                item = Instantiate(apple);
            }
            float x = Random.Range(-28, 28);
            float z = Random.Range(-28, 28);
            item.transform.position = new Vector3(x,35,z);
            item.GetComponent<Missile>().dropSpeed = this.speed;
        }
    }
}
