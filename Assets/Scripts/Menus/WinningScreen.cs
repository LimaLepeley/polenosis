using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningScreen : MonoBehaviour
{
    [SerializeField]
    private Image panel;
    private float fadeDuration = 10f;
    private float t = 0f;
    private Color c;

    void Awake()
    {
        c = panel.color;
    }

    public IEnumerator LoadResultScene()
    {
        panel.gameObject.SetActive(true);
        t = 0f;

        yield return new WaitForSeconds(0.3f);

        while(t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t/fadeDuration);
            Debug.Log(c.a);
            panel.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}
