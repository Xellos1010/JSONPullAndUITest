using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            return _instance;
        }
    }
    public UnityEngine.UI.Text mainTitle;
    public UnityEngine.UI.Text NumberOfCollisions;

    void Awake()
    {
        NumberOfCollisions.enabled = false;
        StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        JSONImport.instance.LoadJSONData();
        yield return new WaitForSeconds(2);
        ObjectManager.instance.InitializeObjectsGenerated();
    }

    public void CheckMainTitleObject()
    {
        if (mainTitle == null)
            mainTitle = GameObject.FindGameObjectWithTag("mainTitle").GetComponent<UnityEngine.UI.Text>();
    }

    public void SetMainTitleObject(string title)
    {
        CheckMainTitleObject();
        mainTitle.text = title;
    }

    void ResetMainTitle()
    {
        mainTitle.text = "No JSON Data";
    }

    public void ResetButton()
    {
        Debug.Log("Reseting Game");
        NumberOfCollisions.text = ("Total number of collisions from last reset = " + CountAllCollisions());
        NumberOfCollisions.enabled = true;
        ResetMainTitle();
        ObjectManager.instance.ResetObjectsGenerated();
        ButtonManager.instance.ResetButtons();
        ReloadJSONData();
    }

    string CountAllCollisions()
    {
        int totalCollisions = 0;
        foreach (ObjectInfo objectInfo in ObjectManager.instance.objectsGenerated)
        {
            try {
                totalCollisions += objectInfo.iNumberCollisions;
            }
            catch
            {
                Debug.Log("Object info not valid. CountAllCollisions() GameManager");
            }
        }
        return totalCollisions.ToString();
    }

    void ReloadJSONData()
    {
        JSONImport.instance.LoadJSONData();
        ButtonManager.instance.SyncButtonTitles();
    }

}