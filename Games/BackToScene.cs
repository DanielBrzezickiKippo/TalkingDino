using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    public void LoadScene()
    {
        loadingUI.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
