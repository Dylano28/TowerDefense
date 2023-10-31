using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{
    public static Levelmanager main;
    public Transform Startingpoint;
    public Transform[] path;

    public int currency;
    

    private void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrently(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("JE HEBT NIET GENOEG GELD LMAOOOOO BROKE ASS NIGGA");
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
