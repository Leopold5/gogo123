using UnityEngine;
using System.Collections;

public class NewMapPanelControlls : MonoBehaviour {

	public GameObject newMapPanelPrefab;
	public GameObject newMapPanel;
	private GameObject newMapPanelInstance;

	void OnEnable (){
		newMapPanelInstance = Instantiate (newMapPanelPrefab) as GameObject;
		newMapPanelInstance.transform.SetParent (newMapPanel.transform, false);
	}

	void OnDisable () {
		Destroy (newMapPanelInstance);
	}
}
