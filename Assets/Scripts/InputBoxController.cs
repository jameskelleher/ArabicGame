using UnityEngine;
using System;
using System.Collections;

public class InputBoxController : MonoBehaviour {

	public int Index;

	[HideInInspector] public bool Occupied { get; set; }
	[HideInInspector] public string ETLetter { get; set; }

	private GameObject _embeddedTile;
	[HideInInspector] public GameObject EmbeddedTile {
		get {
			return this._embeddedTile;
		}
		set {
			this._embeddedTile = value;
			if (value == null) {
				Occupied = false;
				ETLetter = "";
			}
			else {
				Occupied = true;
				ETLetter = this._embeddedTile.GetComponent<LetterController>().character;
			}
		}
	}

	[HideInInspector] public bool IsRoot { get; set; }
	[HideInInspector] public string RootLetter { get; set; }


	private InputTextController textControl;

	public void setET(LetterController lc) {
		EmbeddedTile = lc.gameObject;
	}

	// Use this for initialization
	void Start () {
		Occupied = false;
		ETLetter = "";
		textControl = GameObject.Find("Input Text GUI").GetComponent<InputTextController>();
		if (RootLetter != "") {
			IsRoot = true;
			ETLetter = RootLetter;
		}
//		if (Index == 4) { Debug.Log ("IsRoot = " + IsRoot);}
	}
	
	// Update is called once per frame
	void Update () {
		textControl.UpdateGUI(ETLetter, Index);
	}

}
