using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class ObjectInfo : MonoBehaviour {

    public int iNumberCollisions = 0;
    GameObject objectType;
    UnityEngine.UI.Text _collisionText;
    UnityEngine.UI.Text collisionText
    {
        get
        {
            if (_collisionText == null)
                foreach (Transform t in transform)
                    if (t.GetComponent<UnityEngine.UI.Text>())
                        _collisionText = t.GetComponent<UnityEngine.UI.Text>();
            return _collisionText;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "ObjectManager")
        {
            iNumberCollisions+=1;
            try {
                SyncTextToCollisions();
            }
            catch
            {
                Debug.Log("No Valid Text Object");
            }
        }
    }

    public void SyncTextToCollisions()
    {
        collisionText.text = iNumberCollisions.ToString();
    }

    public void ChangeObjectInfo(ButtonData objectInfo)
    {
        if (!CheckObjectType(objectInfo.objectType))
            ClearModelInfo();
            switch (objectInfo.objectType)
            {
                case "Sphere":
                    AddSphereModelInfo();
                    break;
                case "Cube":
                AddCubeModelInfo();
                    break;
            }
        SetDefaultData();
        ChangeObjectColor(objectInfo.color);
        ChangeGravity(objectInfo.obeyGravity);
	}

    void SetDefaultData()
    {
        objectType.transform.position = transform.position;
        objectType.transform.parent = transform;
        objectType.transform.localScale = new Vector3(30, 30, 30);
    }

    void AddSphereModelInfo()
    {
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);        
        objectType = newGameObject;
    }

    void AddCubeModelInfo()
    {
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newGameObject.transform.parent = transform;
        newGameObject.transform.position = transform.position;
        newGameObject.transform.localScale = new Vector3(30, 30, 30);
        objectType = newGameObject;
    }

    void ChangeObjectColor(string color)
    {
        objectType.GetComponent<MeshRenderer>().material.color = HexToColor(color);
    }

    void ChangeGravity(bool useGravity)
    {
        GetComponent<Rigidbody>().useGravity = useGravity;
    }

    void ClearModelInfo()
    {
        if (objectType != null)
            Destroy(objectType);
    }

    bool CheckObjectType(string objectType)
    {
        if ((GetComponent<SphereCollider>() && objectType == "Sphere") || GetComponent<BoxCollider>() && objectType == "Cube")
            return true;
        else
            return false;
    }
    Color HexToColor(string hex)
    {
        try
        {
            if (hex.Contains("#"))
                hex = hex.Remove(0, 1);
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
        catch
        {
            Debug.Log("No Valid Hex input");
            return new Color32(0, 0, 0, 255);
        }
    }
}
