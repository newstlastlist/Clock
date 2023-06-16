using UnityEngine;

public class UserInputPanel : MonoBehaviour
{
    public ChangeTimeDrag secondsDragZone;
    public ChangeTimeDrag minutesDragZone;
    public ChangeTimeDrag hoursDragZone;

    public void SetGoActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public void SetTimeZonesInteractable(bool interactable)
    {
        secondsDragZone.SetInteractable(interactable);
        minutesDragZone.SetInteractable(interactable);
        hoursDragZone.SetInteractable(interactable);
    }
}
