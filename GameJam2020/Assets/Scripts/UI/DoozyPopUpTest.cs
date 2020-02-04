using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

public class DoozyPopUpTest : MonoBehaviour
{
    public string PopUpName;
    public KeyCode SpawnKey = KeyCode.S;
    private UIPopup popup = null;

    private void OnEnable()
    {
        popup = UIPopup.GetPopup(PopUpName);
        popup.Show();
    }

    private void Update()
    {
        if(Input.GetKeyDown(SpawnKey) && popup == null)
        {
            popup = UIPopup.GetPopup(PopUpName);
            //popup.SetTargetCanvasName(gameObject.name);
            popup.Show();
        }
    }
}
