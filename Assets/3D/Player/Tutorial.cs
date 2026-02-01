using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TutorialText
{
    ZOOM,
    WASD,
    RESET,
    INTERACT,
    MASK1,
    MASK2,
    LOOK
}

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;

    public TMP_Text tutTMP;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private Queue<TutorialText> msgQueue = new();
    private bool isProcessing = false;

    public void Show(TutorialText tut)
    {
        // Neue Nachricht hinten anstellen
        msgQueue.Enqueue(tut);

        // Wenn der Prozess noch nicht läuft, starte ihn
        if (!isProcessing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isProcessing = true;

        while (msgQueue.Count > 0)
        {
            // Die nächste Nachricht aus der Schlange holen
            TutorialText nextTut = msgQueue.Dequeue();

            // Text setzen basierend auf Enum
            tutTMP.text = GetTextForEnum(nextTut);

            // Den eigentlichen Render-Vorgang (Fade In -> Wait -> Fade Out) abwarten
            yield return StartCoroutine(RenderTutRoutine());
        }

        isProcessing = false;
    }

    private string GetTextForEnum(TutorialText tut)
    {
        return tut switch
        {
            TutorialText.ZOOM => "[Press right click to zoom]",
            TutorialText.WASD => "[Press WASD to move Peter]",
            TutorialText.RESET => "[Press R to reset the level]",
            TutorialText.INTERACT => "[Press left click to pick up #####]",
            TutorialText.MASK1 => "[Press 1 to toggle Mask]",
            TutorialText.MASK2 => "[Press 2 to toggle Mask]",
            TutorialText.LOOK => "[Use your Mouse to look around]",
            _ => ""
        };
    }

    private IEnumerator RenderTutRoutine()
    {
        // Fade In
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Clamp01(elapsed));
            yield return null; // yield return null ist performanter als WaitForEndOfFrame
        }

        yield return new WaitForSeconds(5f);

        // Fade Out
        elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Clamp01(1f - elapsed));
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = tutTMP.color;
        c.a = alpha;
        tutTMP.color = c;
    }
}
