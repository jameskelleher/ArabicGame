using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour {

	public Sprite[] spriteLetters;
	public Sprite[] spriteDiacrits;
	public GameObject letterPrefab;
	public GameObject diacritPrefab;
	public GameObject inputPrefab;
	public TextAsset letterList;
	public TextAsset module;
	public float yInput;
	public float yLetters;

	private Dictionary<string, Sprite> imgDict;
	private Dictionary<string, string> transDict;
	private GameObject letterParent;
	private GameObject inputBoxParent;
	private GUIText arabicGUIText;
	private GUIText transGUIText;

	private string arabicComma = "\u060C";
	private string defaultTransMessage = "Enter an Arabic word to see its translation";

	// Use this for initialization
	void Start () {
		imgDict = new Dictionary<string, Sprite>();
		transDict = CreateTranslationDictionary(module.text);

		string[] letterArray = letterList.text.Split('\n');

		var moduleList = module.text.Split('\n');
		var roots = moduleList[0].Split(',');
		var tiles = moduleList[1].Split(',');

		foreach (var key in transDict.Keys) {
			Debug.Log("key: " + key);
		}

//		if (transDict.ContainsKey(RTLService.RTL.GetText(moduleList[2].Replace(arabicComma, ""))))
//		    Debug.Log("test successful");
//		else
//			Debug.Log("test failed");

		for (int i = 0; i < letterArray.Length; i++) {
			imgDict.Add(letterArray[i], spriteLetters[i]);
		}

		letterParent = new GameObject();
		letterParent.name = "Letters";
		letterParent.transform.position = new Vector3(0f, yLetters);

		inputBoxParent = new GameObject();
		inputBoxParent.name = "Input Boxes";
		inputBoxParent.transform.position = new Vector3(0f, yInput);

		var leftOffset = ((float)tiles.Length) / 2f - 0.5f;
		Debug.Log ("LeftOffset = " + leftOffset);

		for (var i = 0; i < tiles.Length; i++) {
			var x = leftOffset - (1f * i);
			CreateLetter(tiles[i], x);
		}

		for (var i = 0; i < 9; i++) {
			if (i < 3 | i > 5) CreateInputBox(i, 6f - 1.5f * i, yInput);
			else CreateInputBox(i, roots[i-3], 6f - 1.5f * i, yInput);
		}

		arabicGUIText = GameObject.Find("Arabic Text GUI").GetComponent<GUIText>();
		transGUIText = GameObject.Find("Translation Text GUI").GetComponent<GUIText>();

		transGUIText.text = defaultTransMessage;
	}
	
	// Update is called once per frame
	void Update () {
		var key = arabicGUIText.text;
		if (transDict.ContainsKey(key)) {
			transGUIText.text = transDict[key];
		}
		else {
			transGUIText.text = defaultTransMessage;
		}
	}

//
//	public GameObject[] CreateInputRow(int len, int firstRootChar, float y, string[] rootLetters) {
//		GameObject[] row = new GameObject[9];
//		for (int i = 0; i < len; i++) {
//			if (i == firstRootChar) {
//				i += rootLetters.Length - 1;
//				continue;
//			}
//			row[i] = CreateInputBox(i, 6f - 1.5f * i, y);
//		}
//
//		for (int i = firstRootChar; i < firstRootChar + rootLetters.Length; i++) {
//			row[i] = CreateInputBox(i, rootLetters[i-firstRootChar], 6f - 1.5f * i, y);
//		}
//		return row;
//	}
	
	public GameObject CreateLetter(string character, float xDefault) {
		Vector3 defaultPosition = new Vector3(xDefault, 0f, -1f);
		return CreateLetter(character, imgDict[character], defaultPosition);
	}

	public GameObject CreateLetter(string character, Sprite sprite, float xDefault) {
		Vector3 defaultPosition = new Vector3(xDefault, 0f, -1f);
		return CreateLetter(character, sprite, defaultPosition);
	}

	public GameObject CreateLetter(string character, Sprite sprite, Vector3 defaultPosition) {
		GameObject clone = (GameObject) Instantiate(letterPrefab, defaultPosition, Quaternion.identity);
		clone.GetComponent<SpriteRenderer>().sprite = sprite;
		clone.GetComponent<LetterController>().character = character;
		clone.transform.parent = letterParent.transform;
		clone.name = character;
		return clone;
	}

	public GameObject CreateInputBox(int index, float x, float y) {
		return CreateInputBox(index, "", new Vector3(x,y));
	}

	public GameObject CreateInputBox(int index, string rootLetter, float x, float y) {
		return CreateInputBox(index, rootLetter, new Vector3(x,y));
	}

	public GameObject CreateInputBox(int index, string rootLetter, Vector3 pos) {
		GameObject clone = (GameObject) Instantiate(inputPrefab, pos, Quaternion.identity);
		clone.GetComponent<InputBoxController>().Index = index;
		clone.transform.parent = inputBoxParent.transform;
		clone.name = "Input Box " + index;
		if (!rootLetter.Equals("")) {
			clone.GetComponent<SpriteRenderer>().sprite = imgDict[rootLetter];
			clone.GetComponent<InputBoxController>().RootLetter = rootLetter;
		}
		return clone;
	}

	public Dictionary<string, string> CreateTranslationDictionary(string module) {
		var dict = new Dictionary<string, string>();
		var moduleArray = module.Split('\n');
		var key = RTLService.RTL.GetText(moduleArray[2].Replace(arabicComma, ""));
		Debug.Log(key);
		Debug.Log(moduleArray[6]);
		dict.Add(key, moduleArray[6]);
		for (var i = 7; i < moduleArray.Length; i += 6) {
			key = RTLService.RTL.GetText(moduleArray[i].Replace(arabicComma, ""));
			if (!dict.ContainsKey(key)) {
				Debug.Log(key);
				Debug.Log(moduleArray[i+5]);
				dict.Add(key, moduleArray[i+5]);
			}
		}
		return dict;
	}

	public string Reverse(string s) {
		if (s == null) return "";

		var charArray = s.ToCharArray();
		Array.Reverse(charArray);
		return new string(charArray);
	}
}
