using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleGamePanel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] public Button playButton;


    public void SetPanel(Sprite _sprite, string _title, string _subtitle)
    {
        image.sprite = _sprite;
        title.text = _title;
        subtitle.text = _subtitle;
    }

}
