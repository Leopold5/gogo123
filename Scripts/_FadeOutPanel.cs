using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour {


	public Image myPanel;
	float fadeTime = 3f;
	Color colorToFadeTo;
		
		
	void Start() 
	{
		colorToFadeTo = new Color (1f, 1f, 1f, 0f);
		myPanel.CrossFadeColor (colorToFadeTo, fadeTime, true, true);
	}
		
}
