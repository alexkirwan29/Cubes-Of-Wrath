using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    #region Instance Stuff
    private static GameCamera sceneInstance;
    public static GameCamera instance
    {
        get
        {
            if(sceneInstance == null)
            {
                sceneInstance = FindObjectOfType<GameCamera>();
                if(sceneInstance == null)
                {
                    Debug.LogWarning("Whoa!, you need at least one GameCamera script in your scene. did you add a camera prefab?");
                }
            }
            return sceneInstance;
        }
    }
    #endregion

    public float distance = 5f;
    public float height = 0.5f;
    public Vector3 angle = new Vector3(0, 0, 30);

    #region camera roll stuff
    public float cameraRollAmmount = 5f;
    public float cameraRollDamp = 0.5f;

    float cameraRollVelocity;
    float cameraRoll;
    #endregion

    Vector3 targetPos;
    Vector3 targetForward;
    //PlayerMovement target;
    Transform target = null;

    void LateUpdate()
    {
        if (target != null)
        {
            targetForward = target.transform.forward;
            targetPos = target.transform.position;
        }
        transform.position = targetPos + targetForward * distance + Vector3.up * height;
        transform.LookAt(targetPos);
        #region camera roll stuff
        /*cameraRoll = Mathf.SmoothDamp(cameraRoll, target.horizontalVelocity * cameraRollAmmount, ref cameraRollVelocity, cameraRollDamp * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((targetPos - transform.position).normalized, Quaternion.Euler(0, 0, cameraRoll) * Vector3.up);
        transform.rotation = Quaternion.Euler(angle.x,angle.y,angle.z + cameraRoll);*/
        #endregion
    }

    /*public void SetTarget(PlayerMovement newTarget)
    {        
        target = newTarget;
        targetPos = target.transform.position;
        targetForward = target.transform.forward;
        Debug.Log(string.Format("Game Camera's target has been changed to <i>{0}</i>",newTarget.name));
    }*/
    public Transform Target
    {
        get { return target; }
        set
        {
            target = value;
            targetPos = target.position;
            targetForward = target.forward;
            Debug.Log(string.Format("Game Camera's target has been changed to <i>{0}</i>", value.name));
        }
    }
}