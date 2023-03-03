using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionLogic : MonoBehaviour
{
    public List<Transform> children = new List<Transform>();
    public bool pointerOnSpell = false;
    public bool pointerOnTower = false;
    public GameObject hoveredGameObject = null;

    public bool isVisible = false;

    public void SetDescription(GameObject item)
    {
        if (hoveredGameObject == null)
        {
            hoveredGameObject = item;

            if (item.TryGetComponent<TowerStats>(out TowerStats towerStatComponent))
            {
                List<float> towerStats = new List<float>()
                {
                    towerStatComponent.damage,
                    towerStatComponent.range,
                    towerStatComponent.attackSpeed
                };

                //SetImage(towerStatComponent.towerSprite);
                SetName(towerStatComponent.towerName);
                SetType(towerStatComponent.towerType.ToString());
                SetDesciption(towerStatComponent.towerDescription);
                SetStats(towerStats);
                ChangeSliderState(true);
            }

            if (item.TryGetComponent<SpellStats>(out SpellStats spellStatsComponent))
            {
                List<float> spellStats = new List<float>()
                {
                    spellStatsComponent.damage,
                    spellStatsComponent.range
                };

                //SetImage(spellStatsComponent.spellSprite);
                SetName(spellStatsComponent.spellName);
                SetType(spellStatsComponent.spellType.ToString());
                SetDesciption(spellStatsComponent.spellDescription);
                SetStats(spellStats);
                ChangeSliderState(false);
            }

            isVisible = true;
        }
    }

    public void NullDescription(GameObject item)
    {
        if (hoveredGameObject == item)
        {
            //children[0].GetComponent<Image>().sprite = null;
            children[1].GetComponent<PanelManager>().NullText();
            children[2].GetComponent<PanelManager>().NullText();
            children[3].GetComponent<PanelManager>().NullText();
            children[4].GetComponent<PanelManager>().NullMultipleText();
            children[4].GetComponent<PanelManager>().NullMultipleValue();
            hoveredGameObject = null;
        }

        isVisible = false;

        //if (item.TryGetComponent<SpellStats>(out SpellStats spellStatsComponent))
        //{
        //    ChangeSliderState();
        //}
    }

    public void NullDescription()
    {
        //children[0].GetComponent<Image>().sprite = null;
        children[1].GetComponent<PanelManager>().NullText();
        children[2].GetComponent<PanelManager>().NullText();
        children[3].GetComponent<PanelManager>().NullText();
        children[4].GetComponent<PanelManager>().NullMultipleText();
        children[4].GetComponent<PanelManager>().NullMultipleValue();
        hoveredGameObject = null;

        isVisible = false;
    }

    void SetImage(Sprite itemSprite)
    {
        children[0].GetComponent<Image>().sprite = itemSprite;
    }

    void SetName(string itemName)
    {
        children[1].GetComponent<PanelManager>().SetText(itemName);
    }

    void SetType(string itemType)
    {
        children[2].GetComponent<PanelManager>().SetText(itemType);
    }

    void SetDesciption(string itemDescription)
    {
        children[3].GetComponent<PanelManager>().SetText(itemDescription);
    }

    void SetStats(List<float> itemStats)
    {
        children[4].GetComponent<PanelManager>().SetMultipleText(itemStats);
        children[4].GetComponent<PanelManager>().SetMultipleValue(itemStats);
    }

    void ChangeSliderState(bool state)
    {
        children[4].GetComponent<PanelManager>().ChangeSliderState(state);
    }
}
