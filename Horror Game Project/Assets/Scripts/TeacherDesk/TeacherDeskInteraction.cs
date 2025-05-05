using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherDeskInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float gradingTime = 3f;
    [SerializeField] private GameObject answerSheetPanel;
    [SerializeField] private CanvasGroup answerSheetCanvasGroup; // fade + interactability
    [SerializeField] private TMP_Text gradingText; // optional "Grading..." timer display
    [SerializeField] private AnswerSheetToggle answerSheetToggle;


    private bool isGrading = false;
    private float holdTime = 0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        LockAnswerSheet(); // Start hidden/locked
    }

    void Update()
    {
        if (isGrading) return;

        // Check if mouse is over desk
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f))
            {
                if (hit.transform == this.transform)
                {
                    float dist = Vector3.Distance(cam.transform.position, transform.position);
                    if (dist <= interactionDistance)
                    {
                        holdTime += Time.deltaTime;
                        gradingText.text = $"Grading... {Mathf.Ceil(gradingTime - holdTime)}s";

                        if (holdTime >= gradingTime)
                        {
                            UnlockAnswerSheet();
                        }
                        return;
                    }
                }
            }
        }

        holdTime = 0f;
        gradingText.text = "";
    }

    private void LockAnswerSheet()
    {
        if (answerSheetToggle != null)
            answerSheetToggle.DisableToggle(); //  prevent toggling during grading

        if (answerSheetCanvasGroup != null)
        {
            answerSheetCanvasGroup.alpha = 0;
            answerSheetCanvasGroup.interactable = false;
            answerSheetCanvasGroup.blocksRaycasts = false;
        }

        if (answerSheetPanel != null)
            answerSheetPanel.SetActive(false);
    }


    private void UnlockAnswerSheet()
    {
        isGrading = true;
        gradingText.text = " Graded!";

        if (answerSheetToggle != null)
            answerSheetToggle.EnableToggle(); //  allow toggling again
    }


}
