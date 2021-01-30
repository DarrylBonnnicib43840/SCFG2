using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{

	

	public static timerManager instance;

    public bool timerStarted;

    float timerValue=0f;

    public Text timerText;
	
	public GameObject GameManager;
	
	
	
	
	void Awake(){
		MakeSingleton();
	}
	
	private void MakeSingleton(){
		if(instance !=null){
			Destroy (gameObject);
		} else{
			instance = this;
			DontDestroyOnLoad (gameObject);
			DontDestroyOnLoad (GameManager);
		
		}
	}
    

    IEnumerator timer()
    {
        while(true)
        { 
            if (timerStarted)
            {
                //measure the time
                timerValue++;

                float minutes = timerValue / 60f;
                float seconds = timerValue % 60f;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;

            }
            
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
		
		GetComponentInChildren<timerManager>().timerStarted = true;
		 
        //the text component attached to THIS object
        timerText = GetComponent<Text>();
        StartCoroutine(timer()); 
		
		 
    }

    
}
