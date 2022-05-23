using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentTracker : MonoBehaviour
{
    [SerializeField]MenuScreen menu;
    
    List<Resident> residents = new List<Resident>();

    public void AddResident(Resident resident)
    {
        residents.Add(resident);
    }

    public void RemoveResident(Resident resident)
    {
        residents.Remove(resident);
        if(residents.Count == 0)
        {
            menu.gameObject.SetActive(true);
            menu.SelectMenu(MenuScreen.Menus.LevelWin);
        }
    }
}
