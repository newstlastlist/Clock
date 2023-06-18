using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClockApplication : MonoBehaviour
{
    [Inject] private ClockModel _clock;
    [SerializeField] private GameObject _currentEntity;
    [SerializeField] private List<ButtonEntityContainer> _containers;
    private void Start()
    {
        InitBtns();
        
        HideAllTimeEntities();

        ShowTimeEntity(TimeEntities.Clock);
            
        _clock.Start();
    }
    private void OnDestroy()
    {
        _clock.Dispose();
    }

    private void InitBtns()
    {
        foreach (TimeEntities timeEntity in (TimeEntities[]) Enum.GetValues(typeof(TimeEntities)))
        {
            foreach (var container in _containers)
            {
                if (container.timeEntity == timeEntity)
                {
                    container.btn.onClick.AddListener(() => ShowTimeEntity(timeEntity));
                    break;
                }
            }
        }
    }
    public void ShowTimeEntity(TimeEntities timeEntity)
    {
        if(_currentEntity != null)
            _currentEntity.SetActive(false);
        
        ButtonEntityContainer buttonEntityContainer = _containers.Where(c => c.timeEntity == timeEntity).First();
        buttonEntityContainer.entity.SetActive(true);
        buttonEntityContainer.btn.interactable = false;

        //enable all other btns except pressed btn
        foreach (var container in _containers)
        {
            if (container.timeEntity != timeEntity)
            {
                container.btn.interactable = true;
            }
        }

        _currentEntity = buttonEntityContainer.entity;
    }

    public void HideAllTimeEntities()
    {
        foreach (var container in _containers)
        {
            container.entity.SetActive(false);
            container.btn.interactable = true;
        }
    }
    public enum TimeEntities
    {
        Clock,
        Timer,
        StopWatch
    }
}

[System.Serializable]
public class ButtonEntityContainer
{
    public Button btn;
    public GameObject entity;
    public ClockApplication.TimeEntities timeEntity;
}
