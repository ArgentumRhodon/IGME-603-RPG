using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float changetime;
    public string sceneName;
    
    void Update()
    {
        changetime-=Time.deltaTime;
        if(changetime<=0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
