using UnityEngine;
using System.Collections;

public class ScenarioLuce : MonoBehaviour {

	Renderer m_ScenarioLuce ;
	[SerializeField]  Transform Fire1;
	[SerializeField]  Transform Fire2;
	[SerializeField]  Transform Fire3;
	[SerializeField]  Transform Fire4;

	float perc = StaticConf.SCORE / StaticConf.MAX_SCORE;
	float AlphaValue;
	float ScalePerc = 0.0f;
	Vector3 Fire1Scale;
	Vector3 Fire2Scale;
	Vector3 Fire3Scale;
	Vector3 Fire4Scale;

	// Use this for initialization
	void Start () {
		m_ScenarioLuce = GetComponent<Renderer> ();
		Fire1Scale = Fire1.transform.localScale;
		Fire2Scale = Fire2.transform.localScale;
		Fire3Scale = Fire3.transform.localScale;
		Fire4Scale = Fire4.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		AlphaValue = m_ScenarioLuce.material.color.a;
		perc = StaticConf.SCORE / StaticConf.MAX_SCORE;
		AlphaValue = 1*perc;
		//Fire1.transform.localScale = Fire1Scale * perc;
		//Fire2.transform.localScale = Fire2Scale * perc;
		//Fire3.transform.localScale = Fire3Scale * perc;
		//Fire4.transform.localScale = Fire4Scale * perc;
		m_ScenarioLuce.material.color = new Color (m_ScenarioLuce.material.color.r, m_ScenarioLuce.material.color.g, m_ScenarioLuce.material.color.b, AlphaValue);
	}
} 
