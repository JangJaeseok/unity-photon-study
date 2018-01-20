using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GAMESTATE {
	MAIN        = 0,
	IDLE        = 1,
	PLAYING     = 2,
	STOPSHOOT   = 3,
	SKILL       = 4,
	STORE       = 5,
	END         = 6,

	Max
}

enum COUNTRYTYPE{
	CHOK = 0,
	WI   = 1,
	O    = 2,
	DONG = 3,
	HWANG = 4,
	Max,
}
public partial class DataManager : MonoBehaviour
{
	static DataManager _instance;

	public int iBundleVersion = 0;
	public int iGameState;
	public int iCurrentStage = 1;
	public bool bTest = false;

	public List<DataGeneral> dataGeneral;  
	public List<DataHelmet>  dataHelmet; 
	public List<DataBeard>   dataBeard;
	public List<DataWeapon>  dataWeapon;
	public List<DataPlayerLevel> dataPlayerLevelData;

	public Texture2D[] smasherTexture  ;       // smasher color
	public Texture2D[] Tarmfoot        ;       // CountryColor
	public Texture2D[] HeadTexture     ;       // skin color
	public Texture2D[] soldierHelmetTexture ;  // soldier helmet texture
	public Texture2D[] soldierBodyTexture   ; 

	public GameObject btnReset;

	public int iDataPlayerLevelData;
	public int iDataCharCount;
	public int iDataEyeCount;
	public int iDataSkinCount;
	public int iDataBeardCount;
	public int iDataHelmetCount;
	public int iDataBodyCount;
	public int iDataWeaponCount;
	public int iDataColorCount;
	public int iDataSmasherColorCount;
	public int iDataApUpgradeCount;
	public int iDataLanguageCount;

	public int id_char    = 0;
	public int id_color   = 0;
	public int id_skin    = 0;
	public int id_eye     = 0;
	public int id_beard   = 0;
	public int id_helmet  = 0;
	public int id_body    = 0;
	public int id_weapon  = 0;
	public int id_smasher = 0;

	public int iMyGold  = 1000;
	public int iMyJewel = 100;
	public int iMyExp   = 1;

	public static DataManager Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		if ( _instance == null ) //
		{
			_instance = this;

			//---------------------------------------------------------------------
			iDataSkinCount   = 7;
			iDataSmasherColorCount = 3;
			iDataWeaponCount = 31;
			iDataColorCount  = (int)COUNTRYTYPE.Max;

			dataGeneral          = new List<DataGeneral>();
			dataBeard     		 = new List<DataBeard>();
			dataWeapon    		 = new List<DataWeapon>();
			dataHelmet    		 = new List<DataHelmet>();
			dataPlayerLevelData  = new List<DataPlayerLevel>();

			HeadTexture = new Texture2D[iDataSkinCount];              //skin color
			HeadTexture[0] = Resources.Load("Data/Char/"+"d_head_01"     , typeof(Texture2D)) as Texture2D;
			HeadTexture[1] = Resources.Load("Data/Char/"+"d_head_02"     , typeof(Texture2D)) as Texture2D;
			HeadTexture[2] = Resources.Load("Data/Char/"+"d_head_gwanu"  , typeof(Texture2D)) as Texture2D;
			HeadTexture[3] = Resources.Load("Data/Char/"+"d_head_yeopo"  , typeof(Texture2D)) as Texture2D;
			HeadTexture[4] = Resources.Load("Data/Char/"+"d_head_03"     , typeof(Texture2D)) as Texture2D;
			HeadTexture[5] = Resources.Load("Data/Char/"+"d_head_choseon", typeof(Texture2D)) as Texture2D;
			HeadTexture[6] = Resources.Load("Data/Char/"+"d_head_jojo"   , typeof(Texture2D)) as Texture2D;

			Tarmfoot    = new Texture2D[iDataColorCount];             //country color
			Tarmfoot[(int)COUNTRYTYPE.CHOK  ]   = Resources.Load("Data/Char/"+"d_armfoot_chok"  , typeof(Texture2D)) as Texture2D;
			Tarmfoot[(int)COUNTRYTYPE.WI    ]   = Resources.Load("Data/Char/"+"d_armfoot_wi"    , typeof(Texture2D)) as Texture2D;
			Tarmfoot[(int)COUNTRYTYPE.O     ]   = Resources.Load("Data/Char/"+"d_armfoot_o"     , typeof(Texture2D)) as Texture2D;
			Tarmfoot[(int)COUNTRYTYPE.DONG  ]   = Resources.Load("Data/Char/"+"d_armfoot_dong"  , typeof(Texture2D)) as Texture2D;
			Tarmfoot[(int)COUNTRYTYPE.HWANG ]   = Resources.Load("Data/Char/"+"d_armfoot_hwang" , typeof(Texture2D)) as Texture2D;

			smasherTexture = new Texture2D[iDataSmasherColorCount];   //smasher color
			smasherTexture[0] = Resources.Load("Data/Char/"+"obj_smasher_chok" , typeof(Texture2D)) as Texture2D;
			smasherTexture[1] = Resources.Load("Data/Char/"+"obj_smasher_wi"   , typeof(Texture2D)) as Texture2D;
			smasherTexture[2] = Resources.Load("Data/Char/"+"obj_smasher_o"    , typeof(Texture2D)) as Texture2D;

			soldierHelmetTexture  = new Texture2D[(int)COUNTRYTYPE.Max];  //  
			soldierHelmetTexture[(int)COUNTRYTYPE.CHOK  ] = Resources.Load("Data/Char/"+"d_helmet_soldierCHOK" , typeof(Texture2D)) as Texture2D;
			soldierHelmetTexture[(int)COUNTRYTYPE.WI    ] = Resources.Load("Data/Char/"+"d_helmet_soldierWI"   , typeof(Texture2D)) as Texture2D;
			soldierHelmetTexture[(int)COUNTRYTYPE.O     ] = Resources.Load("Data/Char/"+"d_helmet_soldierO"    , typeof(Texture2D)) as Texture2D;
			soldierHelmetTexture[(int)COUNTRYTYPE.DONG  ] = Resources.Load("Data/Char/"+"d_helmet_soldierDONG" , typeof(Texture2D)) as Texture2D;
			soldierHelmetTexture[(int)COUNTRYTYPE.HWANG ] = Resources.Load("Data/Char/"+"d_helmet_soldierHWANG", typeof(Texture2D)) as Texture2D;

			soldierBodyTexture    = new Texture2D[(int)COUNTRYTYPE.Max];  //
			soldierBodyTexture[(int)COUNTRYTYPE.CHOK  ] = Resources.Load("Data/Char/"+"d_body_soldierCHOK" , typeof(Texture2D)) as Texture2D;  
			soldierBodyTexture[(int)COUNTRYTYPE.WI    ] = Resources.Load("Data/Char/"+"d_body_soldierWI"   , typeof(Texture2D)) as Texture2D;
			soldierBodyTexture[(int)COUNTRYTYPE.O     ] = Resources.Load("Data/Char/"+"d_body_soldierO"    , typeof(Texture2D)) as Texture2D;
			soldierBodyTexture[(int)COUNTRYTYPE.DONG  ] = Resources.Load("Data/Char/"+"d_body_soldierDONG" , typeof(Texture2D)) as Texture2D;
			soldierBodyTexture[(int)COUNTRYTYPE.HWANG ] = Resources.Load("Data/Char/"+"d_body_soldierHWANG", typeof(Texture2D)) as Texture2D;

			//--------------------------------------------------------------------------------------
			playerAudio      = GetComponent<AudioSource>();

			DontDestroyOnLoad(this);
		}
		else // 
		{
			Destroy(gameObject);
		}
	}
	void Update(){

	}

}
