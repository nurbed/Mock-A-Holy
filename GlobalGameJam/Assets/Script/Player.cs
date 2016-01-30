using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int id;
	public int Input = -1;
	public bool isRealPlayer;
	Animator m_Animator;
	bool enableInput= false;

	// Use this for initialization
	void Start () {
		id = this.transform.GetSiblingIndex ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StaticConf.TURN == 2) {
			enableInput = true;
			StartCoroutine (EnableInput ());
		}

		if (Input == 0) {
			m_Animator.SetTrigger ("Move5");
			CheckScore ();
			Input = -1;
		}
		if (Input == 1) {
			m_Animator.SetTrigger ("Move1");
			CheckScore ();
			Input = -1;
		}
		if (Input == 2) {
			m_Animator.SetTrigger ("Move1Flipped");
			CheckScore ();
			Input = -1;
		}
		if (Input == 3) {
			m_Animator.SetTrigger ("Move2");
			CheckScore ();
			Input = -1;
		}
		if (Input == 4) {
			m_Animator.SetTrigger ("Move2Flipped");
			CheckScore ();
			Input = -1;
		}
	}

	IEnumerator EnableInput()
	{
		yield return new WaitForSeconds (3);
		enabled = false;
	}

	void CheckScore()
	{
		if (StaticConf.TURN == 2 && enableInput == true)
			StaticConf.SCORE += 5;
		else
			StaticConf.SCORE -= 5;
	}
		
}
