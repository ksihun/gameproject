using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject[] enemyobjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;



    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)   //적 스폰 딜레이
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);  //x초부터 y초 사이의 랜덤값으로 몹 생성주기 결정
            curSpawnDelay = 0;
        }

        //UI 점수 업데이트

        player playerLogic = player.GetComponent<player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
            

    }
    void SpawnEnemy()    //적 소환 로직
    {
        int ranEnemy = Random.Range(0, 2);   //적 종류
        int ranPoint = Random.Range(0, 9);   //적 생성 위치
        GameObject enemy = Instantiate(enemyobjs[ranEnemy],
                                       spawnPoints[ranPoint].position,
                                       spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;


        if (ranPoint == 5 || ranPoint == 6)
        {
            //enemy.transform.Rotate(Vector3.back * 30);           
            //적 유닛 방향 조절(사용x)
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1); // 왼쪽 적의 이동

        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            //enemy.transform.Rotate(Vector3.forward * 30);        
            //적 유닛 방향 조절(사용x)
            rigid.velocity = new Vector2(enemyLogic.speed, -1);        // 오른쪽 적의 이동
            

        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));              // 중앙 적의 이동

        }
    }

    public void UpdateLifeIcon(int life)
    {
        // UI 체력 초기화 비활성화
        for (int index=0; index< 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);

        }
        // UI 체력 활성화
        for (int index = 0; index <life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);

        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
        
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        player playerLogic = player.GetComponent<player>(); 
        playerLogic.isHit = false;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);

    }




}

