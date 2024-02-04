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
    [Tooltip("ボールの初速")]
    [SerializeField] float _speed = 50.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_dir = GetComponent<Vector3>();
        /*Vector3 force = new Vector3(0.0f, 0.0f, 10.0f) * _speed; // 初速を与える　★調整予定
        _rb.AddForce(force);*/
        _gameManager = GameManager.Instance;
        _dir = new Vector3(0.0f, 0.0f, 10.0f);  // 最初にボールが飛ぶ方向と速さ


    }
    void Update()
    {
        // スタートであれば記録された向きに力を加える
        if (_gameManager.GetGameState() == GameManager.GameState.Start)
        {
            _rb.AddForce(_dir * _speed);
        }
        // ゲーム中であればベクトルを保存する
        if (_gameManager.GetGameState() == GameManager.GameState.Play)
        {
            _dir = _rb.velocity;
        }
        // ポーズ中であればボールを止める
        if (_gameManager.GetGameState() == GameManager.GameState.Pause)
        {
            _rb.velocity = Vector3.zero;
        }
        // 再開時に一瞬力を加える
        /*if (_gameManager._canBallMove)
        {
            _rb.AddForce(_dir * _speed);    // 進んでいた方向に力を加える
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
        //} // 途中で力を加えるテスト
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
