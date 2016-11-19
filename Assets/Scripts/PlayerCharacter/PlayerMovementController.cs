using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float StrafeSpeed = 4;
    public float BackSpeed = 3;

    private PlayerCameraController mShoulderJoint;

	// Use this for initialization
	void Start ()
    {
        mShoulderJoint = GetComponentInChildren<PlayerCameraController>();
        if (mShoulderJoint == null)
        {
            Debug.LogError("Could not locate shoulder joint object. Ensure that a PlayerCharacterController exists in the hierarchy.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float forwardMove = Input.GetAxis("Vertical");
        float sideMove = Input.GetAxis("Horizontal");

        // Check for any movement; early out if none is available
        if (forwardMove == 0 && sideMove == 0)
            return;

        // Zero the character rotation against the shoulder rotation.
        Vector3 shoulderRotation = mShoulderJoint.transform.localEulerAngles;
        shoulderRotation.Scale(Vector3.up);
        transform.localEulerAngles = transform.localEulerAngles + shoulderRotation;
        shoulderRotation = mShoulderJoint.transform.localEulerAngles;
        shoulderRotation.Scale(new Vector3(1f, 0f, 1f));
        mShoulderJoint.transform.localEulerAngles = shoulderRotation;

        // Apply movement
        Vector3 moveVector = (Vector3.forward * Input.GetAxis("Vertical")) + (Vector3.right * Input.GetAxis("Horizontal"));
        transform.Translate(moveVector * ForwardSpeed * Time.deltaTime);
	}
}
