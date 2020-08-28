using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyScore; //적의 점수
    public string EnemyName;
    public float speed;       //적 이동속도
    public int health;        //적 체력
    public Sprite[] sprites;



    public float maxShotDelay;          // 공격속도
    public float curShotDelay;

    public GameObject EnemyAttack1;    //적 공격1
    public GameObject EnemyAttack2;     //적 공격2
    public GameObject powerup;
    public GameObject player;
    SpriteRenderer spriteRenderer;
    Animator anim;
    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
            anim = GetComponent<Animator>();

        }

    void Update()
    {
        Fire();
        Reload();
        
    }

    void Fire()          //공격 로직
    {
        
        if (curShotDelay < maxShotDelay)
            return;

        //적의 공격
        if (EnemyName == "Slime") //슬라임 공격
        {
            GameObject Attack1 = Instantiate(EnemyAttack1, transform.position + Vector3.up * 0.4f, transform.rotation);
            Rigidbody2D rigid1 = Attack1.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;

            rigid1.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);     //슬라임 투사체 속도

        }
        else if (EnemyName == "Golem") //골렘 공격
        {
            GameObject Attack2 = Instantiate(EnemyAttack2, transform.position + Vector3.up * 0.4f, transform.rotation);
            Rigidbody2D rigid2 = Attack2.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;

            rigid2.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);            //골렘 투사체 속도

        }

                curShotDelay = 0;
    }

    void Reload()            //공격 속도
    {
        curShotDelay += Time.deltaTime;
    }

    void OnHit(int dmg)        //적이 피해를 받았을 때
    {

        

        health -= dmg;
        

        if (health <= 0)
        {
            player playerLogic = player.GetComponent<player>();
            playerLogic.score += enemyScore;


            int ran = Random.Range(0, 10);
            if (ran < 8) 
            {
                Debug.Log("Not Item");
            }
            else if (ran < 10)
            {
                Instantiate(powerup, transform.position, powerup.transform.rotation);
            }

            

            Destroy(gameObject);

           
            
            
        }
    }
    void Returnsprite()

    {
        spriteRenderer.sprite = sprites[0];
 
        



        }

    void OnTriggerEnter2D(Collider2D collision)      //적이 공격 받앗을때 및 맵 끝까지 갔을 때
    {
        if (collision.gameObject.tag == "BorderAttack")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "playerattack"){
            Attack attack = collision.gameObject.GetComponent<Attack>();
            OnHit(attack.dmg);

            Destroy(collision.gameObject);
            //dfd

        }



    }

}



