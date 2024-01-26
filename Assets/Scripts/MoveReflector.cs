using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���t���N�^�[�𓮂������߂̃X�N���v�g
/// ���t���N�^�[�̃v���n�u�ɃA�^�b�`����
/// </summary>
public class MoveReflector : MonoBehaviour
{
    [Tooltip("���t���N�^�[�����ł����X�s�[�h")]
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
