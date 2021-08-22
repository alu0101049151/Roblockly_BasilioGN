﻿/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 18/06/2021
* File: CheckInclinometer.cs : This file contains the logic for the 
*       Inclinometer checkbox selection.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGyroscope : MonoBehaviour
{
    public delegate void GyroscopeChecked();
    public static GyroscopeChecked CheckboxGyroscope;

    public delegate void GyroscopeNotChecked();
    public static GyroscopeNotChecked DeactivateGyroscope;
    public void Acivation()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            CheckboxGyroscope();
        } else {
            gameObject.SetActive(false);
            DeactivateGyroscope();
        } 
    }
}