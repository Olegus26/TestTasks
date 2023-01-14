using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMove : MonoBehaviour
{
    public static event Action<string> OnDestroy;
    private Rigidbody _rigidbody;
    private float _speed = 10f;
    private float _speedRotation = 70f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //float positionZ = transform.position.z + _speed * Time.deltaTime;
        ////_rigidbody.MovePosition(new Vector3(_rigidbody.position.x, _rigidbody.position.y, positionZ));
        transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);
    }

    //private void FixedUpdate()
    //{
    //    float positionZ = _rigidbody.position.z + _speed * Time.fixedDeltaTime;
    //    //_rigidbody.MovePosition(new Vector3(_rigidbody.position.x, _rigidbody.position.y, positionZ));
    //    transform.Translate(_rigidbody.position.x, _rigidbody.position.y, positionZ, Space.Self);
    //}

    private void OnEnable()
    {
        CheckPoint.OnTurn += PassCheckPoint;
    }

    private void OnDisable()
    {
        CheckPoint.OnTurn -= PassCheckPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Let"))
        {
            OnDestroy?.Invoke("you lost");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Let"))
        {
            OnDestroy?.Invoke("you lost");
        }
    }

    private void PassCheckPoint(string direct)
    {
        float presentValueRotationY = transform.rotation.y;
        if (direct== "Left")
        {
            StartCoroutine(Turn(-1));

        }
        else if (direct == "Right")
        {
            StartCoroutine(Turn(1));
        }
        else
        {
            OnDestroy?.Invoke("you win ;)");
        }


    }

    private IEnumerator Turn(int direct)
    {
        float presentValueRotationY = transform.rotation.eulerAngles.y;
        if (direct == 1)
        {
            float t = presentValueRotationY + direct * 90;
            if (t < 0)
            {
                t = 360 - t;
            }
            while (transform.transform.rotation.eulerAngles.y != t)
            {
                transform.Rotate(0, direct * _speedRotation * Time.deltaTime, 0);
                yield return new WaitForSeconds(0.01f);
                if (transform.transform.rotation.eulerAngles.y  >= t)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, presentValueRotationY + direct * 90, transform.rotation.z);
                }
            }   
        }
        else
        {
            float t = presentValueRotationY + direct * 90;
            if (t < 0)
            {
                t = 360 + t;
            }
            while (transform.transform.rotation.eulerAngles.y != t)
            {
                transform.Rotate(0, direct * _speedRotation * Time.deltaTime, 0);
                yield return new WaitForSeconds(0.01f);
                if (transform.transform.rotation.eulerAngles.y <= t)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, presentValueRotationY + direct * 90, transform.rotation.z);
                }
            }
        }


    }

}
