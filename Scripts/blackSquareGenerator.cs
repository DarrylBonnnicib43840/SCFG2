using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


class positionRecord
{
    //the place where I've been
    Vector3 position;
    //at which point was I there?
    int positionOrder;

    GameObject breadcrumbBox;

    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

public class blackSquareGenerator : MonoBehaviour
{

    GameObject playerBox,breadcrumbBox,pathParent;

    List<positionRecord> pastPositions;

    int positionorder = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerBox = Instantiate(Resources.Load<GameObject>("Prefabs/Square"), new Vector3(0f, 0f), Quaternion.identity);

        
        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";

        
        breadcrumbBox = Resources.Load <GameObject>("Prefabs/Square");

        playerBox.GetComponent<SpriteRenderer>().color = Color.black;

        //move the box with the arrow keys
        playerBox.AddComponent<crossController>();

        playerBox.name = "Black player box";

        pastPositions = new List<positionRecord>();
    }


    // Update is called once per frame

    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord p in pastPositions)
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
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = playerBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(playerBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, playerBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("a key was pressed "+Time.time);
            savePosition();
           
        }


    }
}
