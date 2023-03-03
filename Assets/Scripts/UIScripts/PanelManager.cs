using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

    public void SetText(string text)
    {
        if (this.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI proComp))
        {
            proComp.text = text;
        }
        if (this.transform.GetChild(0).TryGetComponent<Text>(out Text textComp))
        {
            textComp.text = text;
        }
    }

    public void NullText()
    {
        if (this.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI proComp))
        {
            proComp.text = null;
        }
        if (this.transform.GetChild(0).TryGetComponent<Text>(out Text textComp))
        {
            textComp.text = null;
        }
    }

    public void SetMultipleText(List<float> texts)
    {
        for (int i = 0; i < texts.Count; i++)
        {
            if (this.transform.GetChild(i).GetChild(0).TryGetComponent(out TextMeshProUGUI proComp))
            {
                proComp.text = texts[i].ToString();
            }
            if (this.transform.GetChild(i).GetChild(0).TryGetComponent<Text>(out Text textComp))
            {
                textComp.text = texts[i].ToString();
            }
        }
    }

    public void SetMultipleValue(List<float> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if (this.transform.GetChild(i).GetChild(1).TryGetComponent<Slider>(out Slider comp))
            {
                comp.value = values[i];
            }
        }
    }

    public void NullMultipleText()
    {
        foreach(Transform child in this.transform)
        {
            if (child.GetChild(0).TryGetComponent(out TextMeshProUGUI proComp))
            {
                proComp.text = null;
            }
            if (child.GetChild(0).TryGetComponent<Text>(out Text textComp))
            {
                textComp.text = null;
            }
        }
    }

    public void NullMultipleValue()
    {
        foreach (Transform child in this.transform)
        {
            if (child.GetChild(1).TryGetComponent<Slider>(out Slider comp))
            {
                comp.value = 0;
            }
        }
    }

    public void ChangeSliderState(bool state)
    {

        this.transform.GetChild(2).gameObject.SetActive(state);
        
    }
}
