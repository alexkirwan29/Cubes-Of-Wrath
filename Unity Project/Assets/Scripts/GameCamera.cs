﻿using UnityEngine;
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

    #region camera roll stuff
    /*public float cameraRollAmmount = 5f;
    public float cameraRollDamp = 0.5f;

    float cameraRollVelocity;
    float cameraRoll;*/
    #endregion

    Vector3 targetPos;
    Vector3 targetForward;
    Transform target;

    Vector3 lastWantedPos;

    void LateUpdate()
    {
        lastWantedPos = targetPos;
        if (target != null)
        {
            targetForward = target.forward;
            targetPos = target.position;
        }
        transform.position = targetPos + targetForward * distance + Vector3.up * height;
        transform.LookAt(targetPos);
        #region camera roll stuff
        /*float horizontalVelocity = targetPos.x - lastWantedPos.x;
        cameraRoll = Mathf.SmoothDamp(cameraRoll, horizontalVelocity * cameraRollAmmount, ref cameraRollVelocity, cameraRollDamp * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((targetPos - transform.position).normalized, Quaternion.Euler(0, 0, cameraRoll) * Vector3.up);*/
        #endregion
    }

    public void SetTarget(Transform newTarget)
    {        
        target = newTarget;
        targetPos = target.position;
        targetForward = target.forward;
        lastWantedPos = targetPos;
        Debug.Log(string.Format("Game Camera's target has been changed to <i>{0}</i>",newTarget.name));
    }
}