using UnityEngine;
using System.Collections;

public class PlayerCameraController : MonoBehaviour
{
    public float MouseSensitivity = 50f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float lookHorizontal = Input.GetAxis("Mouse X");
        float lookVertical = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, lookHorizontal * Time.deltaTime * MouseSensitivity);
        transform.Rotate(Vector3.right, lookVertical * Time.deltaTime * MouseSensitivity * -1f);

        Vector3 shoulderRotation = transform.localEulerAngles;
        shoulderRotation.Scale(new Vector3(1f, 1f, 0f));
        transform.localEulerAngles = shoulderRotation;
	}
}
