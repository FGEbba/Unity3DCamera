using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{


    //Init shit yo
    public Transform target;
    public bool zoomable;

    public bool invertedX;
    public bool invertedY;


    public float yMinLimit = 0f;
    public float yMaxLimit = 90f;

    public float distance = 5f;
    public float distanceMin = 2f;
    public float distanceMax = 10f;

    public float cameraScrollSpeed = 5f;
    public float rotateSpeed = 0.5f;

    public float smoothTime = 2f;

    private float _rotationYAxis = 0.0f;
    private float _rotationXAxis = 0.0f;

    private float velocityX = 0f;
    private float velocityY = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _rotationYAxis = angles.y;
        _rotationXAxis = angles.x;
    }



    void LateUpdate()
    {
        if (target)
        {
            CameraMovement();
        }

        else
        {
            Debug.LogWarning("No \"Player\" object found!");
        }

    }

    void CameraMovement()
    {
        velocityX += Input.GetAxis("Mouse X") * distance * rotateSpeed * (invertedX ? 1 : -1);
        velocityY += Input.GetAxis("Mouse Y") * rotateSpeed * (invertedY ? 1 : -1);

        _rotationXAxis += velocityX;
        _rotationYAxis += velocityY;

        _rotationYAxis = ClampAngle(_rotationYAxis, yMinLimit, yMaxLimit);

        Quaternion toRotation = Quaternion.Euler(_rotationYAxis, _rotationXAxis, 0);
        Quaternion rotation = toRotation;

    

        if (zoomable)
        {
            Zoom();
        }
        Vector3 negDistance = new Vector3(0f, 0f, -distance);

        Vector3 position = rotation * negDistance + target.position;



        transform.rotation = rotation;
        transform.position = position;

        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }

    void Zoom()
    {
        float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelChange != 0)
        {
            distance += scrollWheelChange * cameraScrollSpeed;
        }
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
    }

    
    // Some fix shit
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
