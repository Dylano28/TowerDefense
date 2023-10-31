using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    

    private void OnGUI()
    {
        currencyUI.text = Levelmanager.main.currency.ToString();
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;  
        anim.SetBool("MenuOpen", isMenuOpen);
    }
}
