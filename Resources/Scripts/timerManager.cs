using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerManager : MonoBehaviour
{

	

	public static timerManager instance;

    public bool timerStarted;

    float timerValue=0f;

    public Text timerText;
	
	public GameObject GameManager;
	
	
	 public InputField inputField;
	 
	 bool isPaused = false;
	 
	 Coroutine lastRoutine = null;
	 
	
	
	
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
		
		
 
		lastRoutine = StartCoroutine(timer());
         
		
		 
    }
	
	void Update(){
		
		
		
		
		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;
		
		
		if(sceneName == "SnakeEndScene"){
			
			timerStarted = false;
			
		}
		
		if(sceneName == "SnakeScene1"){
			
			timerStarted = true;
		}
		
		if(sceneName == "SnakeWinScene"){
			
			
			StopCoroutine(lastRoutine);
			
		}
		
		
	}

    
}
