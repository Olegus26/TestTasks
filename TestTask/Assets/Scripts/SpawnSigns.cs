using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnSigns : MonoBehaviour
{
    public static event Action OnSpawn;
    [SerializeField] private GameObject _signPrefab;
    [SerializeField] private Transform _spawnPoint;
    private float _time = 1;
    private float _speedM = 5f;
    private float _speedR = 160f;

    private void OnEnable()
    {
        PlayerInput.OnMove += Spawn;
        PlayerInput.OnClicked += ResetPosition;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= Spawn;
        PlayerInput.OnClicked -= ResetPosition;
    }

    private void Spawn(float direction)
    {
        if (GameManager.Instance.Signs > 0)
        {
            
            _time += Time.deltaTime;
            if (_time >= 0.1)
            {
                float rotationX = _spawnPoint.rotation.eulerAngles.x + direction * _speedR * Time.deltaTime;
                rotationX = Mathf.Clamp(rotationX, 240f, 300f);
                _spawnPoint.rotation = Quaternion.Euler(rotationX, 180, 0);

                float positionY = _spawnPoint.position.y + direction * _speedM * Time.deltaTime;
                positionY = Mathf.Clamp(positionY, transform.position.y - 1f, transform.position.y + 0.2f);
                _spawnPoint.position = new Vector3(_spawnPoint.position.x, positionY, _spawnPoint.position.z);

                var sign = Instantiate(_signPrefab, _spawnPoint.position, _spawnPoint.rotation);
                sign.transform.rotation = Quaternion.Euler(sign.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, sign.transform.rotation.eulerAngles.z);
                OnSpawn?.Invoke();
                Destroy(sign, 0.5f);
                _time = 0;
            }
        }
        
    }

    public void ResetPosition()
    {
        _spawnPoint.rotation = Quaternion.Euler(-90, 180, 0);
        _spawnPoint.position = new Vector3(transform.position.x, transform.position.y-0.25f, transform.position.z+0.75f);
    }
}
