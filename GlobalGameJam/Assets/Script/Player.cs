using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	private int id;
	private int m_animationId = -1;
	private bool m_isRealPlayer;
	Animator m_Animator;
	bool enableInput= false;

	public int AnimationId { get { return m_animationId; } set { m_animationId = value;	} }
	public bool IsRealPlayer { get { return m_isRealPlayer; } set {m_isRealPlayer = value; } }

	void Start () {
		id = this.transform.GetSiblingIndex ();
		m_Animator = GetComponent<Animator>();
	}

	void Update () {
		if (StaticConf.TURN == 2) {
			enableInput = true;
			StartCoroutine (EnableInput ());
		}

		if (m_animationId == 0) {
			m_Animator.SetTrigger ("Move5");
			CheckScore ();
			m_animationId = -1;
		}
		if (m_animationId == 1) {
			m_Animator.SetTrigger ("Move1");
			CheckScore ();
			m_animationId = -1;
		}
		if (m_animationId == 2) {
			m_Animator.SetTrigger ("Move1Flipped");
			CheckScore ();
			m_animationId = -1;
		}
		if (m_animationId == 3) {
			m_Animator.SetTrigger ("Move2");
			CheckScore ();
			m_animationId = -1;
		}
		if (m_animationId == 4) {
			m_Animator.SetTrigger ("Move2Flipped");
			CheckScore ();
			m_animationId = -1;
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
