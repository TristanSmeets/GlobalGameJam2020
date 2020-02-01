using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartIndicator : MonoBehaviour
{
    PopupCreator popupCreator;

    private void OnEnable()
    {
        popupCreator = GetComponent<PopupCreator>();
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    private void AddListeners()
    {
        GameStats.OnRoundEnd += OnRoundEnd;
    }
    private void RemoveListeners()
    {
        GameStats.OnRoundEnd -= OnRoundEnd;
    }
    private void OnRoundEnd()
    {
        popupCreator.CreatePopup("RoundStartPopup");
    }
}
