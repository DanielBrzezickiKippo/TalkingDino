using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Game
{
    public Sprite sprite;
    public string title;
    public string subtitle;
    public int scene;
}

public class GamesManager : MonoBehaviour
{
    [SerializeField] private List<Game> games;
    [SerializeField] private Transform panelSpawner;
    [SerializeField] private GameObject gamePanelPrefab;

    private void Start()
    {
        CreatePanels();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        
    }


    public void CreatePanels()
    {
        foreach(Game g in games)
        {
            GameObject game = Instantiate(gamePanelPrefab, panelSpawner);
            SingleGamePanel sgp = game.GetComponent<SingleGamePanel>();
            sgp.SetPanel(g.sprite, g.title, g.subtitle);
            sgp.playButton.onClick.RemoveAllListeners();
            sgp.playButton.onClick.AddListener(() => { LoadScene(g.scene); });
        }
    }

}
