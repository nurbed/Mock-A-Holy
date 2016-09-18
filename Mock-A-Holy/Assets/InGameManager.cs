using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour
{
    public static float ANALOGIC_TRIGGER = 0.6f;

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

    void Awake()
    {
        GM = GameManager.Instance;

        GM.SetGameState(GameState.GAME_SCIAMANO);
        //GM.OnStateChange += HandleOnStateChange;
    }

    // Use this for initialization
    void Start ()
    {
        if (GM.GameState == GameState.GAME_SCIAMANO)
        {
            m_oHumanCtrl = m_Sciamano.GetComponent<CharacterAnimController>();
        }
        else if(GM.GameState == GameState.GAME_ADEPTO)
        {

        }

        m_Timer = 0;
        m_Turn = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 1f)
        {
            m_Timer -= 1f;
            ++m_Turn;
        }

        if (m_Turn >= 3)
            m_Turn = 0;

        if (GM.GameState == GameState.GAME_SCIAMANO)
        {
            float firstAxisValue = Input.GetAxis("Vertical");
            float secondAxisValue = Input.GetAxis("Horizontal");
            //Debug.Log("axis value:" + firstAxisValue.ToString());
            CharacterAnimController.AnimType newAnim = CharacterAnimController.AnimType.NONE;
            if (firstAxisValue > ANALOGIC_TRIGGER)
                newAnim =  CharacterAnimController.AnimType.UP;
            else if (firstAxisValue < -ANALOGIC_TRIGGER)
                newAnim = CharacterAnimController.AnimType.DOWN;
            if (secondAxisValue > ANALOGIC_TRIGGER)
                newAnim = CharacterAnimController.AnimType.RIGHT;
            else if (secondAxisValue < -ANALOGIC_TRIGGER)
                newAnim = CharacterAnimController.AnimType.LEFT;

            if (newAnim != CharacterAnimController.AnimType.NONE)
            {
                m_oHumanCtrl.StartAnim(newAnim);

                foreach (var child in m_AdeptiFirstRow.transform)
                {
                    CharacterAnimController animCtrl = ((GameObject)child).GetComponent<CharacterAnimController>();
                    if (animCtrl != null)
                        animCtrl.StartAnim(newAnim);
    
            }
            }
        }
        else
        {

        }
    }
}
