using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour
{
    public GameObject m_Sciamano;

    public GameObject m_AdeptiFirstRow;
    public GameObject m_AdeptiSecondRow;
    public GameObject m_AdeptiThirdRow;

    private GameObject m_SelectedRow;
    private int m_numAdepto;

    private CharacterAnimController m_oHumanCtrl;

    private GameManager GM;

    private float m_Timer = 0;
    private int m_Turn = 0;

    private CharacterAnimController[] m_vAnimControllerFirst;
    private CharacterAnimController[] m_vAnimControllerSecond;
    private CharacterAnimController[] m_vAnimControllerThird;

    void Awake()
    {
        GM = GameManager.Instance;


        //GM.OnStateChange += HandleOnStateChange;
    }

    // Use this for initialization
    void Start()
    {
        GM.SetGameState(GameState.GAME_SCIAMANO);
        if (GM.GameState == GameState.GAME_SCIAMANO)
        {
            m_oHumanCtrl = m_Sciamano.GetComponent<CharacterAnimController>();
        }
        else if (GM.GameState == GameState.GAME_ADEPTO)
        {

        }

        m_vAnimControllerFirst = new CharacterAnimController[m_AdeptiFirstRow.transform.childCount];
        for (int idx = 0; idx < m_AdeptiFirstRow.transform.childCount; ++idx)
        {
            var child = m_AdeptiFirstRow.transform.GetChild(idx);
            m_vAnimControllerFirst[idx] = child.gameObject.GetComponent<CharacterAnimController>();
        }

        m_vAnimControllerSecond = new CharacterAnimController[m_AdeptiSecondRow.transform.childCount];
        for (int idx = 0; idx < m_AdeptiSecondRow.transform.childCount; ++idx)
        {
            var child = m_AdeptiSecondRow.transform.GetChild(idx);
            m_vAnimControllerSecond[idx] = child.gameObject.GetComponent<CharacterAnimController>();
        }

        m_vAnimControllerThird = new CharacterAnimController[m_AdeptiThirdRow.transform.childCount];
        for (int idx = 0; idx < m_AdeptiThirdRow.transform.childCount; ++idx)
        {
            var child = m_AdeptiThirdRow.transform.GetChild(idx);
            m_vAnimControllerThird[idx] = child.gameObject.GetComponent<CharacterAnimController>();
        }


        m_Timer = 0;
        m_Turn = 0;
    }

    private void StartAnim(CharacterAnimController animCtrl, CharacterAnimController.AnimType newAnim, bool withBolt, int numAdepto, int idx)
    {
        if (withBolt)
        {
            if (numAdepto == idx)
            {
                switch (newAnim)
                {
                    case CharacterAnimController.AnimType.DOWN:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.DOWN_BOLT);
                        break;
                    case CharacterAnimController.AnimType.UP:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.UP_BOLT);
                        break;
                    case CharacterAnimController.AnimType.LEFT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.LEFT_BOLT);
                        break;
                    case CharacterAnimController.AnimType.RIGHT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.RIGHT_BOLT);
                        break;
                }
            }
            else if (idx == numAdepto - 1)
            {
                switch (newAnim)
                {
                    case CharacterAnimController.AnimType.DOWN:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.DOWN_FEAR_DX);
                        break;
                    case CharacterAnimController.AnimType.UP:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.UP_FEAR_DX);
                        break;
                    case CharacterAnimController.AnimType.LEFT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.LEFT_FEAR);
                        break;
                    case CharacterAnimController.AnimType.RIGHT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.RIGHT_FEAR);
                        break;
                }
            }
            else if (idx == numAdepto + 1)
            {
                switch (newAnim)
                {
                    case CharacterAnimController.AnimType.DOWN:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.DOWN_FEAR_SX);
                        break;
                    case CharacterAnimController.AnimType.UP:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.UP_FEAR_SX);
                        break;
                    case CharacterAnimController.AnimType.LEFT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.LEFT_FEAR);
                        break;
                    case CharacterAnimController.AnimType.RIGHT:
                        animCtrl.StartAnim(CharacterAnimController.AnimType.RIGHT_FEAR);
                        break;
                }
            }
        }
        else
        {
            animCtrl.StartAnim(newAnim);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 1f)
        {
            m_Timer -= 1f;
            ++m_Turn;
        }

        if (m_Turn >= 3)
            m_Turn = 0;

        CharacterAnimController.AnimType newAnim = CharacterAnimController.AnimType.NONE;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            newAnim = CharacterAnimController.AnimType.UP;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            newAnim = CharacterAnimController.AnimType.DOWN;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            newAnim = CharacterAnimController.AnimType.RIGHT;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            newAnim = CharacterAnimController.AnimType.LEFT;

        if (newAnim != CharacterAnimController.AnimType.NONE && m_oHumanCtrl.IsOnIdle())
        {
            if (GM.GameState == GameState.GAME_SCIAMANO)
            {
                m_oHumanCtrl.StartAnim(newAnim);

                int numRow = Random.Range(0, 5);
                int numAdepto = -1;

                if (numRow == 0)
                {
                    numAdepto = Random.Range(0, m_vAnimControllerFirst.Length);
                }
                else if (numRow == 1)
                {
                    numAdepto = Random.Range(0, m_vAnimControllerSecond.Length);
                }
                else if (numRow == 2)
                {
                    numAdepto = Random.Range(0, m_vAnimControllerThird.Length);
                }

                for (int idx = 0; idx < m_vAnimControllerFirst.Length; ++idx)
                {
                    StartAnim(m_vAnimControllerFirst[idx], newAnim, numRow == 0, numAdepto, idx);
                }
                for (int idx = 0; idx < m_vAnimControllerSecond.Length; ++idx)
                {
                    StartAnim(m_vAnimControllerSecond[idx], newAnim, numRow == 1, numAdepto, idx);
                }
                for (int idx = 0; idx < m_vAnimControllerThird.Length; ++idx)
                {
                    StartAnim(m_vAnimControllerThird[idx], newAnim, numRow == 2, numAdepto, idx);
                }
            }
            else
            {

            }
        }
    }
}
