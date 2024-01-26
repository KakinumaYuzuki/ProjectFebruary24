using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リフレクターを動かすためのスクリプト
/// リフレクターのプレハブにアタッチする
/// </summary>
public class MoveReflector : MonoBehaviour
{
    [Tooltip("リフレクターが飛んでいくスピード")]
    [SerializeField] float _moveSpeed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 dir = transform.TransformDirection(transform.position);
        this.transform.Translate(new Vector3(0, 0, 1) * _moveSpeed * Time.deltaTime) ;
    }
}
