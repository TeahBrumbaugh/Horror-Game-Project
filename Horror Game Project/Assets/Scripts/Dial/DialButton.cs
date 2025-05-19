using UnityEngine;

public class DialButton : MonoBehaviour
{
    public enum ButtonType { RotateCW, RotateCCW, SelectInner, SelectOuter }
    public ButtonType buttonType;
    public DialController3D dialController;

    // Called by CrosshairInteraction when button clicked
    public void OnPress()
    {
        switch (buttonType)
        {
            case ButtonType.RotateCW:
                dialController.RotateCW(transform);
                break;
            case ButtonType.RotateCCW:
                dialController.RotateCCW(transform);
                break;
            case ButtonType.SelectInner:
                dialController.SelectInner(transform);
                break;
            case ButtonType.SelectOuter:
                dialController.SelectOuter(transform);
                break;
        }
    }
}
