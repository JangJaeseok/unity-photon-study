using UnityEngine;
using System.Collections;

public class DataLoadingBeard : MonoBehaviour{

	void Awake() {
		BeardDataSetup();  
	}

	public void BeardDataSetup(){
		BeardDataInsert("m_b_yubi"          ,(int)TOY.YUBI);  // empty beard
		BeardDataInsert("m_b_jangbi"        ,(int)TOY.JANGBI);
		BeardDataInsert("m_b_gwanu"         ,(int)TOY.GWANU);
		BeardDataInsert("m_b_jegalryang"    ,(int)TOY.JEGALRYANG);
		BeardDataInsert("m_b_join"          ,(int)TOY.JOIN);
		BeardDataInsert("m_b_hahudon"       ,(int)TOY.HAHUDON);
		BeardDataInsert("m_b_hahuyeon"      ,(int)TOY.HAHUYEON);
		BeardDataInsert("m_b_samaui"        ,(int)TOY.SAMAUI);
		BeardDataInsert("m_b_jojo"          ,(int)TOY.JOJO);
		BeardDataInsert("m_b_hwaung"        ,(int)TOY.HWAUNG);
		BeardDataInsert("m_b_seoyeong"      ,(int)TOY.SEOYEONG);
		BeardDataInsert("m_b_iyu"           ,(int)TOY.IYU);
		BeardDataInsert("m_b_dongtak"       ,(int)TOY.DONGTAK);

		DataManager.Instance.iDataBeardCount    = DataManager.Instance.dataBeard.Count;
	}

	void BeardDataInsert(string m_b,int generalnumber){
		DataBeard a = new DataBeard();

		a.m_beard = ( GameObject )Resources.Load("Data/Char/"+m_b, typeof( GameObject ));
		a.i_generalnumber = generalnumber; 

		DataManager.Instance.dataBeard.Add(a);
	}
}
