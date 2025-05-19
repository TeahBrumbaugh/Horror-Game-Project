using UnityEngine;

public class ModPuzzle : PuzzleBase
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