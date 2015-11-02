using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
