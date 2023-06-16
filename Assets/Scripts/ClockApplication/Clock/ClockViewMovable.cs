using UnityEngine;

public class ClockViewMovable : AbstractClocklView
{
    [SerializeField] private RectTransform _clockRect;
    [SerializeField] private RectTransform _bottomPos;
    [SerializeField] private RectTransform _defaultPos;

    public void MoveToDefaultPos()
    {
        MoveToPos(_defaultPos);
    }
    public void MoveToBottomPos()
    {
        MoveToPos(_bottomPos);
    }

    private void MoveToPos(RectTransform target)
    {
        _clockRect.anchorMax = target.anchorMax;
        _clockRect.anchorMin = target.anchorMin;
        _clockRect.localPosition = target.localPosition;
        _clockRect.localScale = target.localScale;
    }
    
}
