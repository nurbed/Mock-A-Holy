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

	void Start () {
		m_audioSource.Play ();
		Timer = StaticConf.DELTA_TIME;
	}

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
                    StaticConf.TURN_REWIND = Random.Range(3,3);
                }
                
                
            }
            m_audioSource.PlayOneShot(Drum1);

        }

        float firstAxisValue = Input.GetAxis("FirstVertical");
        float secondAxisValue = Input.GetAxis("SecondVertical");
        if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER) {
			m_Animator.SetTrigger ("Move0");
			CheckScore ();
		}
        else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER)
		{
			m_Animator.SetTrigger ("Move1");
			CheckScore ();
		}

        firstAxisValue = Input.GetAxis("FirstHorizontal");
        secondAxisValue = Input.GetAxis("SecondHorizontal");
        if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER) {
			m_Animator.SetTrigger ("Move2");
			CheckScore ();
		}
        else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER) {
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
