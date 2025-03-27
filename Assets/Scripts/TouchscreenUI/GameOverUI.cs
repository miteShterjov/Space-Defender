using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI killCountText;

    void Start()
    {
        killCountText.text = "Kill count: \n" + ScoreKeeper.Instance.GetScore();
    }
}
