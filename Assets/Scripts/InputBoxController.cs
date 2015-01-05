using UnityEngine;
using System;
using System.Collections;

public class InputBoxController : MonoBehaviour {

	public int Index;

	[HideInInspector] public bool Occupied { get; set; }
	[HideInInspector] public string ETLetter { get; set; }

	private Sprite defaultSprite;
	private SpriteRenderer spriteRenderer;

	private GameObject _embeddedTile;
	[HideInInspector] public GameObject EmbeddedTile {
		get {
			Debug.Log("Getting ET");
			return this._embeddedTile;
		}
		set {
			this._embeddedTile = value;
			Debug.Log ("Setting ET");
			if (value == null) {
				Occupied = false;
				ETLetter = "";
				spriteRenderer.sprite = defaultSprite;
			}
			else {
				Debug.Log("Setting occupied to true");
				Occupied = true;
				ETLetter = this._embeddedTile.GetComponent<LetterController>().character;
				spriteRenderer.sprite = null;
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
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		defaultSprite = spriteRenderer.sprite;

		textControl = GameObject.Find("Arabic Text GUI").GetComponent<InputTextController>();
		if (!string.IsNullOrEmpty(RootLetter)) {
			IsRoot = true;
			ETLetter = RootLetter;
			Occupied = true;
		}
		else {
			IsRoot = false;
			ETLetter = "";
			Occupied = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		textControl.UpdateGUI(ETLetter, Index);
	}

}
