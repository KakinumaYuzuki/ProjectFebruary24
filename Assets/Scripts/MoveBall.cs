using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    //bool _collision = false;
    //int _count = 0;
    GameManager _gameManager = null;
    Rigidbody _rb;
    Vector3 _dir = default;
    [Tooltip("�{�[���̏���")]
    [SerializeField] float _speed = 50.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_dir = GetComponent<Vector3>();
        /*Vector3 force = new Vector3(0.0f, 0.0f, 10.0f) * _speed; // ������^����@�������\��
        _rb.AddForce(force);*/
        _gameManager = GameManager.Instance;
        _dir = new Vector3(0.0f, 0.0f, 10.0f);  // �ŏ��Ƀ{�[������ԕ����Ƒ���


    }
    void Update()
    {
        // �X�^�[�g�ł���΋L�^���ꂽ�����ɗ͂�������
        if (_gameManager.GetGameState() == GameManager.GameState.Start)
        {
            _rb.AddForce(_dir * _speed);
        }
        // �Q�[�����ł���΃x�N�g����ۑ�����
        if (_gameManager.GetGameState() == GameManager.GameState.Play)
        {
            _dir = _rb.velocity;
        }
        // �|�[�Y���ł���΃{�[�����~�߂�
        if (_gameManager.GetGameState() == GameManager.GameState.Pause)
        {
            _rb.velocity = Vector3.zero;
        }
        // �ĊJ���Ɉ�u�͂�������
        /*if (_gameManager._canBallMove)
        {
            _rb.AddForce(_dir * _speed);    // �i��ł��������ɗ͂�������
            _gameManager._canBallMove = false;
        }
        if (_gameManager._stopBallMove)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            //_dir = _rb.velocity;
            //Debug.Log(_dir);
        }*/

        AddPower();
    }

    void AddPower()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    _rb.AddForce(_rb.velocity * _speed);
        //} // �r���ŗ͂�������e�X�g
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
