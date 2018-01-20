using UnityEngine;
using System.Collections;

public class DataLoadingHelmet : MonoBehaviour {

	void Awake () {
		HelmetDataSetup(); 
		DataManager.Instance.iDataHelmetCount = DataManager.Instance.dataHelmet.Count;
	}
	
	void HelmetDataSetup(){
		HelmetDataInsert((int)TOY.JOJARYONG     	,"m_h_jojaryong"     );
		HelmetDataInsert((int)TOY.JANGBI        	,"m_h_jangbi"        );
		HelmetDataInsert((int)TOY.GWANU         	,"m_h_gwanu"         );
		HelmetDataInsert((int)TOY.JEGALRYANG    	,"m_h_basecap"       );  //  m_h_basecap
		HelmetDataInsert((int)TOY.YUBI              ,"m_h_yubi"          );
		HelmetDataInsert((int)TOY.JOIN          	,"m_h_join"          );
		HelmetDataInsert((int)TOY.HAHUDON       	,"m_h_hahudon"       );
		HelmetDataInsert((int)TOY.HAHUYEON      	,"m_h_hahuyeon"      );
		HelmetDataInsert((int)TOY.SAMAUI        	,"m_h_basecap"       );  //  m_h_basecap  
		HelmetDataInsert((int)TOY.JOJO          	,"m_h_jojo"          );
		HelmetDataInsert((int)TOY.TAESAJA       	,"m_h_taesaja"       );
		HelmetDataInsert((int)TOY.GAMNYEONG       	,"m_h_gamnyeong"     );
		HelmetDataInsert((int)TOY.JUYU          	,"m_h_juyu"          );
		HelmetDataInsert((int)TOY.YUKSON        	,"m_h_yukson"        );
		HelmetDataInsert((int)TOY.SONGWON       	,"m_h_songwon"       );
		HelmetDataInsert((int)TOY.YEOPO         	,"m_h_yeopo"         );
		HelmetDataInsert((int)TOY.HWAUNG        	,"m_h_hwaung"        );
		HelmetDataInsert((int)TOY.SEOYEONG      	,"m_h_seoyeong"      );
		HelmetDataInsert((int)TOY.IYU           	,"m_h_basecap"       );  //  m_h_basecap
		HelmetDataInsert((int)TOY.DONGTAK       	,"m_h_basecap"       );  //  m_h_basecap

		HelmetDataInsert((int)TOY.SOLDIER       	,"m_h_soldier"       );	
	}					

	void HelmetDataInsert(int num_general ,string m_h){
		DataHelmet a = new DataHelmet(); 
		a.helmet  = Resources.Load("Data/Char/"+m_h , typeof(GameObject)) as GameObject; 
		DataManager.Instance.dataHelmet.Add(a);
	}
}
