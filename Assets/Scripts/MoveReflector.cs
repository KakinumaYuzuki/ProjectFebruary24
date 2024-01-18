using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReflector : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _jumpPower = 5;
    Rigidbody _rb = default;
    bool _isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControllReflector();
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * vertical + Vector3.right * horizontal;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
        dir = dir.normalized * _moveSpeed;

        float y = _rb.velocity.y;
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            y = _jumpPower;
        }

        _rb.velocity = dir * _moveSpeed + Vector3.up * y;*/
    }

    void ControllReflector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float y = _rb.velocity.y;
        //float rotateSpeed = 0.5f;
        Vector3 dir = Vector3.forward * vertical + Vector3.right * horizontal;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir !=Vector3.zero)
        {
            transform.forward = dir; //Vector3.Slerp(dir, dir, Time.deltaTime * rotateSpeed); // 方向転換を滑らかにしたい
        }
        dir = dir.normalized * _moveSpeed;
        // スペースでジャンプ
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            y = _jumpPower;
        }
        // 右クリックで落下速度Up
        if (Input.GetButtonDown("Fire2") && _isGrounded)
        {
            y = -_jumpPower;
        }

        _rb.velocity = dir * _moveSpeed + Vector3.up * y;
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
