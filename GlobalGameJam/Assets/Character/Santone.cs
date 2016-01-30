using UnityEngine;
using System.Collections;

public class Santone : MonoBehaviour {

	[SerializeField] AudioSource m_audioSource;
	[SerializeField] Animator m_Animator;
	[SerializeField] AudioClip Drum1;

    [SerializeField]
    AudioClip Good1;

    [SerializeField]
    AudioClip Good2;


    bool enableInput = false;
	float Timer;

	// Use this for initialization
	void Start () {
		m_audioSource.Play ();
		Timer = StaticConf.DELTA_TIME;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            StaticConf.TURN++;
            if (StaticConf.TURN == 1)
            {
                if (Random.Range(0, 100) < 50)
                    m_audioSource.PlayOneShot(Good1);
                else
                    m_audioSource.PlayOneShot(Good2);
            }
            else
            {
                if (StaticConf.TURN == 2)
                {
                    StartCoroutine(EnableInput());
                }
                else
                    if (StaticConf.TURN == StaticConf.TURN_REWIND)
                {
                    StaticConf.TURN = 0;
                    StaticConf.TURN_REWIND = Random.Range(3,7);
                }
                
                
            }
            m_audioSource.PlayOneShot(Drum1);

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
		enableInput = true;
		yield return new WaitForSeconds(2);
		enableInput = false;

	}

	void CheckScore ()
	{
		if (StaticConf.TURN == 1)
			StaticConf.SCORE += 5;
		else
			StaticConf.SCORE -= 5;
	}

}
