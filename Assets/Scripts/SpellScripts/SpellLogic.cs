using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellLogic : MonoBehaviour
{
    [SerializeField]
    public SpellStatistics spellStatistics;

    [Space]

    [SerializeField]
    private Transform rangeIndicator;
    [SerializeField]
    private Transform rangeIndicatorHolder;
    [SerializeField]
    private List<GameObject> avaibleResources;
    [SerializeField]
    private Transform descriptionPanel;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject gameController;

    private float range;
    private float damage;

    private List<Transform> enemiesInRange = new List<Transform>();

    private bool draggingSpell = false;

    private GraphicRaycaster canvasRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        range = spellStatistics.range;
        damage = spellStatistics.damage;

        canvas = GameObject.Find("UI");
        gameController = GameObject.Find("GameControllerObject");
        descriptionPanel = GameObject.Find("Description Panel").transform;
        canvasRaycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        rangeIndicator = GameObject.Find("SpellRange").transform;
        rangeIndicatorHolder = GameObject.Find("RangeHolder").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(draggingSpell)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (Physics.Raycast(ray, out RaycastHit tileHit))
            {
                rangeIndicator.position = new Vector3(tileHit.point.x - 0.9f, 1f, tileHit.point.z + 1.2f);
            }
            else
            {
                rangeIndicator.position = new Vector3(-0.9f, 1f, 1.2f);
            }
        }
    }

    public void DraggingCard()
    {
        draggingSpell = true;
        rangeIndicator.transform.localScale = new Vector3(range, 0.1f, range);
        this.GetComponentInParent<Image>().enabled = true;
    }

    public void ReleaseCard()
    {
        if (CheckIfCardCanBeReleased())
        {
            if(spellStatistics.spellType == SpellType.AOEInstant)
            {
                enemiesInRange = rangeIndicator.GetComponent<SpellRelease>().enemiesInRange;
                foreach (Transform enemy in enemiesInRange)
                {
                    if (enemy != null)
                    {
                        Debug.Log($"Damaged this enemy: {enemy}");
                        enemy.GetComponent<FollowNavMesh>().TakeDamage(damage);
                    }
                }
                rangeIndicator.position = rangeIndicatorHolder.position;
            }

            if(spellStatistics.spellType == SpellType.ResourceGain)
            {
                gameController.GetComponent<BaseController>().AddResources(spellStatistics.resourceGain);
            }


            draggingSpell = false;
            RemoveCard();
            this.GetComponentInParent<Image>().enabled = false; //weird way to disable card
        }
    }

    private bool CheckIfCardCanBeReleased()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        canvasRaycaster.Raycast(pointerEventData, results);


        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Card Space"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    void RemoveCard()
    {
        Destroy(this.GetComponent<Button>());
        Destroy(this.GetComponent<EventTrigger>());
        Destroy(this.gameObject, 1.0f);
    }

    //void PayMana()
    //{
    //    int moneyNeeded = int.Parse(avaibleResources[0].GetComponent<Text>().text) - this.GetComponent<TowerStats>().price;
    //    avaibleResources[0].GetComponent<Text>().text = moneyNeeded.ToString();
    //    avaibleResources[1].GetComponent<Text>().text = moneyNeeded.ToString();
    //}

    public void CheckIfSpellCanBePlayed()
    {

    }

    public void OnCardHover()
    {
        SetDescription();
        descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell = true;
    }

    public void OnCardLeave()
    {
        NullDescription();
        descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell = false;
    }

    public void NullDescription()
    {
        if (descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        {
           // descriptionPanel.GetComponent<Animator>().Play("CloseDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().NullDescription(this.transform.gameObject);
           // descriptionPanel.GetComponent<DescriptionLogic>().isVisible = false;
        }
    }

    public void SetDescription()
    {
        if (!descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        {
            //descriptionPanel.GetComponent<Animator>().Play("OpenDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().SetDescription(this.transform.gameObject);
           // descriptionPanel.GetComponent<DescriptionLogic>().isVisible = true;
        }
    }
}
