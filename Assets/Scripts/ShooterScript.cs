using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    public void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit)){
            if(hit.transform.name == "spider Variant(Clone)" ){
                Destroy(hit.transform.gameObject);
                Instantiate(smoke, hit.point, Quaternion.identity);
            }
        }
    }
  
}
