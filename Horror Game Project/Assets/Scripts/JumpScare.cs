using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject jumpscareImage;
    public float jumpscareDuration = 1.5f;

    public void TriggerJumpScare()
    {
        StartCoroutine(ShowJumpscare());
    }

    public System.Collections.IEnumerator ShowJumpscare()
    {
        jumpscareImage.SetActive(true);
        yield return new WaitForSeconds(jumpscareDuration);
        jumpscareImage.SetActive(false);
    }
}
