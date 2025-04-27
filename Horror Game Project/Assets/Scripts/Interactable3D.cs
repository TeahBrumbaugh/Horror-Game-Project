using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Interactable3D : MonoBehaviour
{
    public GameObject uiPanel;
    public TMP_InputField inputField;

    public MonoBehaviour[] disableOnOpen;
    public PlayerLook mouselook;

    public void Interact()
    {
        foreach (var mb in disableOnOpen)
        {
            mb.enabled = false;
        }
            
            
        uiPanel.SetActive(true);

        inputField.text = "";
        inputField.ActivateInputField();

        mouselook.UnlockCursor();
    }

    public void OnSubmit()
    {
        string entered = inputField.text;
        Debug.Log($"User entered: {entered}");
        uiPanel.SetActive(false);

        foreach(var mb in disableOnOpen)
        {
            mb.enabled = true;
        }

        mouselook.LockCursor();
    }   

    public void CloseUI()
    {
        uiPanel.SetActive(false);

        foreach(var mb in disableOnOpen)
        {
            mb.enabled = true;
        }

        mouselook.LockCursor();
    }
}
