using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VelocityMovePlayer : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _jumpPower = 5;
    Rigidbody _rb = default;
    bool _isGrounded = true;
    GameObject _cameraObject = null;
    Camera _camera = null;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraObject = GameObject.Find("CameraBrain");
        _camera = _cameraObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ControllReflector();
    }

    void ControllReflector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float z = _rb.velocity.z;
        bool upButtonDown = false;
        bool downButtonDown = false;
        //float rotateSpeed = 0.5f;
        Vector3 dir = Vector3.up * vertical + Vector3.right * horizontal;
        //dir = _camera.transform.TransformDirection(dir);
        dir.z = 0;
        if (dir != Vector3.zero)
        {
            transform.forward = dir; //Vector3.Slerp(dir, dir, Time.deltaTime * rotateSpeed); // 方向転換を滑らかにしたい
        }
        dir = dir.normalized * _moveSpeed;
        // スペースでジャンプ 左クリックで上昇
        /*if (Input.GetButtonDown("Fire1"))// && _isGrounded)
        {
            upButtonDown = true;
            Debug.Log(upButtonDown);
            //y = _jumpPower;
        }
        if (Input.GetButtonUp("Fire1"))
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
            y = _jumpPower;
        }
        if (downButtonDown)
        {
            y = _jumpPower;
        }*/

        _rb.velocity = dir * _moveSpeed + Vector3.forward * z;
    }

    void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }
}
