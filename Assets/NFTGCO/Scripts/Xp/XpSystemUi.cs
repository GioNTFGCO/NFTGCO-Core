using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpSystemUi : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup afkAlertPanel;
    [SerializeField] private Text afkText;

    public CanvasGroup AFKAlertPanel => afkAlertPanel;

    public void AfkAlert(int afkMinutes, bool forceCloseAfkAlert)
    {
        if (afkMinutes == 1)
            afkText.text = "You have been inactive for " + afkMinutes + " minute!";
        else
            afkText.text = "You have been inactive for " + afkMinutes + " minutes!";
        if (afkAlertPanel)
        {
            StartCoroutine(FadeAfkAlert(forceCloseAfkAlert));
        }
    }
    public IEnumerator FadeAfkAlert(bool forceCloseAfkAlert)
    {
        afkAlertPanel.alpha = 0f;
        float a = 0f;

        while (a < 1f)
        {
            a += 0.05f;
            afkAlertPanel.alpha = a;
            if (forceCloseAfkAlert)
            {
                forceCloseAfkAlert = false;
                afkAlertPanel.alpha = 0f;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);
        if (forceCloseAfkAlert)
        {
            forceCloseAfkAlert = false;
            afkAlertPanel.alpha = 0f;
            yield break;
        }
        yield return new WaitForSeconds(1f);

        while (a > 0f)
        {
            a -= 0.05f;
            afkAlertPanel.alpha = a;
            if (forceCloseAfkAlert)
            {
                forceCloseAfkAlert = false;
                afkAlertPanel.alpha = 0f;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}