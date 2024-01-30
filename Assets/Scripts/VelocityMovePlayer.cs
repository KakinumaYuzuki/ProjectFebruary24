using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �v���C���[(�p�h��)�̈ړ��p
/// �v���C���[(�p�h��)�̐e�I�u�W�F�N�g�ɃA�^�b�`
/// Rigidbody�̐ݒ肪�K�v
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class VelocityMovePlayer : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _jumpPower = 5;
    Rigidbody _rb = default;
    Transform _transform = default;
    bool _isGrounded = true;
    GameObject _cameraObject = null;
    Camera _camera = null;
    //[SerializeField]
    //GameObject _parentGameObject = null;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
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
        // ���s
        //Vector3 world = _parentGameObject.transform.InverseTransformPoint(transform.position);
        Vector3 dir = Vector3.up * vertical + Vector3.right * horizontal;
        //dir = _camera.transform.TransformDirection(dir);
        dir.z = 0;
        if (dir != Vector3.zero)
        {
            transform.forward = dir; //Vector3.Slerp(dir, dir, Time.deltaTime * rotateSpeed); // �����]�������炩�ɂ�����
        }
        dir = dir.normalized * _moveSpeed;

        //�@���s
        //_rb.velocity = new Vector3(world.x * horizontal * _moveSpeed, world.y * vertical * _moveSpeed, 0);
        //_rb.AddForce(transform.position.x * horizontal, transform.position.y * vertical, 0);

        _rb.velocity = dir * _moveSpeed + Vector3.forward * z; //�ړ�
        
        //Vector3 worldAngle = _transform.eulerAngles;
        //_transform.eulerAngles = worldAngle;
        transform.localRotation = Quaternion.identity;  // �I�u�W�F�N�g�̌������Œ�
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
