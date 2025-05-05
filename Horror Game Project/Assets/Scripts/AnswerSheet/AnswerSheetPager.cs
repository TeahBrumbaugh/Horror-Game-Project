using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AnswerSheetPager : MonoBehaviour
{
    [SerializeField] private Transform pageContainer;
    [SerializeField] private GameObject inputPageTemplate;
    [SerializeField] private GameObject displayPageTemplate;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private TMP_Text pageNumberText;


    private List<GameObject> pages = new List<GameObject>();
    private int currentPageIndex = 0;

    private void Start()
    {
        inputPageTemplate.SetActive(false);
        displayPageTemplate.SetActive(false);
        AddNewInputPage();
        UpdateNavButtons();
    }

    public void OnSubmitCurrentPage()
    {
        var inputPage = pages[currentPageIndex].GetComponent<AnswerSheetInputPageManager>();
        inputPage.SubmitAnswers();

        // Always convert the current page to a display-only page
        GameObject displayPage = Instantiate(displayPageTemplate, pageContainer);
        var display = displayPage.GetComponent<AnswerSheetDisplayPage>();
        display.SetAnswers(inputPage.Answer1, inputPage.Answer2, inputPage.IsCorrect1, inputPage.IsCorrect2);

        // Replace current input page with read-only page
        inputPage.gameObject.SetActive(false);
        pages[currentPageIndex] = displayPage;

        AddNewInputPage(); // Always add a fresh blank input page
    }


    private void AddNewInputPage()
    {
        GameObject newInput = Instantiate(inputPageTemplate, pageContainer);
        newInput.SetActive(true);
        pages.Add(newInput);
        GoToPage(pages.Count - 1);
    }

    public void GoToNextPage() => GoToPage(currentPageIndex + 1);
    public void GoToPreviousPage() => GoToPage(currentPageIndex - 1);

    private void GoToPage(int index)
    {
        if (index < 0 || index >= pages.Count) return;

        pages[currentPageIndex].SetActive(false);
        currentPageIndex = index;
        pages[currentPageIndex].SetActive(true);

        UpdateNavButtons();
        UpdatePageNumber(); // ⭐ Call this
    }


    private void UpdateNavButtons()
    {
        previousButton.interactable = currentPageIndex > 0;
        nextButton.interactable = currentPageIndex < pages.Count - 1;
    }

    private void UpdatePageNumber()
{
    if (pageNumberText != null)
    {
        pageNumberText.text = $"Page {currentPageIndex + 1} of {pages.Count}";
    }
}

}
