using UnityEngine;
using System.Collections;

public enum CHARCTERSTATE {
	Idle         = 0,
	Run          = 1,
	Attack       = 2,
	Dead         = 3,
	Hit          = 4,
	Hurray       = 5,
	AttackArcher = 6,
	Skill1       = 7,
	WinPose1     = 8,
	WinPoseRepeat= 9,
	HurrayOnce   = 10,
	Max
}
public class FSMBase : MonoBehaviour
{
	public GameObject camera;
	public double KorName;
	public double EngName;
	public int    generalNumber;
	public int    armytype;
	public int    grade;
	public int    country;
	public int    lead;
	public int    power;
	public int    wisdom;
	public int    politics;
	public int    appeal;
	public int    loyalty;
	public int    iLevel=1;

	public int currentHP = 100;
	public int currentMP = 100;
	public int CharId;

	public CHARCTERSTATE state;

	public Animator _a;
	public CharacterController _cc;

	public bool _isNewState;
	public bool bCharAction = false;

	protected virtual void Awake()
	{
		_cc = GetComponent<CharacterController> ();
		_a = GetComponentInChildren<Animator> ();
	}

	protected virtual void OnEnable()
	{
		SetState (CHARCTERSTATE.Idle);

		StartCoroutine (FSMMain ());
	}

	public void SetState(CHARCTERSTATE newState)
	{
		_isNewState = true;
		state = newState;
		_a.SetInteger ("state", (int)state);
	}

	public bool IsDead()
	{
		return (state == CHARCTERSTATE.Dead);
	}


	IEnumerator FSMMain()
	{
		while(true)
		{
			_isNewState = false;
			yield return StartCoroutine(state.ToString());   // CharacterState state
		}
	}

	protected virtual IEnumerator Idle()
	{
		//Entry
		do
		{
			yield return null;
			//상태 처리

		} while(!_isNewState);

		//Exit
	}
}
