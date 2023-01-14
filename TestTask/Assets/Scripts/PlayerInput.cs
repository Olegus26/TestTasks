using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<float> OnMove;
    public static event Action OnClicked;
    private Vector2 _startPosition = Vector2.zero;
    private float _direction = 0f;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetAxisRaw("Vertical")!=0)
        {
            OnMove?.Invoke(Input.GetAxisRaw("Vertical"));
        } 
#endif
#if UNITY_ANDROID
            GetTouchInput();
#endif
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //case TouchPhase.Began:
                //    OnClicked?.Invoke();
                //    break;
                case TouchPhase.Moved:
                    _direction = touch.position.y > _startPosition.y ? 1f : -1f;
                    break;
                default:
                    _startPosition = touch.position;
                    _direction = 0f;
                    break;
            }
            OnMove?.Invoke(_direction);
        }
    }
}
