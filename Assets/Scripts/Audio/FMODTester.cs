using UnityEngine;
using FMODUnity;

public class FMODTester : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Probando sonido...");
        //RuntimeManager.PlayOneShot("event:/sfx-steps");
        //RuntimeManager.PlayOneShot("event:/sfx-lowLife");
        RuntimeManager.PlayOneShot("event:/music-scene_tutorial");
    }
}
