using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody _rb;
    bool _collision = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }

    void BlockMove()
    {
        if (_collision)
        {
            _rb.velocity = transform.forward * -10.0f;
        }
        else
        {
            _rb.velocity = transform.forward * 10.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Block")
        {
            Destroy(collision.gameObject);
            _collision = false ? false : true; //Åö
            Debug.Log(_collision);
        }
    }
}
