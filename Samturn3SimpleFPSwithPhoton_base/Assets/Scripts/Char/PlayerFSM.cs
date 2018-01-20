using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerFSM : FSMBase
{
	public float moveSpeed= 1f;

	public Vector3 target ;  
	public Vector3 po;

	public Vector3 chardirXZ = new Vector3 (0f, 0f, 0f);

	public float h;
	public float v;

	protected override void Awake()
	{
		base.Awake ();
		target =  new Vector3 (0f, 0f, 0f);

	}

	void Update()
	{

		if ( state == CHARCTERSTATE.Dead ) {
			return;
		}


	}

	protected override IEnumerator Idle()
	{
		do
		{
			yield return null;
			//상태 처리
		} while(!_isNewState);
		//Exit
	}

	protected virtual IEnumerator Run()
	{
		//Entry
		do
		{
			yield return null;

			chardirXZ = new Vector3(h*moveSpeed*Time.deltaTime,0f,v*moveSpeed*Time.deltaTime);
			_cc.Move(chardirXZ);
			po = new Vector3(_cc.transform.position.x,0,_cc.transform.position.z);  // because strp off
			_cc.transform.position = po; 

//	
//				transform.position = Vector3.MoveTowards(_cc.transform.position,target,5f * Time.deltaTime);
//
//				float d = Vector3.Distance(transform.position, target);
//				if(d == 0){
//					SetState (CharacterState.Idle);
//				}

		} while(!_isNewState);
		//Exit
	}

	protected virtual IEnumerator Hit()
	{
		//Entry
		do
		{
			yield return null;
			//상태 처리
			if (_a.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
			{
				SetState(CHARCTERSTATE.Idle);
				break;
			}
			
		} while(!_isNewState);
		
		//Exit
	}
	protected virtual IEnumerator Attack()
	{
		do
		{
			yield return null;
			//상태 처리
			//----------------------------------------------------------------
			if (_a.GetCurrentAnimatorStateInfo(0).IsName("Attack") && _a.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
			{
				SetState(CHARCTERSTATE.Idle);
				break;
			}

		} while(!_isNewState);
	}

	protected virtual IEnumerator Hurray()
	{
		do
		{
			yield return null;
			//상태 처리

		} while(!_isNewState);
		//Exit
	}
	protected virtual IEnumerator HurrayOnce()
	{
		do
		{
			yield return null;
			//상태 처리
			if (_a.GetCurrentAnimatorStateInfo(0).IsName("HurrayOnce"))
			{
				SetState(CHARCTERSTATE.Idle);
				break;
			}
		} while(!_isNewState);
		//Exit
	}
	protected virtual IEnumerator WinPoseRepeat() {
		do
		{
			yield return null;
			//상태 처리

		} while(!_isNewState);
	}

	protected virtual IEnumerator WinPose1() {
		do
		{
			yield return null;
			//상태 처리
			if (_a.GetCurrentAnimatorStateInfo(0).IsName("WinPose1"))
			{
				SetState(CHARCTERSTATE.Idle);
				break;
			}
		} while(!_isNewState);
	}

	protected virtual IEnumerator Dead()
	{
		do
		{
			yield return null;
			//상태 처리

		} while(!_isNewState);
		//yield return new WaitForSeconds (1f);
		//Destroy (gameObject);

	}

}
