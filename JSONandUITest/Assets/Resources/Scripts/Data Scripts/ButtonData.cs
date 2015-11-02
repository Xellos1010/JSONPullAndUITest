using UnityEngine;
using System;

public class ButtonData : MonoBehaviour
{
    public string title = "";
    public string color = "#000000";
    public string objectType = "";
    public bool obeyGravity = false;
    private UnityEngine.UI.Button _button;
    private UnityEngine.UI.Button button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<UnityEngine.UI.Button>();
            return _button;
        }
    }

    public void ResetData()
    {
        title = "";
        color = "#000000";
        objectType = "";
        obeyGravity = false;
        SyncTitle();
    }

    public void SetData(SimpleJSON.JSONNode data)
    {
        title = data["title"];
        color = data["color"];
        objectType = data["type"];
        obeyGravity = data["obeyGravity"].AsBool;
        SyncTitle();
    }
    
    public void SyncTitle()
    {
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = title;
    }    

}
