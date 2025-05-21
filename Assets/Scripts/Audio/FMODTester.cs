using UnityEngine;
using FMODUnity;

public class FMODTester : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Probando sonido...");
        //RuntimeManager.PlayOneShot("event:/steps");
        RuntimeManager.PlayOneShot("event:/life");
    }
}
