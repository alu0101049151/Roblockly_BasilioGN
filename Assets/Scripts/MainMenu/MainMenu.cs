/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 15/05/2021
* File: MainMenu.cs: This file contains a MainMenu class implementation to 
       to manage the transitions from the main menu to other scenes. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Used to make the change between scenes.

/// <summary>
/// Class used to manage the transitions from the MainMenu scene to the others.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Method of transitioning to the robot selection scene for individual 
    /// challenge.
    /// </summary>
    public void IndividualChallenge ()
    {
        // Load the next level in the queue (in this case, is the scene 1, called "IndividualSelectionMenu").
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    /// <summary>
    /// Method to quit the game.
    /// </summary>
    public void QuitGame ()
    {   Debug.Log("Quit!"); // To check that it runs properly in Unity
        Application.Quit();
    }
}