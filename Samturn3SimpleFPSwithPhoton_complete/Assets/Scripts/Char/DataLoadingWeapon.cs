using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DataLoadingWeapon : MonoBehaviour {

	void Awake () {
		WeaponDataSetup();   
		DataManager.Instance.iDataWeaponCount = DataManager.Instance.dataWeapon.Count;
	}

	public void WeaponDataSetup(){
		WeaponDataInsert ("itm_sword"          , 0,"item_yubi_sword");
		WeaponDataInsert ("itm_hahudon_sword"  , 1,"item_sword_ha");
		WeaponDataInsert ("itm_bow"            , 2,"item_bow");
		WeaponDataInsert ("itm_fan_normal"     , 3,"item_fan_normal");
		WeaponDataInsert ("itm_sword"          ,26,"item_yubi_sword");
		WeaponDataInsert ("itm_fan"            ,29,"item_jegalryang_fan");

	}

	void WeaponDataInsert(string m_w,int itemnumber,string t_weapon){
		DataWeapon a = new DataWeapon();

		a.m_weapon       = Resources.Load("Data/Item/"+m_w, typeof( GameObject )) as GameObject;
		a.num_weapon     = itemnumber; 
		a.t_weapon       = Resources.Load("Data/Item/"+t_weapon , typeof(Texture2D)) as Texture2D; 
		DataManager.Instance.dataWeapon.Add(a);
	}
}
