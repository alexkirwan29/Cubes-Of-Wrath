//hello this is some extra lines that i added
//just because tom wants me to
//and also hi
using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    #region Instace Stuff
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
                    Debug.LogWarning("Woah!, you need at least one GameCamera script in your scene. did you add a camera prefab?");
                }
            }
            return sceneInstance;
        }
    }
    #endregion

    public float distance = 5f;
    public float height = 0.5f;
    public float angle = 190f;
    public float cameraRollAmmount = 5f;
    public float cameraRollDamp = 0.5f;

    float cameraRollVelocity;
    float cameraRoll;

    Vector3 wantedPos;
    Transform target;

    Vector3 lastWantedPos;

    void LateUpdate()
    {
        lastWantedPos = wantedPos;
        if (target != null)
            wantedPos = target.position;

        wantedPos.y = height;

        transform.position = wantedPos + Quaternion.Euler(angle, 0, 0) * Vector3.forward * distance;

        float horizontalVelocity = wantedPos.x - lastWantedPos.x;

        cameraRoll = Mathf.SmoothDamp(cameraRoll, horizontalVelocity * cameraRollAmmount, ref cameraRollVelocity, cameraRollDamp * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation((wantedPos - transform.position).normalized, Quaternion.Euler(0, 0, cameraRoll) * Vector3.up); ;
    }

    public void SetTarget(Transform newTarget)
    {        
        target = newTarget;
        wantedPos = target.position;
        wantedPos.y = height;
        lastWantedPos = wantedPos;
        Debug.Log(string.Format("Game Camera's target has been changed to <i>{0}</i>",newTarget.name));
    }
}