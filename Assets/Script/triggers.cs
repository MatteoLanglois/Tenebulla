using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class triggers : MonoBehaviour
{
    public string TagToDetect;
    public UnityEvent OnEnter;
    public float OnEnterDelay;
    public UnityEvent OnExit;
    public float OnExitDelay;
    public UnityEvent OnStay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagToDetect)
        {
           
            //OnEnter.Invoke();
           Invoke("OnEnterLaunch", OnEnterDelay);
        }
    }  
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == TagToDetect)
        {
            //OnExit.Invoke();
            Invoke("OnExitLaunch", OnExitDelay);
        }
    }
    
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == TagToDetect)
        {
            OnStay.Invoke();
        }
    }

    void OnEnterLaunch()
    {
        OnEnter.Invoke();
    }

    void OnExitLaunch()
    {
        OnExit.Invoke();
    }
    
}
