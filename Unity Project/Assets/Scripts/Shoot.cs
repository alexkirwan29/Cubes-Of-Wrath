using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public Transform target;

    void Update()
    {
      transform.LookAt(target);
    }
}