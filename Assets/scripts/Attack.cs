﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int dmg;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderAttack")
        {
            Destroy(gameObject);

        }
    }
}
