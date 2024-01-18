using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Rigidbody _rb;
    //bool _collision = false;
    float _speed = 50.0f;
    int _count = 0;
    //Vector3 _dir = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_dir = GetComponent<Vector3>();
        Vector3 force = new Vector3(0.0f, 0.0f, 10.0f) * _speed; // 初速を与える　★調整予定
        _rb.AddForce(force);
    }
    void Update()
    {
        AddPower();
    }

    void AddPower()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(_rb.velocity * _speed);
        } // 途中で力を加えるテスト
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 反射角を求める(途中で力を加えるとき用)
        var transform = collision.gameObject.GetComponent<Transform>();
        if (transform != null)
        {
            return;
        }
        var inDirection = _rb.velocity; // 入射角
        var inNormal = transform.transform.up; // ブロックの法線ベクトル
        var result = Vector3.Reflect(inDirection, inNormal); // 反射角

        _rb.velocity = result;
    }
}
