﻿/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 11/06/2021
* File: RobotSelectionForSensorSelection.cs : This file contains the code to
*       keep robot previously selecited in the "IndividualSelectionMenu" scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manage the application to keep the previously selected robot.
/// </summary>
public class RobotSelectionForSensorSelection : MonoBehaviour
{
    private int robotIndex;
    public GameObject selectedRobot;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpRobot();
    }

    /// <summary>
    /// Method to instantiate the selected robot in previous scene and make it 
    /// not destroyable when change between next scenes. 
    /// </summary>
    private void SetUpRobot()
    {
        robotIndex = PlayerPrefs.GetInt("RobotSelected");

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) && (i == robotIndex))
            {
                selectedRobot = Instantiate(transform.GetChild(i).gameObject, 
                    transform.GetChild(i).gameObject.transform.position, transform.GetChild(i).gameObject.transform.rotation);
                    selectedRobot.gameObject.tag = "SelectedRobot";
                selectedRobot.GetComponent<RobotManager>().Kinematic(true);
            }
        }

        if (!Finder.CheckContains("SelectedRobot"))
        {
            selectedRobot.SetActive(true);
            DontDestroyOnLoad(selectedRobot);
            Finder.AddObject(selectedRobot);
        }
    }
}
