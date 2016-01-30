using UnityEngine;
using System.Collections;

public class Santone : MonoBehaviour {

	[SerializeField] AudioSource m_audioSource;
	[SerializeField] Animator m_Animator;
	[SerializeField] AudioClip Drum1;
	[SerializeField] AudioClip Drum2;

	bool enableInput = false;
	float Timer;

	void Start () {
		m_audioSource.Play ();
		Timer = StaticConf.DELTA_TIME;
	}

	void Update () 
	{
		Timer -= 0.005f;
		if (StaticConf.TURN == 1 && Timer < 0) {
			Timer =  StaticConf.DELTA_TIME;
			StartCoroutine (EnableInput());
		}

		if (Input.GetButtonDown ("Fire1_1")) {
			m_Animator.SetTrigger ("Move0");
			CheckScore ();
		}
		if (Input.GetButtonDown ("Fire2_1")) {
			m_Animator.SetTrigger ("Move1");
			CheckScore ();
		}
		if (Input.GetButtonDown ("Fire3_1")) {
			m_Animator.SetTrigger ("Move2");
			CheckScore ();
		}
		if (Input.GetButtonDown ("Fire4_1")) {
			m_Animator.SetTrigger ("Move2Flipped");
			CheckScore ();
		}
	}

	IEnumerator EnableInput()
	{
		m_audioSource.PlayOneShot (Drum1);
		enableInput = true;
		yield return new WaitForSeconds(2);
		enableInput = false;
		m_audioSource.PlayOneShot (Drum2);
	}

	void CheckScore ()
	{
		if (StaticConf.TURN == 1)
			StaticConf.SCORE += 5;
		else
			StaticConf.SCORE -= 5;
	}
}
