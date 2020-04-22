using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    const string menuHint = "You may type menu at any time.";
    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = { "police", "jail", "prisoner", "weapon", "FBI" };
    string[] level3Passwords = { "space", "ship", "satellite", "laser", "solar" };

    // Game state
    int level;

    string password;

    enum Screen { MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "close" || input == "exit" || input == "quit")
        {
            Terminal.WriteLine("Game was closed");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007") // Easter egg
        {
            Terminal.WriteLine("Choose the level, James Bond");
        }
        else
        {
            Terminal.WriteLine("Choose a valid level, please");
            Terminal.WriteLine(menuHint);
        }
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;

        Terminal.ClearScreen();
        Terminal.WriteLine("What you would to hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for the space station");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();

        Terminal.WriteLine("Enter your passsword, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();

        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Take this book...");
                Terminal.WriteLine(@"
    ________
   /       / /
  /       / /
 /______ / /
(_______( /
                ");
                break;
            case 2:
                Terminal.WriteLine("Take this pet with you...");
                Terminal.WriteLine(@"
              __/ ^\
        PIYO /     \ PIYO
             \_____/
             <    \/
                ");
                break;
            case 3:
                Terminal.WriteLine("Take this with you...");
                Terminal.WriteLine(@"
         /___ //----/___ //
         /              
         /__ //----/__ //
                ");
                break;
        }
    }
}
