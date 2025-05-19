using UnityEngine;

public class DialPuzzle : PuzzleBase
{
    public override void Show()
    {
        base.Show();
    }

    public void OnSubmit()
    {
        Close();
    }
}
