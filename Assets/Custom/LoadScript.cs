using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour {

	ScriptConfig script;

	// Use this for initialization
	void Start () {
		string json = "";
		json = Resources.Load ("script").ToString();
		Debug.Log (json);
		script = JsonUtility.FromJson<ScriptConfig>(json);
		Debug.Log (script.lines [0].description [0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
