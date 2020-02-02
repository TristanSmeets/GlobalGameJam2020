using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSpawner : MonoBehaviour
{
    PopupCreator popupCreator = null;
    private const string pausePopup = "PausePopup";

    private void OnEnable()
    {
        popupCreator = GetComponent<PopupCreator>();
    }
   
    // Update is called once per frame
    void Update()
    {
       if(Input.GetAxis("Pause") > 0)
        {
            popupCreator.CreatePopup(pausePopup);
        }
    }
}
