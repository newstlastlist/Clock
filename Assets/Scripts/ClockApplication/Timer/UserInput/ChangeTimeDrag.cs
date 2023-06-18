using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeTimeDrag : AbstractModelMonoBeh
{
    [SerializeField] private ReactiveProperty<int> _digit = new ReactiveProperty<int>();
    [SerializeField] private int _maxDigitValue;
    [SerializeField] private int _minDigitValue = 0;
    [SerializeField] [Range(50, 5)] private int pixelSensitivity = 5;
    [SerializeField] private Image _image;

    private Vector3 _dragStartPosition;
    public ReactiveProperty<int> Digit => _digit;

    private void Start()
    {
        _digit.AddTo(_disposables);
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        var pointerEventData = eventData as PointerEventData;
        _dragStartPosition = pointerEventData.position;
    }

    public void OnDrag(BaseEventData eventData)
    {
        var pointerEventData = eventData as PointerEventData;
        var dragDistance = pointerEventData.position.y - _dragStartPosition.y;

        int steps = (int) (dragDistance / (pixelSensitivity));

        if (steps != 0)
        {
            _digit.Value += steps;

            if (_digit.Value > _maxDigitValue)
            {
                _digit.Value = _minDigitValue;
            }
            else if (_digit.Value < _minDigitValue)
            {
                _digit.Value = _maxDigitValue;
            }

            _dragStartPosition = pointerEventData.position;
        }
    }

    public IObservable<int> GetDigitAsObservable()
    {
        return _digit;
    }

    public void SetInteractable(bool interactable)
    {
        _image.enabled = interactable;
    }

    public void ResetDigit()
    {
        _digit.Value = 0;
    }
}