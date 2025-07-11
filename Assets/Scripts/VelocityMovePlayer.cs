using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤー(パドル)の移動用
/// プレイヤー(パドル)の親オブジェクトにアタッチ
/// Rigidbodyの設定が必要
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
    //Camera _camera = null;
    //[SerializeField]
    //GameObject _parentGameObject = null;
    // Start is called before the first frame update
    GameManager _gameManager = null;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _cameraObject = GameObject.Find("CameraBrain");
        //_camera = _cameraObject.GetComponent<Camera>();
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._canControll)
        {
            ControllReflector();          
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    void ControllReflector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //float rotateSpeed = 0.5f;
        // 失敗
        //Vector3 world = _parentGameObject.transform.InverseTransformPoint(transform.position);
        
        //Vector3 dir = Vector3.up * vertical + Vector3.right * horizontal; // 元

        Vector3 dir = new Vector3(horizontal * _moveSpeed, vertical * _moveSpeed, 0);
        //dir = transform.TransformDirection(dir);
        //dir = _camera.transform.TransformDirection(dir);

        /*dir.z = 0;
        if (dir != Vector3.zero)
        {
            transform.forward = dir; //Vector3.Slerp(dir, dir, Time.deltaTime * rotateSpeed); // 方向転換を滑らかにしたい
        }
        dir = dir.normalized * _moveSpeed;*///元

        //　失敗
        //_rb.velocity = new Vector3(world.x * horizontal * _moveSpeed, world.y * vertical * _moveSpeed, 0);
        //_rb.AddForce(transform.position.x * horizontal, transform.position.y * vertical, 0);

        //_rb.velocity = dir * _moveSpeed + Vector3.forward * z; //移動元
        _rb.velocity = dir;


        //Vector3 worldAngle = _transform.eulerAngles;
        //_transform.eulerAngles = worldAngle;

        //transform.localRotation = Quaternion.identity;  // オブジェクトの向きを固定元
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
