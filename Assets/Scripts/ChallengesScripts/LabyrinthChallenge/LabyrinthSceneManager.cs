/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 26/07/2021
* File: LabyrinthSeceneManager.cs : This file contains the 
*       "LabyrinthSceneManager" class implementation which allows the 
*        configuration of some stuff of the scene as:
*           - Set the position of the selectedRobot gameobject.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to manage the scene configuration.
/// </summary>
public class LabyrinthSceneManager : MonoBehaviour
{
    private GameObject selectedRobot;
    [SerializeField] private Transform startPoint; // Is the point from which the selected robot will take its position and rotation.

    public GameObject maximizeButtonSmall;
    public GameObject maximizeButtonBig;
    public GameObject smallChallengeViewer;
    public GameObject bigChallengeViewer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SetUpSelectedRobot();
    }

    /// <summary>
    /// This method sets up the robot position and rotation at the start 
    /// method of the scene
    /// </summary>
    void SetUpSelectedRobot()
    {
        selectedRobot = GameObject.FindWithTag("SelectedRobot");
        selectedRobot.transform.position = startPoint.position;
        float XPosCoord = selectedRobot.transform.position.x;
        float YPosCoord = selectedRobot.transform.position.y;
        float ZPosCoord = selectedRobot.transform.position.z;   
        selectedRobot.transform.position = new Vector3(XPosCoord, YPosCoord + 10, ZPosCoord);
        selectedRobot.transform.rotation = startPoint.rotation;
        selectedRobot.GetComponent<RobotManager>().Kinematic(true); // Enables robot physics again.
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSelectionScene" to the "MainMenu" scene.
    /// </summary>
    public void GoBack ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ChangeToBig()
    {
        if ((!bigChallengeViewer.activeSelf) && (smallChallengeViewer.activeSelf))
        {
            smallChallengeViewer.SetActive(false);
            bigChallengeViewer.SetActive(true);
        }
    }

    public void ChangeToSmall()
    {
        if ((bigChallengeViewer.activeSelf) && (!smallChallengeViewer.activeSelf))
        {
            bigChallengeViewer.SetActive(false);
            smallChallengeViewer.SetActive(true);
        }
    }
}