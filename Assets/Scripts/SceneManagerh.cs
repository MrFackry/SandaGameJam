using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManagerh : MonoBehaviour
{
    public void CambiarEscena(string SampleScene)
    {
        SceneManager.LoadScene(1);
    }

    public void CambiarEscena2(string Main)
    {
        SceneManager.LoadScene(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
