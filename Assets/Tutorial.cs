using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] MenuScreen menu;
    [SerializeField] GameObject[] tutorialPages;

    int currentPage = 0;

    private void OnEnable()
    {
        tutorialPages[currentPage].SetActive(true);
    }

    public void NextPage()
    {
        if (currentPage == tutorialPages.Length - 1)
        {
            menu.EscapePressed();
        }

        else
        {
            tutorialPages[currentPage].SetActive(false);
            currentPage++;
            tutorialPages[currentPage].SetActive(true);
        }
    }
}
