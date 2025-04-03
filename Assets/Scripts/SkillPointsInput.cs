using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsInput : MonoBehaviour
{
    InputField inputField;
    void Start()
    {
        inputField = this.GetComponent<InputField>();
        inputField.onEndEdit.AddListener(value => SetSkillPoint(value));
    }

    void SetSkillPoint(string newValue)
    {
        int value = Int32.Parse(newValue);
        if (value > 1 && value < 99)
        Investigator.Instance.skillPoints[this.name] = value;
    }
}
