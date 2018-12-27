using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup dedicationGroup;

    [SerializeField]
    private CanvasGroup openingScreen;

    [SerializeField]
    private CanvasGroup mainMenu;

    [SerializeField]
    private CanvasGroup legal;

    [SerializeField]
    private MeshRenderer anhk;

    [SerializeField]
    private ParticleSystemRenderer fog;

    private bool timerElapsed = false;

    private Coroutine fadeOutRoutine;

    private Coroutine fadeInRoutine;

    private Coroutine newGameStart;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {     
        newGameStart = StartCoroutine(FreshGame());
    }    
	
	// Update is called once per frame
	void Update ()
    {   
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fadeOutRoutine = StartCoroutine(FadeOut(openingScreen));
            fadeInRoutine = StartCoroutine(FadeIn(mainMenu));
        }
	}

    public void ClickButton()
    {
        Debug.Log("Button clicked");
    }

    public void OpenElement(CanvasGroup canvasGroup)
    {
        fadeOutRoutine = StartCoroutine(FadeOut(mainMenu));
        mainMenu.blocksRaycasts = false;
        fadeInRoutine = StartCoroutine(FadeIn(canvasGroup));
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseElement(CanvasGroup canvasGroup)
    {
        fadeOutRoutine = StartCoroutine(FadeOut(canvasGroup));
        canvasGroup.blocksRaycasts = false;
        fadeInRoutine = StartCoroutine(FadeIn(mainMenu));
        mainMenu.blocksRaycasts = true;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, progress);

            progress += rate * Time.deltaTime;

            yield return null;            
        }

        canvasGroup.alpha = 0;
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FreshGame()
    {
        yield return new WaitForSeconds(2);
        fadeInRoutine = StartCoroutine(FadeIn(dedicationGroup));
        yield return new WaitForSeconds(10);
        fadeInRoutine = StartCoroutine(FadeOut(dedicationGroup));        
        fadeInRoutine = StartCoroutine(FadeIn(openingScreen));

    }
}
