using UnityEngine;
using System.Collections;

public class God : MonoBehaviour {

	bool enableInput = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (StaticConf.TURN == 0) {
			StartCoroutine (EnableInput());
		}
	}

	IEnumerator EnableInput()
	{
		enableInput = true;
		yield return new WaitForSeconds(2);
		enableInput = false;
		StaticConf.TURN = 1;
	}
}
