using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public float rotationSpeed;
    public float xDegrees, yDegrees;

    public Transform cannonBody;

    // Update is called once per frame
    void Update()
    {
        CannonMovement();
    }

    void CannonMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        xDegrees += horizontalInput * rotationSpeed * Time.deltaTime;
        yDegrees += verticalInput * rotationSpeed * Time.deltaTime;

        xDegrees = Mathf.Clamp(xDegrees, -43, 43);
        yDegrees = Mathf.Clamp(yDegrees, -7, 43);

        cannonBody.localEulerAngles = new Vector3(0, yDegrees, xDegrees);
    }
}
