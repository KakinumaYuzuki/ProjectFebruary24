using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �I�u�W�F�N�g�̊p�x��ς��邽�߂̃X�N���v�g
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
    /// �p�x��������
    /// </summary>
    void ControllWheel()
    {
        //Vector3 worldAngle = _transform.eulerAngles;
        float rotateX = 0; float rotateY = 0;
        /*float worldAngleX = worldAngle.x;
        float worldAngleY = worldAngle.y;
        float worldAngleZ = worldAngle.z;*/

        // �}�E�X�z�C�[������l���擾
        float wh = Input.GetAxis("Mouse ScrollWheel");

        // �E�N���b�N�������Ă����
        if (Input.GetButton("Fire1"))
        {
            rotateX += wh * 100f;
            //worldAngle.x = 100f;
            //worldAngle.x += wh * 100f;
        }

        // �E�N���b�N�������Ă����
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
    /// �p�x��������]�ɖ߂�
    /// </summary>
    void RotateReset()
    {
        transform.rotation = _initialRotation;
    }
}
