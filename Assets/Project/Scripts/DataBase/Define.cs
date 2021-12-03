using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOD.Define
{

    public enum Scenes
    {
        LobbyScene,
        GameScene,
        InventoryScene,
        LoadingScene
    }

    public enum monsterEnum
    {
        빨강슬라임 = 20001,
        초록슬라임,
        하늘슬라임,
        보라슬라임,
        검정슬라임,
        고블린,
        단검고블린,
        다트고블린,
        샤먼고블린,
        큰고블린,
    }
    public enum skillEnum
    {
        연속찌르기 = 10001,
        돌진,
        회전공격,
        내려찍기,
        검기날리기,
        z자베기,
        밀쳐내기,
        흘리기,
        회복,
    }
    
    public enum bossEnum
    {
        거대나무 = 40001,
        고블린챔피언,
    }
    public enum characterEnum 
    {
        기사 = 30001,

    }

    public enum IntroPhase
    {
        Start, // 스타트
        ApplicationSetting, // 앱 세팅
        StaticData, // 기획데이터 초기화 및 로드
        UserData, // 유저데이터 로드
        Compelte //완료
    }
    class Define
    {
    }
}
