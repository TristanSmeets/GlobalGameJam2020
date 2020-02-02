using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawn : MonoBehaviour
{
    PopupCreator popupCreator = null;

    // Start is called before the first frame update
    void Awake()
    {
        popupCreator = GetComponent<PopupCreator>();
        popupCreator.CreatePopup("MainMenu");
    }
}
