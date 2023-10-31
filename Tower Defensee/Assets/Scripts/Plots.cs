using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plots : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject tower;
    private Color startColor;


    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > Levelmanager.main.currency)
        {
            Debug.Log("u cant buy shit nigga");
            return;
        }
        Levelmanager.main.SpendCurrently(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }


    void Update()
    {
        
    }
}
