using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public static ButtonManager _instance;
    public static ButtonManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("buttonGroup").GetComponent<ButtonManager>();
            return _instance;
        }
    }

    public void SyncButtonTitles()
    {
        foreach(Transform tButton in transform)
        {
            tButton.GetComponent<ButtonData>().SyncTitle();
        }
    }

    public void ResetButtons()
    {
        foreach (Transform tButton in transform)
        {
            tButton.GetComponent<ButtonData>().ResetData();
        }
    }

}
