using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class eventsController : MonoBehaviour
{
    GameObject emailInputField, registerButton;
    string myVar = "hello";
    // Start is called before the first frame update
    void Start()
    {
        emailInputField = GameObject.Find("emailInputField");
        registerButton = GameObject.Find("registerButton");
        
        //button callback method 1
        registerButton.GetComponent<Button>().onClick.AddListener(
            () => 
            { 
                Debug.Log("register User here!");
                Debug.Log(myVar);
            }    
         ); 

        //button callback method 2
        registerButton.GetComponent<Button>().onClick.AddListener(registerButtonPressed);


        //button callback method 3
        registerButton.GetComponent<Button>().onClick.AddListener(
            delegate { 
                registerButtonPressedWithParam("Hello hello");  
                }  
            ); 

        //registering button callbacks
    }

    void registerButtonPressedWithParam(string myparam)
    {
        Debug.Log(myparam);
    }


    void registerButtonPressed()
    {
        Debug.Log("register user here 2!");
        Debug.Log(myVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
