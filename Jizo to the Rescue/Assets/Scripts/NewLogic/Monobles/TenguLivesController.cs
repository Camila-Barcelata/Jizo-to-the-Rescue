using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TenguLivesController : MonoBehaviour
{
    [SerializeField] List<Image> demons;
    [SerializeField] Sprite aliveSprite;
    [SerializeField] Sprite deadSprite;

    void Update()
    {
        int lives = theGAMEMANAGER.Instance.lives;
        switch(lives)
        {
            case 0:
                demons[0].sprite = deadSprite;
                demons[1].sprite = deadSprite;
                demons[2].sprite = deadSprite;
                break;
            case 1:
                demons[0].sprite = aliveSprite;
                demons[1].sprite = deadSprite;
                demons[2].sprite = deadSprite;
                break;

            case 2:
                demons[0].sprite = aliveSprite;
                demons[1].sprite = aliveSprite;
                demons[2].sprite = deadSprite;
                break;

            case 3:
                demons[0].sprite = aliveSprite;
                demons[1].sprite = aliveSprite;
                demons[2].sprite = aliveSprite;
                break;

        }
    }
}
