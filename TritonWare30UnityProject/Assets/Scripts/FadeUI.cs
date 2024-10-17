using DG.Tweening;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 0.25f;
    [SerializeField] private FadeState fadeState;

    private bool fadingIn;
    private bool fadingOut;
    
    public void TryFadeIn()
    {
        if (fadingIn || fadeState == FadeState.IN) return;
        fadingIn = true;
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeTime).OnComplete(() =>
        {
            fadeState = FadeState.IN;
            fadingIn = false;
        });
    }

    public void TryFadeOut()
    {
        if (fadingOut || fadeState == FadeState.OUT) return;
        fadingOut = true;
        canvasGroup.DOFade(0f, fadeTime).OnComplete(() =>
        {
            fadeState = FadeState.OUT;
            fadingOut = false;
            gameObject.SetActive(false);
        });
    }
    private enum FadeState { IN, OUT}
}