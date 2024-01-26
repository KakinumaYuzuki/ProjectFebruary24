using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// リフレクターを生成するためのスクリプト
/// Playerの子オブジェクトのMazzleにアタッチする
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

        // スペースでジャンプ 左クリックで上昇
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
        // 右クリックで落下速度Up
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

        }   // 左クリック押してる間
        if (downButtonDown)
        {

        }   // 右クリック押してる間
        */
    }
}
