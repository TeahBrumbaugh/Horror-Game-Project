using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{

    // Call right after Instantiate to show panel and reset state.
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }


    // Call when the puzzle is done
    // Cleans up via PuzzleManager

    protected void Close()
    {
        PuzzleManager.Instance.CloseCurrentPuzzle();
    }
}
