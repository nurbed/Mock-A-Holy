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

    float Timer;
    int TURN = 0;

    void Start () {
		id = this.transform.GetSiblingIndex ();
		m_Animator = GetComponent<Animator>();

        Timer = StaticConf.DELTA_TIME;
    }

	void Update () {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 2)
            {
                // Per i giocatori umani abilito l'input, per i giocatori AI eseguo una animazione a caso
                if (IsRealPlayer)
                {
                    enableInput = true;
                    StartCoroutine(EnableInput());
                }
                else if (m_animationId == -1)
                {
                    m_animationId = Random.Range(1, 4);
                }

                TURN = 0;
            }
            else TURN++;
        }

        if (!m_Animator.IsInTransition(0) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(StaticConf.IdleState))
        {
            if(m_animationId != -1)
            {
                switch (m_animationId)
                {
                    case 1:
                        m_Animator.SetTrigger("Move0");
                        break;
                    case 2:
                        m_Animator.SetTrigger("Move1");
                        break;
                    case 3:
                        m_Animator.SetTrigger("Move2");
                        break;
                    case 4:
                        m_Animator.SetTrigger("Move2Flipped");
                        break;
                }
                CheckScore();
                m_animationId = -1;
            }
        }
    }

	IEnumerator EnableInput()
	{
		yield return new WaitForSeconds (3);
        enableInput = false;
	}

	void CheckScore()
	{
		if (TURN == 2 && enableInput == true)
			StaticConf.SCORE += 5;
		else
			StaticConf.SCORE -= 5;
	}
		
}
