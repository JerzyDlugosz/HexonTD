using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UpdateCardInfo : MonoBehaviour
{
    [SerializeField]
    private SpellLogic spellLogic;

    [SerializeField]
    private TextMeshProUGUI spellNameUI;
    [SerializeField]
    private TextMeshProUGUI spellDescriptionUI;
    [SerializeField]
    private TextMeshProUGUI spellTypeUI;
    [SerializeField]
    private Image spellArtUI;
    [SerializeField]
    private TextMeshProUGUI damageUI;
    [SerializeField]
    private TextMeshProUGUI lingerTimeUI;
    [SerializeField]
    private TextMeshProUGUI rangeUI;


    private void Start()
    {
        UpdateCard();
    }

    private void UpdateCard()
    {
        SpellStatistics spellStatistics = spellLogic.spellStatistics;

        //if (spellStatistics.spellType == SpellType.AOEInstant)
        //{
        //    spellNameUI.text = spellStatistics.spellName;
        //    spellDescriptionUI.text = spellStatistics.spellDescription;
        //    spellTypeUI.text = spellStatistics.spellType.ToString();
        //    damageUI.text = spellStatistics.damage.ToString();
        //    lingerTimeUI.text = spellStatistics.lingerTime.ToString();
        //    rangeUI.text = spellStatistics.range.ToString();
        //    spellArtUI.sprite = spellStatistics.spellSprite;
        //}

        spellNameUI.text = spellStatistics.spellName;
        spellDescriptionUI.text = spellStatistics.spellDescription;
        spellTypeUI.text = spellStatistics.spellType.ToString();
        spellArtUI.sprite = spellStatistics.spellSprite;
        if (spellStatistics.damage == 0)
        {
            Destroy(damageUI.gameObject);
        }
        else
        {
            damageUI.text = spellStatistics.damage.ToString();
        }

        //if(spellStatistics.lingerTime == 0)
        //{
        //    Destroy(lingerTimeUI.gameObject);
        //}
        //else
        //{
        //    lingerTimeUI.text = spellStatistics.lingerTime.ToString();
        //}

        if (spellStatistics.range == 0)
        {
            Destroy(rangeUI.gameObject);
        }
        else
        {
            rangeUI.text = spellStatistics.range.ToString();
        }
    }
}
