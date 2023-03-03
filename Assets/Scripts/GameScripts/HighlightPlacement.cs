using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HighlightPlacement : MonoBehaviour
{
    public GameObject marker;
    public Transform markerHolder;
    //public GameObject highlightedCard;
    public Transform descriptionPanel;
    bool onTile = false;
    bool onTower = false;
    public GameObject range;
    bool checkRayAgain = false;

    public bool isHeroTowerBought = false;
    public bool isDraggingTower = false;

    public string currentlyHoveredTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits = Physics.RaycastAll(ray);
        //if (Physics.Raycast(ray, out RaycastHit tileHit, LayerMask.GetMask("Placeable Tile")))
        //{
        //    //Debug.Log(tileHit.point);
        //    marker.transform.position = tileHit.point;
        //    marker.transform.position = new Vector3((int)marker.transform.position.x + 0.5f, marker.transform.position.y, (int)marker.transform.position.z - 0.5f);
        //    //if(tileHit.transform.parent.GetComponent<HasTower>().isBaseOnTile)
        //    //{
        //    //    if (!onTower)
        //    //    {
        //    //        tileHit.transform.GetChild(0).GetChild(0).GetComponent<TowerStats>().SetDescription(descriptionPanel);
        //    //        range.transform.position = tileHit.transform.GetChild(0).GetChild(0).position;
        //    //        range.transform.localScale = new Vector3(tileHit.transform.GetChild(0).GetChild(0).GetComponent<TowerStats>().range * 2, 0.01f, tileHit.transform.GetChild(0).GetChild(0).GetComponent<TowerStats>().range * 2);
        //    //        onTower = true;
        //    //    }
        //    //}
        //    onTile = true;
        //}
        //else if (onTile)
        //{
        //    marker.transform.position = markerHolder.position;
        //    NullDescription(descriptionPanel);
        //    onTile = false;
        //}

        if (Physics.Raycast(ray, out RaycastHit tileHit))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);
            Debug.Log(tileHit.transform.name);

            if (tileHit.collider.CompareTag("Tower") && currentlyHoveredTower != tileHit.transform.name)
            {
                if (descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell == false)
                {
                    NullDescription();
                }
            }

            if (tileHit.collider.CompareTag("Placeable Tile"))
            {
                Vector3 vec = new Vector3(tileHit.transform.position.x, tileHit.transform.position.y + 0.15f, tileHit.transform.position.z);
                //Vector3 vec = tileHit.transform.position;
                marker.transform.position = vec;
                if (descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell == false)
                {
                    NullDescription();
                }
                ResetRange();
            }
            else if (tileHit.collider.CompareTag("Tower"))
            {
                SetDescription(tileHit);
                range.transform.position = tileHit.transform.GetChild(0).position;
                range.transform.localScale = new Vector3(tileHit.transform.GetChild(0).GetComponent<TowerStats>().range, 1f, tileHit.transform.GetChild(0).GetComponent<TowerStats>().range);
                currentlyHoveredTower = tileHit.transform.name;
            }
        }
        else
        {
            ResetMarker();
            ResetRange();
        }

        //foreach (var hit in hits)
        //{
        //    if (hit.collider.CompareTag("Placeable Tile"))
        //    {
        //        Vector3 vec = new Vector3((int)hit.point.x + 0.5f, hit.point.y, (int)hit.point.z - 0.5f);
        //        marker.transform.position = vec;
        //        if(hits.Length < 2 && onTower)
        //        {
        //            ResetRange();
        //            if(descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell == false)
        //            {
        //                NullDescription();
        //            }
        //        }
        //        onTile = true;
        //    }
        //    if(hit.collider.CompareTag("Tower"))
        //    {
        //        if (!onTower || checkRayAgain)
        //        {

        //            SetDescription(hit);
        //            range.transform.position = hit.transform.GetChild(0).position;
        //            range.transform.localScale = new Vector3(hit.transform.GetChild(0).GetComponent<TowerStats>().range, 1f, hit.transform.GetChild(0).GetComponent<TowerStats>().range);
        //            onTower = true;
        //            checkRayAgain = false;
        //        }
        //    }
        //}

        //if (hits.Length == 0 && onTile)
        //{
        //    ResetMarker();
        //    if (descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell == false)
        //    {
        //        NullDescription();
        //    }
        //    ResetRange();
        //    onTile = false;
        //}

        //if(descriptionPanel.GetComponent<DescriptionLogic>().pointerOnSpell == false)
        //{
        //    checkRayAgain = true;
        //}


        //else if(onTower)
        //{
        //    range.transform.position = Vector3.zero;
        //    onTower = false;
        //}
        //if(Physics.Raycast(ray, out RaycastHit cardHit, LayerMask.GetMask("Card")))
        //{
        //    Debug.Log("Hit a card!");

        //}
        //else
        //{
        //    if(highlightedCard)
        //    {
        //        highlightedCard.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        //        highlightedCard = null;
        //    }
        //}
    }

    //void CardHighlight(GameObject Card)
    //{
    //    highlightedCard = Card;
    //    Card.transform.GetChild(0).GetComponent<Image>().color = Color.black;
    //}


    public void NullDescription()
    {
        //if (descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        //{
            //descriptionPanel.GetComponent<Animator>().Play("CloseDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().NullDescription();
            //descriptionPanel.GetComponent<DescriptionLogic>().isVisible = false;
        //}
    }

    public void SetDescription(RaycastHit hit)
    {
        //if (!descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        //{
            //descriptionPanel.GetComponent<Animator>().Play("OpenDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().SetDescription(hit.transform.GetChild(0).gameObject);
            //descriptionPanel.GetComponent<DescriptionLogic>().isVisible = true;
        //}
    }

    public void ResetRange()
    {
        range.transform.localPosition = Vector3.zero;
        onTower = false;
    }

    public void ResetMarker()
    {
        marker.transform.localPosition = markerHolder.position;
    }


}
