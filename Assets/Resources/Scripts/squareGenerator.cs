using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//if we are using UI, we need to add this to the top of our code
using UnityEngine.UI;

//this is a CLASS
class Box
{
    //with 3 ATTRIBUTES
    public GameObject boxObject;
    public float scale;
    public string name;

    public string toString()
    {
        return "My Box is called" + this.name;
    }

}

public class squareGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    GameObject myBox,cross;

    Text debugTextObject;
    

    //instead of using the gameobject, I could use the class
    Box aBox;

    List<Box> allMyBoxes;
    
    IEnumerator changeBoxColor()
    {
        cross.transform.position = new Vector3(0f, 0f);
        foreach (Box b in allMyBoxes)
        {
            b.boxObject.GetComponent<SpriteRenderer>().color = Color.green;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


    //FOURTH THING TO HAPPEN
    IEnumerator animation2()
    {
        StartCoroutine(changeBoxColor());
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //get the current value of whether or not the text is showing
            bool debugenabled = GameObject.Find("debugText").GetComponent<Text>().enabled;
            //flip its value
            debugenabled = !debugenabled;
            //show or hide the text accordingly
            GameObject.Find("debugText").GetComponent<Text>().enabled = debugenabled;
        }    
    }

    


    //this method happens ONCE, FIRST THING TO HAPPEN
    void Start()
    {
        debugTextObject = GameObject.Find("debugText").GetComponent<Text>();
     
        //go to the Resources folder, then go to the Prefabs subfolder
        //and find the GameObject Square
        myBox = Resources.Load<GameObject>("Prefabs/Square");
        

        cross = new GameObject("Cross");
        cross.transform.position = new Vector3(0f, 0f);
        //cross.AddComponent<crossController>();

        //   StartCoroutine(createBoxes());
        
       
            
      
     
    }

    GameObject createSquare(float xpos,float ypos)
    {
       return Instantiate(myBox, new Vector3(xpos, ypos), Quaternion.identity);
    }

}
