using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject slimeenemyPrefab;
    public GameObject golemenemyPrefab;
    public GameObject powerupPrefab;
    public GameObject playerattack1Prefab;
    public GameObject playerattack2Prefab;
    public GameObject playerattack3Prefab;
    public GameObject EnemyAttack1Prefab;
    public GameObject EnemyAttack2Prefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    GameObject[] slimeenemy;
    GameObject[] golemenemy;
    GameObject[] enemyB;

    GameObject[] powerup;

    GameObject[] playerattack1;
    GameObject[] playerattack2;
    GameObject[] playerattack3;
    GameObject[] EnemyAttack1;
    GameObject[] EnemyAttack2;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] targetPool;


    void Awake()
    {
        enemyB = new GameObject[1];
        slimeenemy = new GameObject[10];
        golemenemy = new GameObject[10];
        powerup = new GameObject[10];

        playerattack1 = new GameObject[100];
        playerattack2 = new GameObject[100];
        playerattack3 = new GameObject[100];
        EnemyAttack1 = new GameObject[100];
        EnemyAttack2 = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];


        Generate();

    }
   
    void Generate()
    {
        //enemy
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }
        for (int index = 0; index < slimeenemy.Length; index++)
        {
            slimeenemy[index] = Instantiate(slimeenemyPrefab);
            slimeenemy[index].SetActive(false);
        }
        for (int index = 0; index < golemenemy.Length; index++)
        {
            golemenemy[index] = Instantiate(golemenemyPrefab);
            golemenemy[index].SetActive(false);
        }

        //item

        for (int index = 0; index < powerup.Length; index++)
        {
            powerup[index] = Instantiate(powerupPrefab);
            powerup[index].SetActive(false);
        }
        //attack

        for (int index = 0; index < playerattack1.Length; index++)
        {
            playerattack1[index] = Instantiate(playerattack1Prefab);
            playerattack1[index].SetActive(false);
        }

        for (int index = 0; index < playerattack2.Length; index++)
        {
            playerattack2[index] = Instantiate(playerattack2Prefab);
            playerattack2[index].SetActive(false);
        }

        for (int index = 0; index < playerattack3.Length; index++)
        {
            playerattack3[index] = Instantiate(playerattack3Prefab);
            playerattack3[index].SetActive(false);
        }

        for (int index = 0; index < EnemyAttack1.Length; index++)
        {
            EnemyAttack1[index] = Instantiate(EnemyAttack1Prefab);
            EnemyAttack1[index].SetActive(false);
        }
        for (int index = 0; index < EnemyAttack2.Length; index++)
        {
            EnemyAttack2[index] = Instantiate(EnemyAttack2Prefab);
            EnemyAttack2[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }

    }

    public GameObject MakeObj(string type)
    {

        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "Slimeenemy":
                    targetPool = slimeenemy;
                break;
            case "Golemenemy":
                    targetPool = golemenemy;
                break;
            case "Powerup":
                    targetPool = powerup;
                break;
            case "Playerattack1":
                    targetPool = playerattack1;
                break;
            case "Playerattack2":
                    targetPool = playerattack2;
                break;
            case "Playerattack3":
                    targetPool = playerattack3;
                break;
            case "EnemyAttack1":
                    targetPool = EnemyAttack1;
                break;
            case "EnemyAttack2":
                    targetPool = EnemyAttack2;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf) {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }


    //public GameObject[] GetPool(string type)
    // {
    //   switch (type) {          
    //      case "EnemyB":
    //           targetPool = enemyB;
    //       break;
    //        case "Slimeenemy":
    //  targetPool = slimeenemy;
    //         break;
    //         case "Golemenemy":
    // targetPool = golemenemy;
    //          break;
    //         case "Powerup":
    // targetPool = powerup;
    //         break;
    //          case "Playerattack1":
    //targetPool = playerattack1;
    //        break;
    //          case "Playerattack2":
    // targetPool = playerattack2;
    //         break;
    //        case "Playerattack3":
    //                 targetPool = playerattack3;
    //         break;
    //          case "EnemyAttack1":
    //  targetPool = EnemyAttack1;
    //          break;
    //         case "EnemyAttack2":
    // targetPool = EnemyAttack2;
    //         break;
    //         case "BulletBossA":
    // targetPool = bulletBossA;
    //          break;
    //        case "BulletBossB":
    //  targetPool = bulletBossB;
    //          break;
    //      }
    //      return targetPool;
    //}
    }




