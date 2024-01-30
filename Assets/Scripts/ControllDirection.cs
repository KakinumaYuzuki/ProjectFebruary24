using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// オブジェクトの角度を変えるためのスクリプト
/// </summary>
public class ControllDirection : MonoBehaviour
{
    Transform _transform = default;
    Quaternion _initialRotation = default;

    void Start()
    {
        //_transform = GetComponent<Transform>();
        _transform = gameObject.transform;
        
    }

    void Update()
    {
        ControllWheel();

        if (Input.GetButtonDown("Jump"))
        {
            RotateReset();
        }
    }

    /// <summary>
    /// 角度調整する
    /// </summary>
    void ControllWheel()
    {
        //Vector3 worldAngle = _transform.eulerAngles;
        float rotateX = 0; float rotateY = 0;
        /*float worldAngleX = worldAngle.x;
        float worldAngleY = worldAngle.y;
        float worldAngleZ = worldAngle.z;*/

        // マウスホイールから値を取得
        float wh = Input.GetAxis("Mouse ScrollWheel");

        // 右クリックを押している間
        if (Input.GetButton("Fire1"))
        {
            rotateX += wh * 100f;
            //worldAngle.x = 100f;
            //worldAngle.x += wh * 100f;
        }

        // 右クリックを押している間
        if (Input.GetButton("Fire2"))
        {
            rotateY += wh * 100;
            //worldAngle.y += wh * 100f;
        }
        /*if (Input.GetButtonDown("Jump"))
        {
            rotateX += 0 * wh;
            rotateY += 0 * wh;
            //_transform.Rotate(Vector3.zero);
            Debug.Log("a");
        }*/
        _transform.Rotate(rotateX, rotateY, 0);
        //worldAngle.y += wh * 100f;
        //_transform.eulerAngles = worldAngle;
        //_transform(0, wh * 100f, 0); 
    }

    /// <summary>
    /// 角度を初期回転に戻す
    /// </summary>
    void RotateReset()
    {
        transform.rotation = _initialRotation;
    }
}
