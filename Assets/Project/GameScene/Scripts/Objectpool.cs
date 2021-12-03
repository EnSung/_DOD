using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MultiMap<TKey, TValue>
{
    private readonly Dictionary<TKey, IList<TValue>> storage;

    public MultiMap()
    {
        storage = new Dictionary<TKey, IList<TValue>>();
    }

    public void Add(TKey key, TValue value)
    {   
        if (!storage.ContainsKey(key)) storage.Add(key, new List<TValue>());
        storage[key].Add(value);
    }

    public IEnumerable<TKey> Keys
    {
        get { return storage.Keys; }
    }

    public bool ContainsKey(TKey key)
    {
        return storage.ContainsKey(key);
    }

    public IList<TValue> this[TKey key]
    {
        get
        {
            if (!storage.ContainsKey(key))
                throw new KeyNotFoundException(
                    string.Format(
                        "The given key {0} was not found in the collection.", key));
            return storage[key];
        }
    }

    public TValue Removeit(TKey unit)
    {
        var obj = storage[unit][0];
        storage[unit].RemoveAt(0);

        return obj;
    }
}
public class Objectpool : MonoBehaviour
{
    public static Objectpool Instance;

    [Header("Prefab Object")]
    [SerializeField]
    private GameObject CoinPrefabs;
    [SerializeField]
    private GameObject[] MonsterPrefabs;
    [SerializeField]
    private GameObject[] BossMonsterPrefabs;
    [SerializeField]
    private GameObject DamageTextPrefab;
    [SerializeField]
    private GameObject healEffectPrefab;
    [SerializeField]
    private GameObject CTObjPrefab;

    private Queue<Coin>CoinQueue = new Queue<Coin>();
    private Queue<HealEffect>healEffectQueue = new Queue<HealEffect>();

    MultiMap<int,Monster> monsterMap = new MultiMap<int, Monster>();
    MultiMap<int,BossMonster> bossMap = new MultiMap<int, BossMonster>();

    private Queue<DamageText> DamageTextQueue = new Queue<DamageText>();
    private Queue<SkillCoolTimeObject>CTObjQueue = new Queue<SkillCoolTimeObject>();


    void Awake()
    {
        Instance = this;
        Initialize(50);
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CoinQueue.Enqueue(CreateNewCoin());
            CTObjQueue.Enqueue(CreateNewCTObj());
            CreateNewMonsters();
            healEffectQueue.Enqueue(CreateNewhealEffect());
        }

        for (int i = 0; i < 3; i++)
        {
            CreateNewBossMonsters();
        }
        for (int i = 0; i < 1000; i++)
        {
            DamageTextQueue.Enqueue(CreateNewDamageText());
        }

        
    }

    #region Coin

    private Coin CreateNewCoin()
    {
        var newObj = Instantiate(CoinPrefabs).GetComponent<Coin>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static Coin GetCoinobject(Vector2 pos)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.CoinQueue.Dequeue();
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewCoin();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnCoin(Coin Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.CoinQueue.Enqueue(Obj);
    }

    #endregion

    #region Monster

    private void CreateNewMonsters()
    {

        foreach (GameObject monster in MonsterPrefabs)
        {
            var newObj = Instantiate(monster).GetComponent<Monster>();
            newObj.transform.parent = Instance.transform;
            newObj.gameObject.SetActive(false);
            monsterMap.Add(newObj.unitNo, newObj);
        }
        

    }



    public static Monster GetMonsterobject(int monsterNo,  Vector2 pos/*, BattleManager.Monster type*/)
    {
        if (Instance.monsterMap[monsterNo].Count > 0)
        {
            var obj = Instance.monsterMap.Removeit(monsterNo);
            
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            Instance.CreateNewMonsters();
            var newObj = Instance.monsterMap.Removeit(monsterNo);

            newObj.transform.position = pos;
            newObj.gameObject.SetActive(true);

            return newObj;
        }
    }

    public static void ReturnMonster(Monster Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.monsterMap.Add(Obj.unitNo,Obj);
    }

    #endregion

    #region DamageText

    private DamageText CreateNewDamageText()
    {
        var newObj = Instantiate(DamageTextPrefab).GetComponent<DamageText>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static DamageText GetDamageText(GameObject parent, Vector2 pos, string content)
    {
        if (Instance.DamageTextQueue.Count > 0)
        {
            var obj = Instance.DamageTextQueue.Dequeue();
            obj.transform.SetParent(parent.transform);
            obj.transform.position = pos;
            obj.text.text = content;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewDamageText();
            newObj.transform.SetParent(parent.transform);
            newObj.transform.position = pos;
            newObj.text.text = content;
            newObj.gameObject.SetActive(true);
            return newObj;



        }
    }

    public static void ReturnDamageText(DamageText Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.DamageTextQueue.Enqueue(Obj);
    }


    #endregion

    #region CoolTimeObject

    private SkillCoolTimeObject CreateNewCTObj()
    {
        var newObj = Instantiate(CTObjPrefab).GetComponent<SkillCoolTimeObject>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static SkillCoolTimeObject GetCTobj(int Key, GameObject parents)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.CTObjQueue.Dequeue();

            obj.skillKey = Key;
            obj.gameObject.transform.parent = parents.transform;
            obj.gameObject.transform.localPosition = Vector2.zero;

            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var obj = Instance.CreateNewCTObj();


            obj.skillKey = Key;
            obj.gameObject.transform.parent = parents.transform;
            obj.gameObject.transform.localPosition = Vector2.zero;
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public static void ReturnCTObj(SkillCoolTimeObject Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.CTObjQueue.Enqueue(Obj);
    }
    #endregion

    #region HealEffect
    private HealEffect CreateNewhealEffect()
    {
        var newObj = Instantiate(healEffectPrefab).GetComponent<HealEffect>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static HealEffect GetHealobject(Vector2 pos)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.healEffectQueue.Dequeue();
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewhealEffect();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnhealEffect(HealEffect Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.healEffectQueue.Enqueue(Obj);
    }
    #endregion


    #region BossMonster


    private void CreateNewBossMonsters()
    {

        foreach (GameObject monster in BossMonsterPrefabs)
        {
            var newObj = Instantiate(monster).GetComponent<BossMonster>();
            newObj.transform.parent = Instance.transform;
            newObj.gameObject.SetActive(false);
            bossMap.Add(newObj.unitNo, newObj);
        }



    }



    public static BossMonster GetBossMonsterobject(int monsterNo, Vector2 pos/*, BattleManager.Monster type*/)
    {
        if (Instance.bossMap[monsterNo].Count > 0)
        {
            var obj = Instance.bossMap.Removeit(monsterNo);

            obj.transform.position = pos;
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            Instance.CreateNewMonsters();
            var newObj = Instance.bossMap.Removeit(monsterNo);

            newObj.transform.position = pos;
            newObj.gameObject.SetActive(true);

            return newObj;
        }
    }

    public static void ReturnBossMonster(BossMonster Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.bossMap.Add(Obj.unitNo, Obj);
    }
    #endregion
}

