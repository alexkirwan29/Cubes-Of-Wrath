using UnityEngine;
using System.Collections;

public class objMove : MonoBehaviour {

    public float distance;
    public float speed = 1;
    public bool forward = true;

    [Range(0, 1)]
    public float value;

    public Vector3 start;
    public Vector3 end;

    void Start()
    {
       

        Ray negxRay = new Ray(transform.position, Vector3.left);
        Ray posxRay = new Ray(transform.position, Vector3.right);

        Debug.DrawRay(negxRay.origin, negxRay.direction, Color.green, 10);
        Debug.DrawRay(posxRay.origin, posxRay.direction, Color.red, 10);

        RaycastHit neghit;
        if (Physics.Raycast(negxRay, out neghit, 100))
        {
            start = neghit.point;
            
        }

        else
                {
            start = transform.position;
        }

        RaycastHit poshit;
        if (Physics.Raycast(posxRay, out poshit, 100))
        {
            end = poshit.point;
            
        }

        else
        {
            end = transform.position;
        }

        distance = Vector3.Distance(start, end);
    } 



    void Update()
    {
        transform.position = Vector3.Lerp(start, end, value);
        if (forward)
        {
            value += Time.deltaTime * speed / distance;
        }

        else
        {
            value -= Time.deltaTime * speed / distance;
        }
        if (value > 1)
        {
            forward = false;
        }
        else if (value < 0)
            {
            forward = true;
        }
    }


    }