using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField] InputManager m_oInputManager;

	// Use this for initialization
	void Start () {
		m_oInputManager.OnSlideToRight += SLideRight;
		m_oInputManager.OnSlideToLeft += SLideLeft;

		m_oInputManager.OnSlideToBottom += SLideBottom;

		m_oInputManager.OnSlideToTop += SLideTOp;
		m_oInputManager.OnSingleClick += Click;
		m_oInputManager.OnCLickContinuos += ClickContinuos;
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SLideRight ()
	{
		Debug.Log ("SLIDERIGHT");
	}
	void SLideLeft ()
	{
		Debug.Log ("SLIDELEFT");
	}
	void SLideBottom ()
	{
		Debug.Log ("SLIDEBOTTOM");
	}
	void SLideTOp ()
	{
		Debug.Log ("SLIDETOP");
	}
	void Click ()
	{
		Debug.Log ("TOP");
	}
	void ClickContinuos ()
	{
		Debug.Log ("CLICKCONTINUOS");
	}
}
