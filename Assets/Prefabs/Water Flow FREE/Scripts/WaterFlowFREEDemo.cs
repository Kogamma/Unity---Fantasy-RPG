// Water Flow FREE
// Version: 1.1.5
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:			Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:	https://www.assetstore.unity3d.com/en/#!/content/26434
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#endregion

// ######################################################################
// WaterFlowFREEDemo class
//
// This class handles switching demo water, displays and updates GUI of water shader.
// ######################################################################

public class WaterFlowFREEDemo : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Water Simple
	Color m_WaterSimple_Color;
	float m_WaterSimple_UMoveSpeed;
	float m_WaterSimple_VMoveSpeed;
	Color m_WaterSimpleOriginal_Color;
	float m_WaterSimpleOriginal_UMoveSpeed;
	float m_WaterSimpleOriginal_VMoveSpeed;

	// Water game objects
	public GameObject m_SimpleWater;

	// Water Setting panels
	public GameObject m_SimpleWaterSettingsPanel;

	// Sliders
	public Slider m_SimpleR, m_SimpleG, m_SimpleB, m_SimpleUSpeed, m_SimpleVSpeed;

	#endregion

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour


	void Start()
	{


	}


	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Slider Responder functions
	// ########################################

	#region Slider Responder

	// Red color
	void OnSlider_SimpleR(float value)
	{
		m_WaterSimple_Color = new Color(value,
									m_WaterSimple_Color.g,
									m_WaterSimple_Color.b,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// Green color
	void OnSlider_SimpleG(float value)
	{
		m_WaterSimple_Color = new Color(m_WaterSimple_Color.r,
									value,
									m_WaterSimple_Color.b,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// Blue color
	void OnSlider_SimpleB(float value)
	{
		m_WaterSimple_Color = new Color(m_WaterSimple_Color.r,
									m_WaterSimple_Color.g,
									value,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// U speed
	void OnSlider_SimpleUSpeed(float value)
	{
		m_WaterSimple_UMoveSpeed = value;
		m_SimpleWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedU", m_WaterSimple_UMoveSpeed);
	}

	// V speed
	void OnSlider_SimpleVSpeed(float value)
	{
		m_WaterSimple_VMoveSpeed = value;
		m_SimpleWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedV", m_WaterSimple_VMoveSpeed);
	}

	#endregion // Slider Responder

	// ########################################
	// Button Responder
	// ########################################

	#region Button Responder

	// Open full verion page on Unity Asset Store
	public void OnFindMoreInFullVersion()
	{
		// http://docs.unity3d.com/ScriptReference/Application.OpenURL.html
		Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/26430");
	}

	#endregion // Button Responder
}
