using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JSONImport : MonoBehaviour
{
    public string url = "http://atm.s3.amazonaws.com/Unity/UnityTest.json";
    public GameObject LoadingScreen;

    private static JSONImport _instance;
    public static JSONImport instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("JSONImport").GetComponent<JSONImport>();
            return _instance;
        }
    }
    
    public void LoadJSONData()
    {
        Debug.Log("Loading JSON Data");        
        StartCoroutine(LoadJSON());
    }    

    IEnumerator LoadJSON()
    {
        LoadingScreen.SetActive(true);
        WWW jsonURL = new WWW("http://atm.s3.amazonaws.com/Unity/UnityTest.json");
        yield return jsonURL;
        if (jsonURL.error != null)
            Debug.Log("json could not be loaded. url = " + url);
        else
        {
            JSONNode jsonData = JSON.Parse(jsonURL.text);
            Debug.Log(jsonData);
            ApplyData(jsonData);
        }
        yield return new WaitForSeconds(2);
        LoadingScreen.SetActive(false);
    }

    void ApplyData(JSONNode masterJSON)
    {
        Debug.Log("Appling Data from JSON");
        SetTitle(masterJSON[0]);
        SetMaxObjects(masterJSON[1]);
        SetButtonData(masterJSON[2]);        
    }

    void SetTitle(JSONNode titleNode)
    {
        GameManager.instance.SetMainTitleObject(titleNode.Value);
    }

    void SetMaxObjects(JSONNode maxObjectData)
    {
        StaticsManager.maxObjects =  maxObjectData.AsInt;
    }

    void SetButtonData(JSONNode buttonDataArray)
    {
        for(int iNode = 0; iNode < buttonDataArray.Count; iNode++)
        {
            //Debug.Log(buttonDataArray[iNode]);
            if (iNode < ButtonManager.instance.transform.childCount)
            {
                ButtonManager.instance.transform.GetChild(iNode).GetComponent<ButtonData>().SetData(buttonDataArray[iNode]);
            } 
        }
    }
}
