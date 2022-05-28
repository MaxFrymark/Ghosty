using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentTracker : MonoBehaviour
{
    [SerializeField]MenuScreen menu;
    
    List<Resident> residents = new List<Resident>();

    bool levelComplete = false;
    public bool LevelComplete { get { return levelComplete; } }

    public void AddResident(Resident resident)
    {
        residents.Add(resident);
    }

    public void RemoveResident(Resident resident)
    {
        residents.Remove(resident);
        if(residents.Count == 0)
        {
            levelComplete = true;
            StartCoroutine(EndingLevel());
        }
    }

    private IEnumerator EndingLevel()
    {
        yield return new WaitForSeconds(2);
        menu.gameObject.SetActive(true);
        menu.SelectMenu(MenuScreen.Menus.LevelWin);
    }
}
