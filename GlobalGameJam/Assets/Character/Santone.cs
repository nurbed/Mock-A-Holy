using UnityEngine;
using System.Collections;

public class Santone : MonoBehaviour {

	[SerializeField] AudioSource m_audioSource;
	[SerializeField] Animator m_Animator;

    [SerializeField]
    AudioClip Sant1;

    [SerializeField]
    AudioClip Sant2;


    bool enableInput = false;
	float Timer;
    int TURN = 0;

    void Start () {
		Timer = StaticConf.DELTA_TIME;
	}

	void Update () 
	{
		Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 2)
            {
                TURN = 0;
            }
            else
            {
                TURN++;
            }
        }

        if (!m_Animator.IsInTransition(0) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(StaticConf.IdleState))
        {
            float firstAxisValue = Input.GetAxis("FirstVertical");
            float secondAxisValue = Input.GetAxis("SecondVertical");
            if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER)
            {
                m_Animator.SetTrigger("Move0");
                m_audioSource.PlayOneShot(Sant1);
                CheckScore();
            }
            else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER)
            {
                m_Animator.SetTrigger("Move1");
                m_audioSource.PlayOneShot(Sant2);
                CheckScore();
            }

            firstAxisValue = Input.GetAxis("FirstHorizontal");
            secondAxisValue = Input.GetAxis("SecondHorizontal");
            if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER)
            {
                m_Animator.SetTrigger("Move2");
                CheckScore();
            }
            else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER)
            {
                m_Animator.SetTrigger("Move2Flipped");
                CheckScore();
            }
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
		if (TURN == 1)
			StaticConf.SCORE += 5;
		else
			StaticConf.SCORE -= 5;
	}
}
