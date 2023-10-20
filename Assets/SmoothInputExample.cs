using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SmoothInputExample : MonoBehaviour
{
    [SerializeField]
    private InputAction _move;

    [SerializeField]
    private TextMeshProUGUI _text;

    public float speed = 5.0f;
    public float smoothTime = 0.1f;

    private bool _isInput;

    private Vector2 _smoothInput;
    private Vector2 _velocity;

    void Awake()
    {
        _isInput = false;
        _smoothInput = Vector2.zero;
        _velocity = Vector2.zero;

        _move.started += OnStarted;
        _move.performed += OnPerformed;
        _move.canceled += OnCanceled;
    }

    private void OnStarted(InputAction.CallbackContext context)
    {
        _isInput = true;
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        // nothing...
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        _isInput = false;
    }

    private void Update()
    {
        // 新Input Systemからの入力を取得
        Vector2 currentInput = Vector3.zero;
        if (_isInput) {
            currentInput = _move.ReadValue<Vector2>();
        }
        // スムーズに入力を変換
        _smoothInput = Vector2.SmoothDamp(_smoothInput, currentInput, ref _velocity, smoothTime);

        //Debug.Log($"smoothInput={_smoothInput}");
        _text.text = $"{_smoothInput}";
    }

    private void OnEnable() => _move.Enable();
    private void OnDisable() => _move.Disable();
    private void OnDestroy() => _move.Dispose();
}
