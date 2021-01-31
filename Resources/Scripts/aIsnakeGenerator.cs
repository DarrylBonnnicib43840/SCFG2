using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class positionRecord2

{


    //the place where I've been
    Vector3 position;
    //at which point was I there?
    int positionOrder;
 

    GameObject breadcrumbBox;

    public void changeColor()
    {
        this.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.black;
    }




    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        positionRecord2 p = obj as positionRecord2;
        if ((System.Object)p == null)
            return false;
        return position == p.position;
    }


    public bool Equals(positionRecord2 o)
    {
        if (o == null)
            return false;

        
            //the distance between any food spawned
            return Vector3.Distance(this.position,o.position) < 2f;
       
       
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }




    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

public class aIsnakeGenerator : MonoBehaviour
{

    public int Aisnakelength;

	public GameObject enemyBox;

    int pastpositionslimit = 100;

    GameObject breadcrumbBox,pathParent;

    List<positionRecord2> pastPositions;

    int positionorder = 0;

    bool firstrun = true;
	
	
	

    Color AisnakeColor;



    // Start is called before the first frame update
    void Start()
    {
		
        AisnakeColor = Color.red;
        
		
		
		
		StartCoroutine(wait());
			
			
			
		
       
    }

 


    IEnumerator Task5()
    {
        //this takes me to the edge of the screen
        float xpos = 0f;
        while(xpos < 10f)
        {
            Debug.Log(xpos);
            enemyBox.transform.position += new Vector3(1f, 0f);
            xpos++;
            savePosition();
            yield return new WaitForSeconds(0.1f);
        }

        
        yield return null;
    }
	
	IEnumerator wait(){

		
		
			yield return new WaitForSeconds(4f);
			
			
			
			//enemyBox = Instantiate(Resources.Load<GameObject>("Prefabs/Square3"), new Vector3(-9f, -9f), Quaternion.identity);
			
			pathParent = new GameObject();

			pathParent.transform.position = new Vector3(0f, 0f);

			pathParent.name = "Path Parent";

			
			breadcrumbBox = Resources.Load <GameObject>("Prefabs/Square");

			enemyBox.GetComponent<SpriteRenderer>().color = Color.red;

		   
			//enemyBox.name = "Enemy player box";

			pastPositions = new List<positionRecord2>();


			drawTail(Aisnakelength);
			
	
		
		
		
		
	
	}
    

    // Update is called once per frame

    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord2 p in pastPositions)
        {
            
            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }


    void savePosition()
    {
        positionRecord2 currentBoxPos = new positionRecord2();

        currentBoxPos.Position = enemyBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(enemyBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, enemyBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        //Debug.Log("Have made this many moves: " + pastPositions.Count);
       
    }


    void cleanList()
    {
        for(int counter = pastPositions.Count - 1 ; counter > pastPositions.Count;counter--)
        {
            pastPositions[counter] = null;
        }
    }

    
     void changeSnakeColor(int length,Color color)
    {
        int tailStartIndex = pastPositions.Count - 1;
        int tailEndIndex = tailStartIndex - length;
        
        AisnakeColor = color;

        for (int snakeblocks = tailStartIndex;snakeblocks>tailEndIndex;snakeblocks--)
        {
        
            pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void drawTail(int length)
    {
        clearTail();

        if (pastPositions.Count>length)
        {
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;
          

            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex;snakeblocks>tailEndIndex;snakeblocks--)
            {


                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                
				
					
				pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = AisnakeColor;
				
					
				
				}

            }

         

        if (firstrun)
        {
            
            //I don't have enough positions in the past positions list
            for(int count =length;count>0;count--)
            {
                positionRecord2 fakeBoxPos = new positionRecord2();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);

                pastPositions.Add(fakeBoxPos);

                 


            }
            firstrun = false;
            drawTail(length);
            //Debug.Log("Not long enough yet");
        }

    }


    //if hit tail returns true, the snake has hit its tail
     bool hitTail(Vector3 headPosition, int length)
    {
        int tailStartIndex = pastPositions.Count - 1;
        int tailEndIndex = tailStartIndex - length;

        //I am checking all the positions in the tail of the snake
        for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
        {
            if ((headPosition == pastPositions[snakeblocks].Position) && (pastPositions[snakeblocks].BreadcrumbBox != null))
            {
              
                return true;
				Debug.Log("Hit Tail");
            }
			
        }

		
       return false;

    }



    void clearTail()
    {
        cleanList();
        foreach (positionRecord2 p in pastPositions)
        {
           // Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }


  


    void Update()
    {


    }
	
	
}
