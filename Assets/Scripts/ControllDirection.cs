using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllDirection : MonoBehaviour
{
    Transform _transform = null;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ControllWheel();
    }

    void ControllWheel()
    {
        Vector3 worldAngle = _transform.eulerAngles;
        /*float worldAngleX = worldAngle.x;
        float worldAngleY = worldAngle.y;
        float worldAngleZ = worldAngle.z;*/

        float wh = Input.GetAxis("Mouse ScrollWheel");
        worldAngle.y += wh * 100f;
        _transform.eulerAngles = worldAngle;
        //_transform(0, wh * 100f, 0); 
    }
}
