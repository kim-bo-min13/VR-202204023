using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool isJump;
    bool isDodge;
    bool isBorder;

    Rigidbody rigid;
    public AudioClip coinSE;
    public AudioClip missileSE;
    AudioSource aud;
    GameObject director;

    Vector3 moveVec;
    Vector3 dodgeVec;
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();  
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        FixedUpdate();


    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
        {
            moveVec = dodgeVec;
        }
        if(!isBorder)
             transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 16, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }

    }
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero &&  !isJump && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);
        }

    }
    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")    {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            this.director.GetComponent<GameDirector>().GetApple();
           
            this.aud.PlayOneShot(this.coinSE);
        }
        else if (other.gameObject.tag == "Missile")
        {
            this.director.GetComponent<GameDirector>().GetBomb();
            
            this.aud.PlayOneShot(this.missileSE);
        }
        Destroy(other.gameObject);
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    
    void StopToWall()
    {
        Debug.DrawRay(transform.position,transform.forward * 5 ,Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate()
    {
        StopToWall();
    }
}
