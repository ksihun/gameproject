using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    public string[] enemyobjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyobjs = new string[] { "Slimeenemy", "Golemenemy", "EnemyB" };
        ReadSpawnFile();
    }




    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage 1") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
         
            if (line == null)
                break;


            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)   //적 스폰 딜레이
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //UI 점수 업데이트

        player playerLogic = player.GetComponent<player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
            

    }
    void SpawnEnemy()    //적 소환 로직
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "Slime":
                enemyIndex = 0;
                break;
            case "Golem":
                enemyIndex = 1;
                break;
                case "B":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;

        GameObject enemy = objectManager.MakeObj(enemyobjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
                                         
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;


        if (enemyPoint == 5 || enemyPoint == 6)
        {
            //enemy.transform.Rotate(Vector3.back * 30);           
            //적 유닛 방향 조절(사용x)
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1); // 왼쪽 적의 이동

        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            //enemy.transform.Rotate(Vector3.forward * 30);        
            //적 유닛 방향 조절(사용x)
            rigid.velocity = new Vector2(enemyLogic.speed, -1);        // 오른쪽 적의 이동
            

        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));              // 중앙 적의 이동

        }

        //# 리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        //# 다음 리스폰 딜레이
        nextSpawnDelay = spawnList[spawnIndex].delay;
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

