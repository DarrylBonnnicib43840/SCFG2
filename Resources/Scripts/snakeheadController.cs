﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class snakeheadController : MonoBehaviour
{
    snakeGenerator mysnakegenerator;
    foodGenerator myfoodgenerator,myfoodgenerator2;
	
   
    public Vector3 findClosestFood()
    {
        if (myfoodgenerator.allTheFood.Count > 0)
        {
            List<positionRecord> sortedFoods = myfoodgenerator.allTheFood.OrderBy(
        x => Vector3.Distance(this.transform.position, x.Position)
       ).ToList();
            return sortedFoods[0].Position;
        }
        return new Vector3(0f, 0f);
    }

    public IEnumerator automoveCoroutine()
    {
        while(true)
        {


            yield return null;
        }
        
    }



    private void Start()
    {
        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
        myfoodgenerator = Camera.main.GetComponent<foodGenerator>();
        

    }

    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize-1)) || (transform.position.x > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(-transform.position.x,transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 1)) || (transform.position.y > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }


    }
	void OnTriggerEnter2D(Collider2D collision){
		
		
		
		if(collision.CompareTag("EndPoint")){
			Debug.Log("Restart Scene");
			//GameManager.snakeLength = 0;
			string currentSceneName = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}
		
		if(collision.CompareTag("Obsticle")){
			Debug.Log("Restart Scene");
			
			
			SceneManager.LoadScene("SnakeEndScene");
		}
		
		if(collision.CompareTag("Enemy")){
			Debug.Log("Restart Scene");
			
			SceneManager.LoadScene("SnakeEndScene");
		}
		
		
		
		
	}

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(1f,0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(1f, 0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }

        

        //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength)); 
    }
	
}
