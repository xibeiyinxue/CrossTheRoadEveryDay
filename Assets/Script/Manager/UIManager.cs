using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private Image m_fader = null;

    protected override void Awake()
    {
        base.Awake();
        if (m_fader != null)
        {
            m_fader.gameObject.SetActive(m_fader);
        }
    }

    public virtual void FaderOn(bool state,float duration)
    {
        if (m_fader != null)
        {
            m_fader.gameObject.SetActive(true);
            if (state)
            {
                StartCoroutine(FadeInOut.FadeImage(m_fader, duration, new Color(0, 0, 0, 1f)));
            }
            else
            {
                StartCoroutine(FadeInOut.FadeImage(m_fader, duration, new Color(0, 0, 0, 0f)));
            }
        }
    }
}
