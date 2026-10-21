
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayMenu : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Image image;

    public void ChangeSprite(int i)
    {
        image.sprite = sprites[i];
    }
}