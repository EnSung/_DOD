using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDUserRank : TableBase
{
    public string device_id;
    public string nickname;
    public string score;



    public void SetJsonData(JObject info)
    {
       this.device_id = info["device_id"].Value<string>();
       this.nickname = info["nickname"].Value<string>();
       this.score = info["score"].Value<string>();

    }
}

