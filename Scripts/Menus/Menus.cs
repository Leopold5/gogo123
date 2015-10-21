using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour {

	public GameObject firstChoisePanel;
	public GameObject campainSettingsPanel;
	public GameObject skirmishSettingsPanel;
	public GameObject editorSettingsPanel;
//	public GameObject mainMenuPanel;
//	public GameObject mainMenuPanel;

	void Start (){
		firstChoisePanel.SetActive (true);
		campainSettingsPanel.SetActive (false);
		skirmishSettingsPanel.SetActive (false);
		editorSettingsPanel.SetActive (false);
	}
}
