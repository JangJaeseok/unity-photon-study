using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataPlayerLevel : ScriptableObject
{
	[System.Serializable]
	public class Attribute
	{
		public int level;

		public int reqExp;
    
	}

	public List<Attribute> list = new List<Attribute>();
}
