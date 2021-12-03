using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerater : MonoBehaviour
{
    [SerializeField]
    public float SpawnInterval;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    public float currentTime;
    int monsterNo;
    int Posindex;
    int PrevPosindex;

    void Start()
    {
        PrevPosindex = 99;
        Posindex = 0;
        spawnAmount = 1;
        SpawnInterval = 5;
        monsterNo = 20001;
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        Spawn();

    }

    void Spawn()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        if (currentTime >= SpawnInterval && !BattleManager.Instance.isBoss)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                switch (Posindex) // 몬스터 위치 인덱스의 위치
                {
                    case 0:
                        var obj = Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.51f));
                        obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        break;
                    case 1:
                        var obj2 = Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.35f));
                        obj2.GetComponent<SpriteRenderer>().sortingOrder = 2;
                        break;
                    case 2:
                        var obj3 = Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.123f));
                        obj3.GetComponent<SpriteRenderer>().sortingOrder = 3;
                        break;

                    default:
                        Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, Random.Range(0.09f, 0.51f)));
                        break;
                }
                //Objectpool.GetMonsterobject(monsterNo,new Vector2(transform.position.x, Random.Range(0.09f, 0.51f)));
                if (ScoreCheck(0, 8))
                {
                    currentTime = 0;
                    Randomindex();
                    monsterNo = 20001;
                    spawnAmount = Random.Range(1, 3);
                    SpawnInterval = Random.Range(4, 7);
                }
                else if (ScoreCheck(8, 20))
                {
                    currentTime = 0;
                    Randomindex();
                    RandomMonsterNo(20001, 20002);
                    spawnAmount = Random.Range(1, 4);
                    SpawnInterval = Random.Range(3, 6);
                }

                else if (ScoreCheck(20, 40))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        monsterNo = 20003;
                        RandomMonsterNo(20001, 20003);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(40, 60))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20001, 20005);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(60, 100))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20001, 20005);
                        spawnAmount = Random.Range(2, 3);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(100, 110))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        monsterNo = 20006;
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(110, 120))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20006, 20007);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(120, 140))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20004, 20008);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(140, 160))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20004, 20009);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else if (ScoreCheck(160, 200))
                {
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20004, 20010);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);

                    }
                }
                else
                {
                    print("hello world");
                    if (currentTime >= SpawnInterval)
                    {
                        currentTime = 0;

                        Randomindex();
                        RandomMonsterNo(20001, 20009);
                        spawnAmount = Random.Range(2, 4);
                        SpawnInterval = Random.Range(3, 6);
                    }
                }
            }


        }
    }

    public void Randomindex() // 몬스터 위치 인덱스 설정 
    {
        Posindex = Random.Range(0, 3);

        if (Posindex == PrevPosindex)
        {
            for (; Posindex != PrevPosindex;)
            {

                Posindex = Random.Range(0, 3);
            }
        }
    }

    public void RandomMonsterNo(int min, int max) // 몬스터 init num
    {
        monsterNo = Random.Range(min, max+1);
    }

    public bool ScoreCheck(int min, int max) 
    {
        if (BattleManager.Instance.Score + (BattleManager.Instance.purpose-BattleManager.Instance.BossInterval)>= min && BattleManager.Instance.Score + (BattleManager.Instance.purpose - BattleManager.Instance.BossInterval) < max && !BattleManager.Instance.isBoss)
        {
            return true;
        }

        return false;
    }
}