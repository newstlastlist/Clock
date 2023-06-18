using System;
using System.Security.Cryptography;
using UnityEngine;

public class LapTimeHistory : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private LapTime _lapTimePrefab;

    public void LapTime(TimeSpan time)
    {
        LapTime lap = Instantiate(_lapTimePrefab, _content);
        lap.SetTimeText(time);
    }

    public void ClearHistory()
    {
        foreach (Transform lap in _content)
        {
            Destroy(lap.gameObject);
        }
    }
}
