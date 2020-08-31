using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasScript : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}
