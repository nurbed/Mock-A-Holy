using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class God : MonoBehaviour {

    [SerializeField]
    AudioSource m_audioSource;
    [SerializeField]
    AudioClip Drum1;

    [SerializeField]
    AudioClip Good1;

    [SerializeField]
    AudioClip Good2;

    float Timer;
    int TURN = 0;
    [SerializeField] Slider ScoreSlider;

    // Use this for initialization
    void Start () {
        m_audioSource.Play();
        Timer = StaticConf.DELTA_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticConf.SCORE < 0)
            StaticConf.SCORE = 0;
        ScoreSlider.value = StaticConf.SCORE;

        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 0)
            {
                if (Random.Range(0, 100) < 50)
                {
                    m_audioSource.PlayOneShot(Good1);
                    StaticConf.RIGHT_MOVE = 1;
                }
                else
                {
                    m_audioSource.PlayOneShot(Good2);
                    StaticConf.RIGHT_MOVE = 2;
                }
            }

            if (TURN == 2)
            {
                TURN = 0;
            }
            else
            {
                TURN++;
            }
            m_audioSource.PlayOneShot(Drum1);
        }
    }
}
