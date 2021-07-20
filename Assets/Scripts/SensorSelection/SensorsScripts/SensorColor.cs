﻿/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 20/07/2021
* File: SensorColor.cs : This file contains the "SensorColor" class 
*       implementation. This class is used to manage any logic of the Color 
*       sensor type.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage any logic of the Color sensor type.
/// </summary>
public class SensorColor : SensorGeneric
{
    private float range;
    private float precision;
    // Start is called before the first frame update
    void Start()
    {
        SnapController.SetLinkedToARobotOn += base.SetLinkSensor;
    }

    public override void SetSensorName(string snapPoint)
    {
        base.SetSensorName(snapPoint);
        gameObject.transform.name = gameObject.transform.name + "Color";
        base.StoreSensorName(gameObject.transform.name);
    }
}
