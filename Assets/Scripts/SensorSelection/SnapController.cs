/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 07/06/2021
* File: SnapController.cs : This file contains the 
*       "SnapController" class implementation. This class is
*       used to manage the snap point to fix the selected sensors
*       to each robot.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the different snap points of each robot.
/// </summary>
public class SnapController : MonoBehaviour
{                                       
    public List<Transform> SnapPoints; // Contains all the snap points of each 
                                       // robot.
    public float SnapRange = 2.0f;
    DragObject[] SensorsToDrag; // Contains all the draggable
                                // sensors;

    // The delegate below is used to create a new box in the "Sensores añadidos" panel. This deletate is used in the "BoxesManager.cs" script.
    public delegate void CreateAddedSensorBox(DragObject sensorOfTheBox);
    public static event CreateAddedSensorBox CreateNewAddedSensorBox;

    private List<Transform> UsedSnapPoints = new List<Transform>();

    /// <summary>
    // Start is called before the first frame update
    /// </summary>
   void Start()
    {
        AddedSensorBoxScript.SetFreeSnappedPoint = SetFreeSnapPoint;
        DragObject.ActiveSnapPointsColor = ActivateSnapPoints;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        AddNewSensor();
    }

    /// <summary>
    /// AddNewSensor is called in every frame on the Update loop to check if
    /// a new sensor has been created.
    /// </summary>
    private void AddNewSensor()
    {
        SensorsToDrag = Object.FindObjectsOfType<DragObject>(); // Looks for 
                                                    // each new added sensor.
        if (SensorsToDrag.Length > 0)
        {
            for (int i = 0; i < SensorsToDrag.Length; i++)
            {
                if (SensorsToDrag[i].tag == "NewSensor")
                {
                    SensorsToDrag[i].tag = "NotNewSensor";
                    SensorsToDrag[i].DragFinished = OnDragEnded;
                }
            }
        }
    }

    /// <summary>
    /// OnDragEnded is called when the mouse up event occurs and consequently, 
    /// the dragg process finish.
    /// </summary>
    private void OnDragEnded(DragObject sensorToDrag)
    {
        float ClosestDistance = -1;
        Transform ClosestSnapPoint = null;
        foreach (Transform currentSnapPoint in SnapPoints)
        {
            float currentDistance = Vector3.Distance(sensorToDrag.transform.position, currentSnapPoint.transform.position);
            if (ClosestSnapPoint == null || currentDistance < ClosestDistance)
            {
                ClosestSnapPoint = currentSnapPoint;
                ClosestDistance = currentDistance;
            }
        }

        bool used = false; // Auxiliary variable to check if the closest snap 
                           // point has been used.
        if (UsedSnapPoints.Count > 0) // Check if any snap point has been used.
        {
            //Debug.Log("UsedSanpPoints.Count: " + UsedSnapPoints.Count);
            for (int i = 0; i < UsedSnapPoints.Count; i++)
            {
                if(UsedSnapPoints[i] == ClosestSnapPoint)
                {
                    used = true;
                }
            }
        }

        if (!used) // If the Closest Sanp Point is free
        {
            //Debug.Log("ClosestSnapPoint = " + ClosestSnapPoint);
            //Debug.Log("ClosestDistance = " + ClosestDistance);

            if ((ClosestSnapPoint != null) && (ClosestDistance <= SnapRange))
            { // If the snap was successful:
                UsedSnapPoints.Add(ClosestSnapPoint);
                sensorToDrag.SnappedPoint = ClosestSnapPoint;
                //Debug.Log("Ha hecho el snap");
                switch (sensorToDrag.name)
                {
                    case "SensorUS(Clone)":
                        SetUltrasoundSensor(sensorToDrag, ClosestSnapPoint);
                        break;
                    case "SensorTouch(Clone)":
                        SetTouchSensor(sensorToDrag, ClosestSnapPoint);
                        break;
                    case "SensorIR(Clone)":
                        SetInfraredColorSensor(sensorToDrag, ClosestSnapPoint);
                        break;
                    case "SensorColor(Clone)":
                        SetInfraredColorSensor(sensorToDrag, ClosestSnapPoint);
                        break;
                    default:
                        Debug.Log("There aren't any sensor of this type");
                        break;
                }
                sensorToDrag.transform.parent = gameObject.transform; // Adds the sensor as a child of the robot.
                CreateNewAddedSensorBox(sensorToDrag);
                //CreateAddedSensorBox();
            }
            else // If the sensor doesn't snaps any point.
            {
                ActivateSnapPoints(false); // Turn off color in the snap points.
                Destroy(sensorToDrag.gameObject);
            }
        }
        else // If the sensor doesn't snaps any point.
        {
            ActivateSnapPoints(false); // Turn off color in the snap points.
            Destroy(sensorToDrag.gameObject);
        }
    }

    /// <summary>
    /// This method is used to set up the position of any Ultrasound sensor 
    /// when it is snapped to the robot.
    /// </summary>
    private void SetUltrasoundSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //Debug.Log ("Es un sensor de ultrasonido");
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = 3f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case "TopFrontSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "BackSnap":
                offset = 3f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, -90.0f, 0.0f);
                break;
            case "LeftSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 5.0f, -60.0f);
                break;
            case "RigthSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
        }
    }
    
    /// <summary>
    /// This method is used to set up the position of any Touch sensor 
    /// when it is snapped to the robot.
    /// </summary>
    private void SetTouchSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case "TopFrontSnap":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "BackSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, -90.0f, 0.0f);
                break;
            case "LeftSnap":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 0.0f, -60.0f);
                break;
            case "RightSnap":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
        }
    }

    /// <summary>
    /// This method is used to set up the position of any Infrared or Color
    /// sensor when it is snapped to the robot.
    /// </summary>
    private void SetInfraredColorSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(70.0f, 180.0f, 0.0f);
                break;
            case "TopFrontSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(110.0f, 0.0f, 180.0f);
                break;
            case "BackSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 0.0f, 0.0f);
                break;
            case "LeftSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 90.0f, 0.0f);
                break;
            case "RightSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 180.0f, 0.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
        }
    }

    /// <summary>
    /// This method is used to set up the position of any Touch sensor 
    /// when it is snapped to the robot.
    /// </summary>
    private void SetFreeSnapPoint(Transform snappedPoint)
    {
        for (int i = 0; i < UsedSnapPoints.Count; i++)
        {
            if (UsedSnapPoints[i] == snappedPoint)
            {
                UsedSnapPoints.Remove(UsedSnapPoints[i]);
            }
        }
    }

    /// <summary>
    /// This method is used to activate and deactivate the snap points when the 
    /// user drags and drops a sensor. 
    /// </summary>
    private void ActivateSnapPoints(bool activation)
    {
        if (activation)
        {
            //Debug.Log("Activation: true");
            for (int i = 0; i < SnapPoints.Count; i++)
            {
                SnapPoints[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 1)); // Change color to red.
            }
        } else {
            //Debug.Log("Activation: false");
            for (int i = 0; i < SnapPoints.Count; i++)
            {
                SnapPoints[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, 0)); // Changes back the color to transparent.
            }
        }
    }
}
