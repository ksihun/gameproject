using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float power;             //공격 종류 
    public float speed;             //속도
    public float maxShotDelay;          // 공격속도
    public float curShotDelay;
    public bool isTouchtop;              //벽
    public bool isTouchbottom;            //벽
    public bool isTouchright;             //벽
    public bool isTouchleft;               //벽

    public int life;
    public int score;
    
    

    public GameObject playerattack1;    //공격1
    public GameObject playerattack2;     //공격2
    public GameObject playerattack3;       //공격3
    Animator anim;


    public GameManager manager;


    public bool isHit;
    void Awake()
    {
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
        anim.SetBool("isFire", Input.GetButton("Fire1"));
    }

    private void Move()    //이동 로직
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchright && h == 1) || (isTouchleft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchtop && v == 1) || (isTouchbottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

    }


    void Fire()          //공격 로직
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject Attack1 = Instantiate(playerattack1, transform.position+Vector3.up*0.4f, transform.rotation);
                Rigidbody2D rigid1 = Attack1.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject Attack2 = Instantiate(playerattack2, transform.position + Vector3.up * 0.4f, transform.rotation);
                Rigidbody2D rigid2 = Attack2.GetComponent<Rigidbody2D>();
                rigid2.AddForce(Vector2.up * 15, ForceMode2D.Impulse);

                break;
            case 3:
                GameObject Attack3 = Instantiate(playerattack3, transform.position + Vector3.up * 0.4f, transform.rotation);
                Rigidbody2D rigid3 = Attack3.GetComponent<Rigidbody2D>();
                rigid3.AddForce(Vector2.up * 15, ForceMode2D.Impulse);

                break;


        }
        

        curShotDelay = 0;
    }

    void Reload()            //공격 속도
    {
        curShotDelay += Time.deltaTime;
    }


    // 충돌
    void OnTriggerEnter2D(Collider2D collision)    
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchtop = true;
                    break;
                case "bottom":
                    isTouchbottom = true;
                    break;
                case "right":
                    isTouchright = true;
                    break;
                case "left":
                    isTouchleft = true;
                    break;

            }

        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyAttack")    //적의 공격 및 적과의 충돌
        {

            if (isHit)                             //점수 버그 방지(당장은 필요 x, 총알이 2개 나가게 되면 필요)
                return;

            isHit = true;
            life--;
            manager.UpdateLifeIcon(life);
            
            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
            manager.RespawnPlayer();
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)  
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchtop = false;
                    break;
                case "bottom":
                    isTouchbottom = false;
                    break;
                case "right":
                    isTouchright = false;
                    break;
                case "left":
                    isTouchleft = false;
                    break;

            }

        }
    }
}
