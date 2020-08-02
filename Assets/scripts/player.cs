using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float power;
    public float speed;
    public float maxShotDelay;
    public float curShotDelay;
    public bool isTouchtop;
    public bool isTouchbottom;
    public bool isTouchright;
    public bool isTouchleft;

    
    public GameObject playerattack1;
    public GameObject playerattack2;
    public GameObject playerattack3;
    void Update()
    {
        Move();
        Fire();
        Reload();

       

    }

    private void Move()
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


    void Fire()
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

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
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
                    isTouchleft= true;
                    break;

            }

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
