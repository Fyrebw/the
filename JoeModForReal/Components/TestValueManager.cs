﻿using ModdedEntityStates.TeslaTrooper;
using UnityEngine;

internal class TestValueManager : MonoBehaviour {

    //how do doing attributes
    //[debugfloat("valuename", KeyCode.U, KeyCode.J, 5)] on any static value elsewhere
    //would be neat
    //public static float bloob = 0.7f;

    private float _tim;
    private float _holdTime = 0.4f;    

    //compiler flags when
    private bool _testingEnabled => Modules.Config.Debug;
    public static float value1 = 5.5f;
    public static float value2 = 1.8f;
    
    public static float value3 = 50f;
    public static float value4 = 0.5f;
    public static float value5 = 0.2f;
    
    public static float value6 = 150f;

    void Update() {
        if (!_testingEnabled)
            return;

        if (!Input.GetKey(KeyCode.LeftAlt))
            return;

        manageTestValue(ref value1, "1 fireball dam", KeyCode.T, KeyCode.G, 0.1f);

        manageTestValue(ref value2, "2 primary dam", KeyCode.Y, KeyCode.H, 0.1f);

        manageTestValue(ref value3, "3 tenticle armor", KeyCode.U, KeyCode.J, 1f);
        manageTestValue(ref value4, "4 tenticle move", KeyCode.I, KeyCode.K, 0.1f);
        manageTestValue(ref value5, "5 tenticle aspeed", KeyCode.O, KeyCode.L, 0.1f);

        manageTestValue(ref value6, "6 dash armor", KeyCode.P, KeyCode.Semicolon, 10f);
    }

    private void manageTestValue(ref float value, string valueName, KeyCode upKey, KeyCode downKey, float incrementAmount) {

        if (Input.GetKeyDown(upKey)) {

            value = setTestValue(value + incrementAmount, valueName);
        }

        if (Input.GetKeyDown(downKey)) {
            
            value = setTestValue(value - incrementAmount, valueName);
        }


        if (Input.GetKey(upKey) || Input.GetKey(downKey)) {

            float amount = incrementAmount * (Input.GetKey(upKey) ? 1 : -1);

            _tim += Time.deltaTime;

            if (_tim > _holdTime) {
                _tim = _holdTime - 0.02f; 
                value = setTestValue(value + amount, valueName);
            }
        }


        if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey)) {
            _tim = 0;
        }

    }

    private float setTestValue(float value, string print) {
        Helpers.LogWarning($"{print}: {value.ToString("0.000")}");
        return value;
    }
}
