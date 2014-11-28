using UnityEngine;
using System.Collections;

public class DemoScript : MonoBehaviour
{

    //Unity Inspector variables
    public string InputText = "Your Right to left text here...";
    public RTLService.RTL.NumberFormat NumberFormat = RTLService.RTL.NumberFormat.KeepOriginalFormat;
    public bool IsLtrText = false;
    public int WordWrapBias = 0;
    public GUISkin Skin;
    public Texture ArrowSign;


    //private variables
    string _inputText = "";
    RTLService.RTL.NumberFormat _numberFormat;
    bool _isLtrText = false;
    int _wordwarpBias = 0;
    bool _needRefreshText = true;
    string _convertedText = "";
    string strNeededCode = "";
    // Update is called once per frame
    void Update()
    {
        _needRefreshText = false;
        //Check if text parameters are changes
        if (_inputText != InputText)
        {
            _inputText = InputText;
            _needRefreshText = true;
        }
        if (_numberFormat != NumberFormat)
        {
            _numberFormat = NumberFormat;
            _needRefreshText = true;
        }
        if (_isLtrText != IsLtrText)
        {
            _isLtrText = IsLtrText;
            _needRefreshText = true;
        }
        if (_wordwarpBias != WordWrapBias)
        {
            _wordwarpBias = WordWrapBias;
            _needRefreshText = true;
        }
        if (_needRefreshText)
        {
            strNeededCode = string.Format("RTLService.RTL.GetText(InputText, RTL.{0}, {1}, {2});",
                _numberFormat, _isLtrText.ToString().ToLower(), _wordwarpBias);
            _convertedText = RTLService.RTL.GetText(InputText, _numberFormat, _isLtrText, _wordwarpBias);
        }
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        GUI.color = Color.white;
        ///////////////////////////////////////////////////////////////////////////////////////
        GUI.Label(new Rect(6, 4, 600, 20), "RTL Plugin 4.0  : Right to left text format (Arabic, Hebrew, Persian, Afghan, Urdu, Kurd)", Skin.customStyles[2]);
        ///////////////////////////////////////////////////////////////////////////////////////
        InputText = GUI.TextArea(new Rect(6, 25, 460, 150), _inputText);
        GUI.DrawTexture(new Rect(500, 42, 200, 120), ArrowSign, ScaleMode.StretchToFill);
        GUI.Label(new Rect(565, 90, 200, 120), "Unity's default", Skin.customStyles[0]);
        ///////////////////////////////////////////////////////////////////////////////////////
        if (GUI.Button(new Rect(6, 190, 100, 25), "ArabicFormat"))
        {
            NumberFormat = RTLService.RTL.NumberFormat.ArabicFormat;
        }
        if (GUI.Button(new Rect(104, 190, 100, 25), "EnglishFormat"))
        {
            NumberFormat = RTLService.RTL.NumberFormat.EnglishFormat;
        }
        if (GUI.Button(new Rect(206, 190, 130, 25), "KeepOriginalFormat"))
        {
            NumberFormat = RTLService.RTL.NumberFormat.KeepOriginalFormat;
        }
        GUI.Label(new Rect(6, 220, 120, 25), "Number Format :");
        switch (NumberFormat)
        {
            case RTLService.RTL.NumberFormat.ArabicFormat:
                GUI.Label(new Rect(112, 220, 130, 25), "ArabicFormat");
                break;
            case RTLService.RTL.NumberFormat.EnglishFormat:
                GUI.Label(new Rect(112, 220, 130, 25), "EnglishFormat");
                break;
            case RTLService.RTL.NumberFormat.KeepOriginalFormat:
                GUI.Label(new Rect(112, 220, 130, 25), "KeepOriginalFormat");
                break;
        }
        GUI.Label(new Rect(6, 240, 120, 25), "Is left to right text :");
        IsLtrText = GUI.Toggle(new Rect(125, 240, 25, 25), _isLtrText, "");
        GUI.Label(new Rect(150, 240, 100, 25), "WordWarp Bias :");
        string newbias = GUI.TextField(new Rect(250, 240, 40, 22), _wordwarpBias.ToString());
        int.TryParse(newbias, out WordWrapBias);
        //
        GUI.Label(new Rect(6, 298, 352, 25), "RTL source code you would use in Update method:");
        GUI.color = Color.green;
        GUI.Label(new Rect(6, 320, 570, 35), strNeededCode);
        //
        GUI.DrawTexture(new Rect(500, 215, 200, 120), ArrowSign, ScaleMode.StretchToFill);
        GUI.Label(new Rect(570, 265, 190, 120), "RTL Options", Skin.customStyles[0]);
        ///////////////////////////////////////////////////////////////////////////////////////
        GUI.color = Color.yellow;
        GUI.Box(new Rect(6, 362, 464, 154), "");
        GUI.Box(new Rect(6, 362, 464, 154), "");
        GUI.Label(new Rect(9, 365, 460, 150), _convertedText, Skin.customStyles[1]);
        GUI.DrawTexture(new Rect(500, 385, 200, 120), ArrowSign, ScaleMode.StretchToFill);
        GUI.Label(new Rect(574, 435, 190, 120), "RTL Result", Skin.customStyles[0]);
        ///////////////////////////////////////////////////////////////////////////////////////
    }
}
