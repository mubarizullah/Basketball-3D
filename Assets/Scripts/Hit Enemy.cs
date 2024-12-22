using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HitEnemy : MonoBehaviour
{
    public GameObject[] bodyParts;
    public MeshRenderer glass ;
    public MeshRenderer leftArm;
    public MeshRenderer rightArm;
    //GameObject glasses = GameObject.Find("Glasses");
public void Start()
{   
  
}
private void OnCollisionEnter(Collision collison)
{
  if (collison.gameObject.CompareTag("Enemy Body"))
  {
    GetComponent<MeshRenderer>().enabled= false;
    GetComponent<Rigidbody>().isKinematic = true;
    GetComponent<PlayerMovement>().enabled= false;
    glass.enabled =false;
    leftArm.enabled = false;
    rightArm.enabled = false;
    //glasses.GetComponent<MeshRenderer>().enabled = false;
    
    Invoke("Reload",2.0f) ; 
  }  
}

private void Reload()
{
SceneManager.LoadScene("Scene1");
}

}

