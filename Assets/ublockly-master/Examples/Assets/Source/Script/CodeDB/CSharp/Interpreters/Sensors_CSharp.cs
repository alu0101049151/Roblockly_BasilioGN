using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "sensors_touch_contact")]
    public class Detection_Sensor_Touch_Contact_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetTouchInfo(sensorPosition);
            Debug.Log("Devuelve " + data.ToString());
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_touch_not_contact")]
    public class Detection_Sensor_Touch_Not_Contact_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetTouchInfo(sensorPosition);
            if(!data)
            {
                data = true;
            } else {
                data = false;
            }
            Debug.Log("Devuelve " + data.ToString());
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_ir_detect_white")]
    public class Detection_Sensor_IR_White_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetIRInfo(sensorPosition, "White (Instance)");
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_ir_detect_black")]
    public class Detection_Sensor_IR_Black_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetIRInfo(sensorPosition, "Black (Instance)");
            return new DataStruct(data);
        }
    }
}