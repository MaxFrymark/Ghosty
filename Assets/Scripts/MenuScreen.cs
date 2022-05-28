using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] Inputs player;

    [SerializeField] GameObject mainMenu;

    [SerializeField] SceneLoader loader;

    [SerializeField] GameObject levelLostScreen;
    [SerializeField] GameObject levelWonScreen;


    public enum Menus { MainMenu, LevelWin, LevelLost, Tutorial, StartingMenu, EndScreen, None }
    [SerializeField] private Menus activeMenu;

    bool levelComplete = false;

    private void OnEnable()
    {
        player.PlayerControl = false;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        player.PlayerControl = true;
        Time.timeScale = 1;
    }

    public void SelectMenu(Menus menu)
    {
        activeMenu = menu;
        switch (activeMenu)
        {
            case Menus.MainMenu:
                mainMenu.SetActive(true);
                break;
            case Menus.LevelLost:
                levelLostScreen.SetActive(true);
                break;
            case Menus.LevelWin:
                levelComplete = true;
                levelWonScreen.SetActive(true);
                break;
                
        }
    }

    public void SpacebarPressed()
    {
        switch (activeMenu)
        {
            case Menus.MainMenu:
                mainMenu.SetActive(false);
                activeMenu = Menus.None;
                gameObject.SetActive(false);
                break;
            case Menus.StartingMenu:
                loader.LoadNextScene();
                break;
            case Menus.LevelWin:
                loader.LoadNextScene();
                break;
            case Menus.LevelLost:
                loader.ReloadLevel();
                break;
            case Menus.EndScreen:
                loader.RestartGame();
                break;
        }
    }

    
    public void EscapePressed()
    {
        if(activeMenu == Menus.StartingMenu || activeMenu == Menus.EndScreen)
        {
            Application.Quit();
        }

        else
        {
            loader.RestartGame();
        }
    }
}
