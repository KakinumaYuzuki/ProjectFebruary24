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
        Vector3 force = new Vector3(0.0f, 0.0f, 10.0f) * _speed; // ������^����@�������\��
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
        } // �r���ŗ͂�������e�X�g
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���ˊp�����߂�(�r���ŗ͂�������Ƃ��p)
        var transform = collision.gameObject.GetComponent<Transform>();
        if (transform != null)
        {
            return;
        }
        var inDirection = _rb.velocity; // ���ˊp
        var inNormal = transform.transform.up; // �u���b�N�̖@���x�N�g��
        var result = Vector3.Reflect(inDirection, inNormal); // ���ˊp

        _rb.velocity = result;
    }
}
