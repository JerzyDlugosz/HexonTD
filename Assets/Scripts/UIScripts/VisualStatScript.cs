using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualStatScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private PlayerData playerData;
    private List<float> mainStatValues;

    public List<float> BasicTowerModifiers = new List<float>(); //DamageAdditive, DamageMultiplicative, RangeAdditive, ..., AttackSpeedAdditive, ...
    public List<float> TeslaTowerModifiers = new List<float>();
    public List<float> RailgunTowerModifiers = new List<float>();
    public List<float> MissileTowerModifiers = new List<float>();
    public List<float> HeroTowerModifiers = new List<float>();

    public void OnClick()
    {
        if(animator.GetBool("Show"))
        {
            animator.SetBool("Show", false);
            animator.SetBool("Hide", true);
        }
        else
        {
            animator.SetBool("Hide", false);
            animator.SetBool("Show", true);
        }
    }

    private void Start()
    {
        playerData = GameStateManager.instance.GetComponent<PlayerData>();
        mainStatValues = new List<float>()
        {
            playerData.BasicTowerModifiers[0],
            playerData.BasicTowerModifiers[2],
            playerData.BasicTowerModifiers[4],

            playerData.TeslaTowerModifiers[0],
            playerData.TeslaTowerModifiers[2],
            playerData.TeslaTowerModifiers[4],

            playerData.RailgunTowerModifiers[0],
            playerData.RailgunTowerModifiers[2],
            playerData.RailgunTowerModifiers[4],

            playerData.MissileTowerModifiers[0],
            playerData.MissileTowerModifiers[2],
            playerData.MissileTowerModifiers[4],

            playerData.HeroTowerModifiers[0],
            playerData.HeroTowerModifiers[2],
            playerData.HeroTowerModifiers[4],
        };
        SetupHexStats();
    }

    private void SetupHexStats()
    {
        int j = 0;
        foreach(Transform child in transform)
        {
            VisualStatData visualStatData = child.GetComponent<VisualStatData>();
            List<float> hexStats = new List<float>();
            for (int i = j; i < j + 3; i++) 
            {
                hexStats.Add(mainStatValues[i]);
            }
            SetData(visualStatData, hexStats);
            j += 3;
        }
    }

    private void SetData(VisualStatData visualStatData, List<float> statValues)
    {
        for (int i = 0; i < statValues[0]; i++)
        {
            visualStatData.dmgStatFilling[i].enabled = true;
        }
        for (int i = 0; i < statValues[1]; i++)
        {
            visualStatData.rngStatFilling[i].enabled = true;
        }
        for (int i = 0; i < statValues[2]; i++)
        {
            visualStatData.aspStatFilling[i].enabled = true;
        }
    }
}
