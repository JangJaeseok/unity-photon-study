using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CCM : Photon.MonoBehaviour {
	static CCM _sinstance;

	public static CCM Instance
	{
		get
		{
			return _sinstance;
		}
	}
	
	void Awake () {

		if ( _sinstance == null ) 
		{
			_sinstance = this;

			DontDestroyOnLoad(this);
		}
		else 
		{
			Destroy(gameObject);
		}
	}

	public DataCharacter CharacterSetting(int id,GameObject basebody, int num_general, float charX, float charY, float charZ,int countrycolor,int armytype) { 
		DataCharacter a = new DataCharacter ();

		int iarmyType = 0; 
		int icountry = countrycolor;
		int iHelmet = 0; 
		int iEye = 0; 
		int iBeard = -1; 
		int iTbody = 0; 
		int iskin = 0; 
		int iweapon = 0;
		int grade= 0;

		if (num_general == (int)TOY.SMASHER) {     // smasher
			a.playerChar = Instantiate (Resources.Load ("Data/Char/"+"smasher", typeof(GameObject))) as GameObject;

			a.playerChar.transform.Find ("yubi/Bone01/Bone02/Object01").gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.smasherTexture [countrycolor];
			a.playerChar.transform.Find ("yubi/Bone01/Bone04/Object04").gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.smasherTexture [countrycolor];
			a.playerChar.transform.Find ("yubi/Bone01/Bone06/Object03").gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.smasherTexture [countrycolor];
		
			iarmyType = (int)ARMYTYPE.SMASHER;
		} else {                                     // character
			//a.playerChar = Instantiate (Resources.Load ("basebody", typeof(GameObject))) as GameObject;   
			a.playerChar = basebody;

			if (num_general == (int)TOY.SOLDIER) {
				iHelmet = (int)TOY.SOLDIER; 
				iEye = (int)TOY.SOLDIER; 
				iTbody = (int)TOY.SOLDIER; 
				iBeard = 0;  // no beard
				iskin = DataManager.Instance.dataGeneral[(int)TOY.SOLDIER].iSkin; 
			} else {
				iHelmet = num_general; 
				iEye    = num_general; 
				iTbody  = num_general; 
				iskin   = DataManager.Instance.dataGeneral[num_general].iSkin; 
				a.playerChar.GetComponent<PlayerFSM> ().generalNumber = num_general;
			}

			if (armytype == -1) {  // Default armytype
				iarmyType = DataManager.Instance.dataGeneral[num_general].armyType; 
			} else {              // Change character armytype 
				iarmyType = armytype;  
			}

			for (int i = 0; i < DataManager.Instance.iDataBeardCount; i++) {
				if (num_general == DataManager.Instance.dataBeard[i].i_generalnumber) {
					iBeard = i;   // beard index
				}
			}
			//-----------------------------------Eye----------------------------------------------------------------------------------
			a.eye = Instantiate (DataManager.Instance.dataGeneral[iEye].eye) as GameObject;   
			//-----------------------------------Head--------------------------------------------------------------------------------------
			if (num_general == (int)TOY.JOJO) {
				a.head = Instantiate (Resources.Load ("Data/Char/"+"m_head_jojo", typeof(GameObject)))  as GameObject;    //jojo head
			} else {
				a.head = Instantiate (Resources.Load ("Data/Char/"+"m_head_base", typeof(GameObject)))  as GameObject;    //base head
			}
			a.head.gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.HeadTexture [iskin];
			//----------------------------------Helmet-----------------------------------------------------------------------------------------
			a.helmet = Instantiate (DataManager.Instance.dataHelmet[iHelmet].helmet)as GameObject;  //
			if (DataManager.Instance.dataGeneral[iHelmet].basecap == 1) {  // basecap texture
				a.helmet.gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.dataGeneral[iHelmet].tHelmet; 
			} 
			//---------------------------------Body Arm Foot--------------------------------------------------------------------------------------------
			a.playerChar.transform.Find ("yubi/Bip01/Bip01 Pelvis/Bip01 Spine/body").gameObject.GetComponent<Renderer> ().material.mainTexture
			= DataManager.Instance.dataGeneral[iTbody].Tbody;        // body texture
			a.playerChar.transform.Find ("yubi/armfoot").gameObject.GetComponent<Renderer> ().material.mainTexture
			= DataManager.Instance.Tarmfoot [icountry];  // arm foot texture
			//---------------------------------Soldier Body Arm Foot-----------------------------------------------------------------------------------
			if (num_general == (int)TOY.SOLDIER) {   // helmet, body texture
				a.helmet.gameObject.GetComponent<Renderer> ().material.mainTexture = DataManager.Instance.soldierHelmetTexture [icountry];      // change helmet texture countrycolor 
				a.playerChar.transform.Find ("yubi/Bip01/Bip01 Pelvis/Bip01 Spine/body").gameObject.GetComponent<Renderer> ().material.mainTexture	= DataManager.Instance.soldierBodyTexture [icountry];        // change body texture countrycolor
			}
			//----------------------------------------------------------------------------------------------------------------------------------------------
			a.point_head   = a.playerChar.transform.Find ("yubi/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Neck/Bip01 Head").GetComponent<Transform> ();
			a.point_weapon = a.playerChar.transform.Find ("yubi/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/point_weapon").GetComponent<Transform> ();

			a.head.transform.position   = a.playerChar.transform.position;    
			a.eye.transform.position    = a.playerChar.transform.position;   
			a.helmet.transform.position = a.playerChar.transform.position;   

			a.head.transform.SetParent (a.point_head.transform);              
			a.eye.transform.SetParent (a.point_head.transform);                
			a.helmet.transform.SetParent (a.point_head.transform);     

			int iweaponindex = 0;
			for (int i = 0; i < DataManager.Instance.dataWeapon.Count; i++) {
				if(iweapon == DataManager.Instance.dataWeapon[i].num_weapon){
					iweaponindex = i;
				}
			}
//			a.weapon = Instantiate (DataManager.Instance.dataWeapon[iweaponindex].m_weapon)as GameObject;    
//			a.weapon.transform.position = a.point_weapon.transform.position;  
//			a.weapon.transform.rotation = a.point_weapon.transform.rotation;  
//			a.weapon.transform.SetParent (a.point_weapon.transform);      

			if (iBeard >=0) {
				a.beard = Instantiate (DataManager.Instance.dataBeard[iBeard].m_beard)as GameObject;   // 
				a.beard.transform.position = a.playerChar.transform.position;    // 
				a.beard.transform.SetParent (a.point_head.transform);   //
			}	
		}

		a.playerChar.transform.position = new Vector3 (charX, charY, charZ);  
		a.playerChar.GetComponent<PlayerFSM> ().armytype = iarmyType;
		a.playerChar.GetComponent<PlayerFSM> ().grade    = grade;
		a.playerChar.GetComponent<PlayerFSM> ().CharId   = id;
		a.playerChar.GetComponent<PlayerFSM> ().country  = icountry;
		a.playerChar.GetComponent<PlayerFSM> ().generalNumber = num_general;

		return a;
	}
}
