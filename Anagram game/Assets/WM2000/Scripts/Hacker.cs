using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
   
    //  This variable will tell the user that he or she can type menu at any screen and it will return to the menu screen
    const string menuHint = "You can type menu at any time.";

    //  These arrays hold the passwords to be used in our game
    string[] level1Passwords = { "tomato", "towel", "cheese", "shirt", "milk", "shampoo" };
    string[] level2Passwords = { "tuition", "assignment", "schedule", "professor", "essay" };
    string[] level3Passwords = { "espionage", "dossier", "international", "terrorism" };
    string[] level4Passwords = { "grape", "pineapple", "watermelon", "banana" };
    string[] level5Passwords = { "muse", "aerosmith", "acdc", "queen" };

    //  Game state variables
    int level;                                      //  Level that we are cracking
    enum GameState { MainMenu, Password, Win };     //  Screen states
    GameState currentScreen = GameState.MainMenu;   //  Current screen state
    string password;                                //  Password to be cracked;

    //  This flag help us by not changing the password until the gamers crack it.
    bool changePassword;     

    //  Initialization
    //  With the ShowMainMenu() method we initialize the game
    void Start()
    {
        ShowMainMenu(); //  Main menu is shown to the user
    }

    //  This method will show the user the game's main menu
    void ShowMainMenu()
    {
        //  Clear the screen
        Terminal.ClearScreen();

        //  Show the menu
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for the local supermarket");
        Terminal.WriteLine("Press 2 for the university");
        Terminal.WriteLine("Press 3 for the NSA");
        Terminal.WriteLine("Press 4 for the Funny fruits market");
        Terminal.WriteLine("Press 5 for the International allience of awesome bands");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");

        //  We set the first current screen as the main menu
        currentScreen = GameState.MainMenu;

        //  With this flag we set that we will try to crack the password
        changePassword = true;
    }


    //This method is called whenever the user clicks enter on the keyboard
    void OnUserInput(string input)
    {
        //  If the user types menu on the terminal the ShowMainMenu method will be called and will return the user the menu
        if (input == "menu")
        {
            ShowMainMenu();
        }
        //  However, if the user types quit, close or exit it will ask the user to close the game
        else if (input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("Please, close the browser's tab"); //If the user is playing on the web it will ask to close the browser's tab
            Application.Quit();
        }
     
        else if (currentScreen == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
  
        else if (currentScreen == GameState.Password)
        {
            CheckPassword(input);
        }
    }

    //This method verifys if the password introduced by the user is correct, if it is then it will display the win screen.
    private void CheckPassword(string input)
    {
     
        if (input == password)
        {
            DisplayWinScreen();
        }
        //  If the password is incorrect we call again the AskForPassword() method.
        else
        {
            AskForPassword();
        }
    }

    //  This method updates the current game state and displays the win screen.
    private void DisplayWinScreen()
    {
        //  We set the current game state to be the Win screen. 
        currentScreen = GameState.Win;

        // Clear the screen
        Terminal.ClearScreen();

        // We call this method to provide a reward to the user and only typing menu will return the user to the Menu
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    //  This method shows a reward for the user.
    private void ShowLevelReward()
    {
        //  Here we have the rewards depending on the level 
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Grab a lime!");
                Terminal.WriteLine(@"
     /
  __|__
 /     \
|       )
 \_____/
                ");
                break;
            case 2:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /______//
(______(/
                ");
                break;
            case 3:
                Terminal.WriteLine("Greetings...");
                Terminal.WriteLine(@"
 _ __   ___  __ _
| '_ \ / __|/ _` |
| | | |\__ \ (_| |
|_| |_||___)\__,_|
                ");
                Terminal.WriteLine("Welcome to the NSA server");
                break;
            case 4:
                Terminal.WriteLine("Take a bunny!");
                Terminal.WriteLine(@"
 (\__/)
 (='.'=) 
('')_('')
                ");
                break;
            case 5:
                Terminal.WriteLine("Here is a guitar!");
                Terminal.WriteLine(@"
            */.) 
           */..| 
          */...|
         *(___)
           |_| 
           |_| 
           |=|  
           |=|
       __  |=| ____
      /#|___|=|___/_ 
     (##(__..|=|__)#) 
     ###/__|=|_/###/ 
      (##)#____.##(/ 
       )#/#____####) 
      /#|##____#####) 
     (##___###.#####) 
      (############) 
                ");
                break;
            default:
                Debug.LogError("Invalid level reached.");
                break;
        }
    }

    //  This method validates the user's input when the game state is MainMenu.
    void RunMainMenu(string input)
    {
        //  We check if the input introduced is valid.
        bool isValidInput = (input == "1") || (input == "2") || (input == "3") || (input == "4") || (input == "5");

        //  If the user inputs a valid level, we convert that input to an int
        //  value and then we call the AskForPassword() method.
        if (isValidInput)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        //  If the user did not enter a valid input, then we validate
        //  for our Easter egg.  If the user enters "007", we greet them as
        //  Mr. Bond (with utmost respect!)
        else if (input == "007")
        {
            Terminal.WriteLine("Please enter a valid level, Mr. Bond");
        }
        //If the user types 911 we tell them to calm down
        else if (input == "911")
        {
            Terminal.WriteLine("I believe this is not an emergency, please keep calm and enter a valid level");
        }
        //  If it's not any of the others then we print this.
        else
        {
            Terminal.WriteLine("Enter a valid level");
        }
    }

    //  This method set the current game state to Password screen and chooses
    //  a new password if needed.
    private void AskForPassword()
    {
        //  Here we set the current game state as Password
        currentScreen = GameState.Password;

        //  Clear our terminal screen
        Terminal.ClearScreen();

        //  Here we call the SetRandomPassword() method if we need to change our
        //  password.
        if (changePassword)
        {
            SetRandomPassword();
        }

        //  We ask the user to enter the password, giving them a hint of
        //  its characters.
        Terminal.WriteLine("Enter your password. Hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    //  This method randomly choses a password to be cracked, based on the level.
    private void SetRandomPassword()
    {
        //  Since we are choosing a new password to crack, we set our
        //  changePassword flag to false.  This way, the game won't change the
        //  password if the use does not guess correctly.
        changePassword = false;

        switch (level)
        {
            case 1:
                password = level1Passwords[UnityEngine.Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[UnityEngine.Random.Range(0, level3Passwords.Length)];
                break;
            case 4:
                password = level4Passwords[UnityEngine.Random.Range(0, level4Passwords.Length)];
                break;
            case 5:
                password = level5Passwords[UnityEngine.Random.Range(0, level5Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level.  How did you manage that?");
                
                //  We set our changePassword flag to true once more.
                changePassword = true;
                break;
        }
    }
}
