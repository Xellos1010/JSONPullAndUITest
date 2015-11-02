using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

    public ObjectInfo[] objectsGenerated;
    public Transform objectTemplate;
    public int currentIndex = 0;
    public Transform spawnPoint;
    public Transform ObjectOrganizer;
    private static ObjectManager _instance;
    public static ObjectManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
            return _instance;
        }
    }

    public void InitializeObjectsGenerated()
    {
        objectsGenerated = new ObjectInfo[StaticsManager.maxObjects];
    }

    public void GenerateObject(ButtonData objectData)
    {
        Debug.Log("Generating Object");
        if (objectsGenerated[currentIndex] == null)
        {
            objectsGenerated[currentIndex] = GenerateGameObject(objectData);
            objectsGenerated[currentIndex].SyncTextToCollisions();
        }
        else
            objectsGenerated[currentIndex].ChangeObjectInfo(objectData);
        IncrementIndex();
    }

    ObjectInfo GenerateGameObject(ButtonData data)
    {
        Transform generatedObject = (Transform)Instantiate(objectTemplate, transform.position, transform.rotation) as Transform;
        Debug.Log(generatedObject.name);
        Rigidbody body = generatedObject.gameObject.AddComponent<Rigidbody>();
        body.mass = 1000;
        ObjectInfo generatedObjectInfo = generatedObject.GetComponent<ObjectInfo>();
        generatedObjectInfo.ChangeObjectInfo(data);        
        if (spawnPoint != null)
            spawnPoint = transform.GetChild(0);
        generatedObject.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        generatedObject.transform.parent = ObjectOrganizer;
        body.AddForce(GenerateForce());
        return generatedObjectInfo;
    }

    void IncrementIndex()
    {
        currentIndex += 1;
        Debug.Log(StaticsManager.maxObjects);
        if (currentIndex >= StaticsManager.maxObjects)
            currentIndex = 0;
    }

    Vector3 GenerateForce()
    {
        return new Vector3(UnityEngine.Random.Range(50, 100)*5000, UnityEngine.Random.Range(50, 100) * 5000, UnityEngine.Random.Range(50, 100) * 5000);
    }

    public void ResetObjectsGenerated()
    {
        foreach (ObjectInfo objectGenerated in objectsGenerated)
        {
            if (objectGenerated)
            {
                objectGenerated.iNumberCollisions = 0;
                Destroy(objectGenerated.gameObject);
            }
        }
    }

}
