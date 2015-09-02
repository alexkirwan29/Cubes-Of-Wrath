using UnityEngine;
using System.Collections;
using Cow;

public class PathFollower : MonoBehaviour                                          //                     === Comments ===
{
    public float heighOffset = 1f;
    public float speed = 1f;
    public TileCoord[] path = {new TileCoord(0,1),new TileCoord(0,-1)};
    public bool loop = true;
    public bool forward = true;

    bool move = true;

    Vector3 startPos;
    Vector3 endPos;
    float distance;

    int currPos;

    float t = 0;
    void Start()                                                                   //                   === Start Method ===
    {
        startPos = path[0].ToVector3(heighOffset);
        endPos = startPos;
        NextPos();
    }
    void Update()                                                                  //                   === Update Method ===
    {
        if (!move)
            return;

        t += Time.deltaTime * speed / distance;
        transform.position = Vector3.Lerp(startPos, endPos, t);

        if (t > 1)
        {
            NextPos();
            t = 0;
        }
    }
    public void NextPos()                                                          //                   === NextPos Method ===
    {
        if (path == null || path.Length == 0)                                       // Don't move if we don't have a path to follow.
        {
            move = false;                                                          // Set the move bool to false so the rest of the script
            return;                                                                // knows what's up. Then exit out of the update loop.
        }

        #region Loop Code
        if (loop)                                                                  // Has this script been told to loop?
        {
            if (forward)                                                           // Are we moving forward?
            {
                currPos++;                                                         // Increment the currPos.
                if (currPos > path.Length - 1)                                     // If the currPos is to big for the path array then make
                    currPos = 0;                                                   // it zero because we have been told to loop.
            }
            else                                                                   // If we are not moving forward we are moving backwards.
            {
                currPos--;                                                         // Decrease the currPos because we are moving backwards.
                if (currPos < 0)                                                   // If the currPos is to small for the path array set it to
                    currPos = path.Length - 1;                                     // the last point in the path because we are looping in reverse.
            }
        }
        #endregion
        #region Repeat Code
        else                                                                       // If we are not looping we have to be repeating.
        {
            if (forward)                                                           // Are we moving forward?
            {
                currPos++;                                                         // Increment the currPos.
                if (currPos >= path.Length - 1)                                    // If the currPos is on the second last point tell the script to
                    forward = false;                                               // move backwards.
            }
            else                                                                   // If we are not moving forward we have to be moving backwards.
            {
                currPos--;                                                         // Decrease the currCos because we are moving backwards.
                if (currPos <= 0)                                                  // If we are on the second point then tell the script to move
                    forward = true;                                                // backwards.
            }
        }
        #endregion
        startPos = endPos;                                                         // Set the starting position for the next path segment to the
        Debug.Log(currPos);
        endPos = path[currPos].ToVector3(heighOffset);                             // current end position before we change it. Next we set the end
                                                                                   // position to the new end position.
        distance = Vector3.Distance(startPos, endPos);                             // Find the distance between the two points.
    }

    void OnDrawGizmosEnabled()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < path.Length; i++)
        {
            if(i != 0)
                Gizmos.DrawLine(path[i-1], path[i]);
        }
    }
}