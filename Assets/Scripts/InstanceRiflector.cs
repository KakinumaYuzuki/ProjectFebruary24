using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// ���t���N�^�[�𐶐����邽�߂̃X�N���v�g
/// Player�̎q�I�u�W�F�N�g��Mazzle�ɃA�^�b�`����
/// </summary>
public class InstanceRiflector : MonoBehaviour
{
    GameObject _reflectorPrefab = null;
    void Start()
    {
        _reflectorPrefab = (GameObject)Resources.Load("TestReflector");
    }

    void Update()
    {
        InputButton();
    }

    void InputButton()
    {
        bool upButtonDown = false;
        bool downButtonDown = false;

        // �X�y�[�X�ŃW�����v ���N���b�N�ŏ㏸
        if (Input.GetButtonDown("Fire1"))// && _isGrounded)
        {
            //upButtonDown = true;
            GameObject.Instantiate(_reflectorPrefab, this.transform.position, this.transform.localRotation);
            Debug.Log(upButtonDown);
            //y = _jumpPower;
        }
        /*if (Input.GetButtonUp("Fire1"))
        {
            upButtonDown = false;
            Debug.Log(upButtonDown);
        }
        // �E�N���b�N�ŗ������xUp
        if (Input.GetButtonDown("Fire2"))// && _isGrounded)
        {
            downButtonDown = true;
            Debug.Log(downButtonDown);
            //y = -_jumpPower;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            downButtonDown = false;
            Debug.Log(downButtonDown);
        }

        if (upButtonDown)
        {

        }   // ���N���b�N�����Ă��
        if (downButtonDown)
        {

        }   // �E�N���b�N�����Ă��
        */
    }
}
