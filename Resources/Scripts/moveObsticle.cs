using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObsticle : MonoBehaviour
{
    public Transform target;

    //the code that is going to move my target.
    IEnumerator moveTarget()
    {
        //create a new list of positions
        List<Vector3> positions = new List<Vector3>();
		
		
		//list of waypoints with obsticles
        if(gameObject.tag == "Obsticle")
        {
            
            positions.Add(new Vector3(5f, 5f));

            positions.Add(new Vector3(4f, 4f));

            positions.Add(new Vector3(3f, 3f));
        }
		if(gameObject.tag == "Obsticle1")
        {
            
            positions.Add(new Vector3(-5f, 5f));

            positions.Add(new Vector3(-4f, 4f));

            positions.Add(new Vector3(-3f, 3f));
        }
		if(gameObject.tag == "Obsticle2")
        {
            
            positions.Add(new Vector3(5f, -5f));

            positions.Add(new Vector3(4f, -4f));

            positions.Add(new Vector3(3f, -3f));
        }
		if(gameObject.tag == "Obsticle3")
        {
            
            positions.Add(new Vector3(-5f, -5f));

            positions.Add(new Vector3(-4f, -4f));

            positions.Add(new Vector3(-3f, -3f));
        }

        

       
        StartCoroutine(moveTarget(target.transform, positions,true));

        yield return null;

    }
    IEnumerator moveTarget(Transform t, List<Vector3> points,bool loop)
    {
        if (loop)
        {
            
            while (true)
            {
                List<Vector3> forwardpoints = points;
                
                foreach (Vector3 position in forwardpoints)
                {
                    while (Vector3.Distance(t.position, position) > 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, position, 0.5f);
                        Debug.Log(position);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                //reverse the points supplied here
                forwardpoints.Reverse();
                yield return null;
                
            }
        } else
        {
            foreach (Vector3 position in points)
            {
                while (Vector3.Distance(t.position, position) > 0.5f)
                {
                    t.position = Vector3.MoveTowards(t.position, position, 1f);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }

    IEnumerator updateGraph()
    {
        while (true)
        {
            AstarPath.active.Scan();

            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //move the green box. runs indefinetly
        StartCoroutine(moveTarget());

        
        StartCoroutine(updateGraph());
    }
}