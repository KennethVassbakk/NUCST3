// Author: John Hauge

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private SceneTransition transition;
    public int SceneNR;

    private void Start()
    {
        transition = FindObjectOfType<SceneTransition>();
    }

    public void LoadTheScene(int SceneInt)
    {
        SceneManager.LoadScene(SceneInt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transition.DrawCurtains(this);
        }
    }

    public void CurtainClosed()
    {
        LoadTheScene(SceneNR);
    }
    
}
