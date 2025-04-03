using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour
{

    public InputField firstNameInput, lastNameInput, ageInput, birthMonthInput, birthDayInput;
    public Text errorMessage;

    public void Start()
    {
        firstNameInput.onEndEdit.AddListener(value => SetFirstName(value));
        lastNameInput.onEndEdit.AddListener(value => SetLastName(value));
        ageInput.onEndEdit.AddListener(value => SetAge(value));
        birthMonthInput.onEndEdit.AddListener(value => SetBirthMonth(value));
        birthDayInput.onEndEdit.AddListener(value => SetBirthDay(value));
        
        InputField[] characteristicInputs = GameObject.Find("Characteristic").GetComponentsInChildren<InputField>();
        foreach(InputField inputField in characteristicInputs)
        {
            inputField.onEndEdit.AddListener(value => SetCharacteristicPoint(inputField, value));
        }
    }

    public void SetFirstName(string firstName)
    {
        string newName = firstName.Replace(" ", "");
        if (!newName.Contains('*') && !newName.Contains(';') && !newName.Contains('/'))
        {
            Investigator.Instance.firstName = newName;
        }
        else
        {
            firstNameInput.text = Investigator.Instance.firstName;
        }
    }
    public void SetLastName(string lastName)
    {
        string newName = lastName.Replace(" ", "");
        if (!newName.Contains('*') && !newName.Contains(';'))
        {
            Investigator.Instance.lastName = newName;
        }
        else
        {
            lastNameInput.text = Investigator.Instance.lastName;
        }
    }
    
    public void SetAge(string newAge)
    {
        if (string.IsNullOrWhiteSpace(newAge))
        {
            ageInput.text = Investigator.Instance.age.ToString();
            return;
        }

        int age = Int32.Parse(newAge);
        if (age >= 20 && age <= 50)
            Investigator.Instance.age = age;
        else
            ageInput.text = Investigator.Instance.age.ToString();
    }

    public void SetBirthMonth(string newBirthMonth)
    {
        if (string.IsNullOrWhiteSpace(newBirthMonth))
        {
            birthMonthInput.text = Investigator.Instance.birthMonth.ToString();
        }
        
        int birthMonth = Int32.Parse(newBirthMonth);
        if (IsValidDate(birthMonth, Investigator.Instance.birthDay))
        {
            Investigator.Instance.birthMonth = birthMonth;
        }
        else
        {
            birthMonthInput.text = Investigator.Instance.birthMonth.ToString();
        }
    }
    
    public void SetBirthDay(string newBirthDay)
    {
        if (string.IsNullOrWhiteSpace(newBirthDay))
        {
            birthDayInput.text = Investigator.Instance.birthDay.ToString();
        }

        int birthDay = Int32.Parse(newBirthDay);
        if (IsValidDate(Investigator.Instance.birthMonth, birthDay))
        {
            Investigator.Instance.birthDay = birthDay;
        }
        else
        {
            birthDayInput.text = Investigator.Instance.birthDay.ToString();
        }
    }

    private bool IsValidDate(int month, int day)
    {
        if(month < 1 || month > 12)
            return false;
        int maxDays = System.DateTime.DaysInMonth(1448, month);
        return day >= 1 && day <= maxDays;
    }

    public void SetCharacteristicPoint(InputField inputField, string newValue)
    {
        int point = Int32.Parse(newValue);
        if (point < 10 || point > 99)
        {
            point = 10;
            inputField.text = point.ToString();
        }
        Investigator.Instance.skillPoints[inputField.name] = point;
    }

    public void Submit()
    {
        Investigator investigator = Investigator.Instance;
        if (string.IsNullOrWhiteSpace(investigator.firstName) || string.IsNullOrWhiteSpace(investigator.lastName))
        {
            errorMessage.text = "이름이 올바르지 않습니다.";
        }
        else if (investigator.age == 0)
        {
            errorMessage.text = "나이는 최소 20세, 최대 50세 내의 범위여야 합니다.";
        }
        else if (!IsValidDate(investigator.birthMonth, investigator.birthDay))
        {
            errorMessage.text = "생일 날짜가 올바르지 않습니다.";
        }
        else if (!investigator.skillPoints.ContainsKey("EXP") || !investigator.skillPoints.ContainsKey("PHY") || !investigator.skillPoints.ContainsKey("INT") || !investigator.skillPoints.ContainsKey("LCK"))
        {
            errorMessage.text = "입력되지 않은 특성치가 있습니다. 각 값이 10 이상인지 확인해주세요.";
        }
        else if (investigator.skillPoints["EXP"] + investigator.skillPoints["PHY"] + investigator.skillPoints["INT"] + investigator.skillPoints["LCK"] > 200)
        {
            errorMessage.text = "특성치의 합은 200 이하여야 합니다.";
        }
        else
        {
            Debug.Log("성공!");
        }

    }

}