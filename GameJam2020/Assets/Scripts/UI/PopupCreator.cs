using Doozy.Engine.UI;
using UnityEngine;

public class PopupCreator : MonoBehaviour
{
    private UIPopup popup = null;

    public void CreatePopup(string name) //PlayerCreation
    {
        if(popup != null)
        {
            return;
        }

        popup = UIPopup.GetPopup(name);
        popup.Show();
    }
}
