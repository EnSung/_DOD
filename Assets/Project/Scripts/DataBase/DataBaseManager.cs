using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using DOD.Define;
using System;

namespace DOD.DB
{
    
    public class DataBaseManager : Singleton<DataBaseManager>
    {

        public Dictionary<int, TDSkill> tdSkillDict = new Dictionary<int, TDSkill>();
        public Dictionary<int, TDCharacter> tdCharacterDict = new Dictionary<int, TDCharacter>();

        public Dictionary<int, TDMonster> tdMonsterDict = new Dictionary<int, TDMonster>();
        public Dictionary<int, TDBoss> tdBossDict = new Dictionary<int, TDBoss>();

        public List<TDUserRank> userRankDict = new List<TDUserRank>();




        public List<string> tipText = new List<string>();
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void LoadTable()
        {
            LoadSkillTable();
            LoadMonsterTable();
            LoadCharacterTable();
            LoadTipText();
            LoadBossTable();
        }


        void LoadMonsterTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Monster_Json"); // Json 불러오기  

            tdMonsterDict.Clear();

            JObject parsedObj = new JObject(); //Json Object 생성

            parsedObj = JObject.Parse(jsonText.text);  //제이슨 파싱

            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDMonster tdMonster = new TDMonster();

                tdMonster.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdMonsterDict.Add(tdMonster.unitNo, tdMonster);
            }

            Debug.Log("몬스터 테이블 완료");

        }

        public void LoadRankData()
        {
            Debug.Log("랭크실행");

            userRankDict.Clear();

            StartCoroutine(rank());



        }


        public IEnumerator rank()
        {
            Debug.Log("랭크코루틴 실행");




            UnityWebRequest www = UnityWebRequest.Get("http://15.165.160.44:3000/show_rank");
            yield return www.Send();

            string a = www.downloadHandler.text;

            Debug.Log(a);


            JObject parsedObj = new JObject(); //Json Object 생성

            parsedObj = JObject.Parse(a);

            JArray jArray = new JArray();
            jArray = JArray.Parse(parsedObj["data"].ToString());

            foreach (JObject jo in jArray)
            {

                TDUserRank tdrank = new TDUserRank();

                tdrank.SetJsonData(jo);


                userRankDict.Add(tdrank);
            }
            Debug.Log("랭크 테이블 완료");
        }

        void LoadSkillTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Skill_Json"); // Json 불러오기

            tdSkillDict.Clear();

            JObject parsedObj = new JObject(); // Json Object 생성


            parsedObj = JObject.Parse(jsonText.text); // Json Parsing



            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDSkill tdSkill = new TDSkill();

                tdSkill.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());

                tdSkillDict.Add(tdSkill.SKey, tdSkill);
            }
            Debug.Log("스킬 테이블 완료");
        }


        void LoadCharacterTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Character_Json"); // Json 불러오기

            Debug.Log(jsonText);
            tdCharacterDict.Clear();

            JObject parsedObj = new JObject(); // Json Object 생성


            parsedObj = JObject.Parse(jsonText.text); // Json Parsing

            Debug.Log(parsedObj);


            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDCharacter tdCharacter= new TDCharacter();

                tdCharacter.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());

                tdCharacterDict.Add(tdCharacter.UnitNo, tdCharacter);
            }

            foreach (var item in tdCharacterDict.Keys)
            {
                Debug.Log(item);
            }
            Debug.Log("캐릭터 테이블 완료");
        }

        void LoadBossTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Boss_Json"); // Json 불러오기

            Debug.Log(jsonText);
            tdBossDict.Clear();

            JObject parsedObj = new JObject(); // Json Object 생성


            parsedObj = JObject.Parse(jsonText.text); // Json Parsing

            Debug.Log(parsedObj);


            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDBoss tdBoss = new TDBoss();

                tdBoss.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());

                tdBossDict.Add(tdBoss.UnitNo, tdBoss);
            }
            Debug.Log("보스 테이블 완료");
        }


        public void LoadTipText()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("Tip");
            string[] Tip = jsonText.text.Split(',');
            foreach (string text in Tip)
            {
                tipText.Add(text);
            }
        }
    }
}