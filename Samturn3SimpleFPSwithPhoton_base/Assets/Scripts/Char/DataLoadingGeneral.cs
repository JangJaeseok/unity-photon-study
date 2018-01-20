using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum ARMYTYPE{
	CAVALRY = 0,
	INFANTRY = 1,
	ARCHER   = 2,
	MAGIC    = 3,
	GENERAL  = 4,
	SOLDIER  = 5,
	SMASHER  = 6,
}


public class DataLoadingGeneral : MonoBehaviour {
	
	void Awake () {
		GeneralDataSetup ();  
	}

	void GeneralDataSetup(){
		GeneralDataInsert((int)TOY.JOJARYONG     	,0,6,48,50000,00000,7 ,0,"조운"    ,"趙雲"     ,(int)ARMYTYPE.CAVALRY ,"t_jojaryong"     ,"m_e_jojaryong"     ,"d_body_jojaryong"	 ,"");
		GeneralDataInsert((int)TOY.JANGBI        	,0,6,48,50000,00000,6 ,0,"장비"    ,"張飛"     ,(int)ARMYTYPE.INFANTRY,"t_jangbi"        ,"m_e_jangbi"        ,"d_body_jangbi"		 ,"");
		GeneralDataInsert((int)TOY.GWANU         	,1,6,50,50000,00000,5 ,0,"관우"    ,"關羽"     ,(int)ARMYTYPE.ARCHER  ,"t_gwanu"         ,"m_e_gwanu"         ,"d_body_gwanu"		 ,"");
		GeneralDataInsert((int)TOY.JEGALRYANG    	,0,6,51,50000,00000,29,1,"제갈량"  ,"諸葛亮"   ,(int)ARMYTYPE.MAGIC   ,"t_jegalryang"    ,"m_e_jegalryang"    ,"d_body_jegalryang"	 ,"d_helmet_jegalryang");  // basecap
		GeneralDataInsert((int)TOY.YUBI             ,0,6,45,50000,00000,26,0,"유비"    ,"劉備"     ,(int)ARMYTYPE.GENERAL ,"t_yubi"          ,"m_e_yubi"          ,"d_body_yubi"		     ,"");
		GeneralDataInsert((int)TOY.JOIN          	,0,5,35,00000,100000,4 ,0,"조인"    ,"曹仁"     ,(int)ARMYTYPE.CAVALRY ,"t_join"          ,"m_e_join"          ,"d_body_join"		     ,"");
		GeneralDataInsert((int)TOY.HAHUDON       	,0,6,47,50000,00000,1 ,0,"하후돈"  ,"夏侯惇"   ,(int)ARMYTYPE.INFANTRY,"t_hahudon"       ,"m_e_hahudon"       ,"d_body_hahudon"	     ,"");
		GeneralDataInsert((int)TOY.HAHUYEON      	,0,6,46,50000,00000,2 ,0,"하후연"  ,"夏侯淵"   ,(int)ARMYTYPE.ARCHER  ,"t_hahuyeon"      ,"m_e_hahuyeon"      ,"d_body_hahuyeon"	     ,"");
		GeneralDataInsert((int)TOY.SAMAUI        	,0,6,50,50000,00000,3 ,1,"사마의"  ,"司馬懿"   ,(int)ARMYTYPE.MAGIC   ,"t_samaui"        ,"m_e_samaui"        ,"d_body_samaui"		 ,"d_helmet_samaui");  // basecap
		GeneralDataInsert((int)TOY.JOJO          	,0,6,50,50000,00000,26,0,"조조"    ,"曹操"     ,(int)ARMYTYPE.GENERAL ,"t_jojo"          ,"m_e_jojo"          ,"d_body_jojo"		     ,"");
		GeneralDataInsert((int)TOY.TAESAJA       	,0,6,49,50000,00000,25,0,"태사자"  ,"太史慈"   ,(int)ARMYTYPE.CAVALRY ,"t_taesaja"       ,"m_e_taesaja"       ,"d_body_taesaja"		 ,"");
		GeneralDataInsert((int)TOY.GAMNYEONG        ,0,6,49,50000,00000,1 ,0,"감녕"    ,"甘寧"     ,(int)ARMYTYPE.INFANTRY,"t_gamnyeong"     ,"m_e_gamnyeong"     ,"d_body_gamnyeong"	 ,"");
		GeneralDataInsert((int)TOY.JUYU          	,0,6,49,50000,00000,2 ,0,"주유"    ,"周瑜"     ,(int)ARMYTYPE.ARCHER  ,"t_juyu"          ,"m_e_juyu"          ,"d_body_juyu"		     ,"");
		GeneralDataInsert((int)TOY.YUKSON        	,0,6,49,50000,00000,3 ,0,"육손"    ,"陸遜"     ,(int)ARMYTYPE.MAGIC   ,"t_yukson"        ,"m_e_yukson"        ,"d_body_yukson"		 ,"");
		GeneralDataInsert((int)TOY.SONGWON       	,0,6,48,50000,00000,26,0,"손권"    ,"孫權"     ,(int)ARMYTYPE.GENERAL ,"t_songwon"       ,"m_e_songwon"       ,"d_body_songwon"		 ,"");
		GeneralDataInsert((int)TOY.YEOPO         	,0,6,52,50000,00000,8 ,0,"여포"    ,"呂布"     ,(int)ARMYTYPE.CAVALRY ,"t_yeopo"         ,"m_e_yeopo"         ,"d_body_yeopo"		 ,"");
		GeneralDataInsert((int)TOY.HWAUNG        	,0,5,35,00000,10000,1 ,0,"화웅"    ,"華雄"     ,(int)ARMYTYPE.INFANTRY,"t_hwaung"        ,"m_e_hwaung"        ,"d_body_hwaung"		 ,"");
		GeneralDataInsert((int)TOY.SEOYEONG      	,0,5,34,00000,9000,2 ,0,"서영"    ,"徐榮"     ,(int)ARMYTYPE.ARCHER  ,"t_seoyeong"      ,"m_e_seoyeong"      ,"d_body_seoyeong"	     ,"");
		GeneralDataInsert((int)TOY.IYU           	,0,5,40,00000,40000,3 ,1,"이유"    ,"李儒"     ,(int)ARMYTYPE.MAGIC   ,"t_iyu"           ,"m_e_iyu"           ,"d_body_iyu"			 ,"d_helmet_iyu");       // basecap
		GeneralDataInsert((int)TOY.DONGTAK       	,0,5,38,00000,50000,23,1,"동탁"    ,"董卓"     ,(int)ARMYTYPE.GENERAL ,"t_dongtak"       ,"m_e_dongtak"       ,"d_body_dongtak"		 ,"d_helmet_dongtak");   // basecap
																																															                                
		GeneralDataInsert((int)TOY.SOLDIER       	,0,-1,01,00000,1000,1,0,"병사"    ,"兵士"     ,(int)ARMYTYPE.SOLDIER ,"t_soldierchok"   ,"m_e_soldier"       ,"d_body_soldierCHOK"   ,"");	
	}		
	void GeneralDataInsert(int num_general,int iskin,int grade,int power,int priceJewel,int pricegold,int iweapon,int bbasecap,string korname,string chname,int armytype,string s_char ,string m_e,string d_body,string d_helmet){
		DataGeneral a = new DataGeneral ();   
		a.iSkin     = iskin;
		a.grade     = grade;
		a.power     = power;
		a.priceJewel= priceJewel;
		a.priceGold = pricegold;
		a.iweapon   = iweapon;
		a.basecap   = bbasecap;
		a.korName   = korname;
		a.chName    = chname;
		a.armyType  = armytype;
		a.SsChar    = Resources.Load<Sprite>("Data/Char/s_char/"+s_char);          // 2d general face
		a.eye       = Resources.Load("Data/Char/"+m_e      , typeof(GameObject)) as GameObject; // eye model
		a.Tbody     = Resources.Load("Data/Char/"+d_body   , typeof(Texture2D)) as Texture2D;   // body texture
		a.tHelmet   = Resources.Load("Data/Char/"+d_helmet , typeof(Texture2D)) as Texture2D;   // basecap texture
		DataManager.Instance.dataGeneral.Add (a);
	}
}

