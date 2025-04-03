using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigator : MonoBehaviour
{
    public string firstName, lastName;
    public int age, birthMonth, birthDay;
    public Dictionary<string, int> skillPoints;

    public static Investigator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        firstName = "";
        lastName = "";
        age = 20;
        birthMonth = 1;
        birthDay = 1;
        skillPoints = new Dictionary<string, int>();
        
    }
}
