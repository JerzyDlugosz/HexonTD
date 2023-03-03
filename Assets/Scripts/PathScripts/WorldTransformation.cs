using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class WorldTransformation : MonoBehaviour
{
    public Transform world;
    public float multiplier;
    public Transform currentTile = null;
    public Transform selectedTile = null;
    public Vector3 baseScale;
    public Button button;
    public Transform description;

    private List<TextMeshProUGUI> textMeshProUGUIs = new List<TextMeshProUGUI>();
    private bool isAnimationRunning;
    [SerializeField]
    private Transform cameraHolder;
    private Vector3 cameraAngle;
    private Vector3 targetDirection;
    

    private void Start()
    {
        foreach (Transform text in description.transform)
        {
            textMeshProUGUIs.Add(text.GetComponent<TextMeshProUGUI>());
        }
        baseScale = world.localScale;
    }

    public void OnMouseDrag()
    {
        if(isAnimationRunning)
        {
            return;
        }

        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));

        cameraAngle = new Vector3(Input.GetAxis("Mouse Y") * multiplier, Input.GetAxis("Mouse X") * multiplier, 0);
        cameraHolder.Rotate(cameraAngle, Space.Self);

        //world.Rotate(Input.GetAxis("Mouse Y") * multiplier, -Input.GetAxis("Mouse X") * multiplier, 0, Space.World);
        //Camera.main.transform.RotateAround(world.transform.position,)
    }

    public void OnClick()
    {
        //EasterEgg Start
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("EasterEgg"))
            {
                description.GetChild(0).GetComponent<TextMeshProUGUI>().text = "M";
                description.GetChild(1).GetComponent<TextMeshProUGUI>().text = "A";
                description.GetChild(2).GetComponent<TextMeshProUGUI>().text = "T";
                description.GetChild(3).GetComponent<TextMeshProUGUI>().text = "E";
                description.GetChild(4).GetComponent<TextMeshProUGUI>().text = "U";
                description.GetChild(5).GetComponent<TextMeshProUGUI>().text = "SZ";
            }
        }
        //EasterEgg End

        if (isAnimationRunning)
        {
            return;
        }

        if(currentTile != null)
        {
            button.interactable = true;
            selectedTile = currentTile;
            Debug.Log(selectedTile.name);

            //Camera.main.transform.parent.transform.eulerAngles = new Vector3(selectedTile.transform.eulerAngles.x, selectedTile.transform.eulerAngles.y, selectedTile.transform.eulerAngles.z);
            isAnimationRunning = true;
            //previousTile.GetComponent<...> ().levelNumber;
        }
        else
        {
            button.interactable = false;
            selectedTile = null;
        }
    }

    private void Update()
    {
        if (isAnimationRunning)
        {
            targetDirection = currentTile.transform.position - cameraHolder.position;
            // The step size is equal to speed times frame time.
            float singleStep = multiplier * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            //Vector3 newDirection = Vector3.RotateTowards(new Vector3(0, 0, -cameraHolder.position.z), targetDirection, singleStep, 0.0f);
            //Vector3 newDirection = Vector3.RotateTowards(cameraHolder.forward, targetDirection, singleStep, 0.0f);

            Quaternion rotation = Quaternion.LookRotation(targetDirection);

            if (Mathf.Abs(Quaternion.Dot(cameraHolder.rotation, rotation)) > 0.9999f)
            {

                isAnimationRunning = false;
                return;
            }

            cameraHolder.rotation = Quaternion.Lerp(cameraHolder.rotation, rotation, singleStep);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits = Physics.RaycastAll(ray);

        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("Path"))
            {
                if (!hitInfo.transform.GetComponent<RaycastData>().isRaycasted)
                {
                    hitInfo.transform.GetComponent<RaycastData>().isRaycasted = true;
                    if (hitInfo.transform.GetComponent<PathData>().isCompleted)
                    {
                        return;
                    }
                    hitInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    currentTile = hitInfo.transform;
                    SetDescription(hitInfo.transform.GetComponent<PathData>());
                }
            }
            else if (hitInfo.transform.CompareTag("ResetFlags") && currentTile)
            {
                if (!hitInfo.transform.GetComponent<RaycastData>().isRaycasted)
                {
                    currentTile.localScale = new Vector3(1f, 1f, 1f);
                    currentTile.GetComponent<RaycastData>().isRaycasted = false;
                    currentTile = null;
                    NullDescription();
                }
            }
        }
        else if (currentTile != null)
        {
            currentTile.localScale = new Vector3(1f, 1f, 1f);
            currentTile.GetComponent<RaycastData>().isRaycasted = false;
            currentTile = null;
            NullDescription();
        }

        //CheckFlags(hits);

        //foreach (RaycastHit hit in hits)
        //{
        //    if (hit.transform.CompareTag("Path"))
        //    {
        //        if(!hit.transform.GetComponent<RaycastData>().isRaycasted)
        //        {
        //            hit.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        //            hit.transform.GetComponent<RaycastData>().isRaycasted = true;
        //        }
        //    }
        //}    
    }

    void CheckFlags(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Path"))
            {
                if (hit.transform.GetComponent<RaycastData>().isRaycasted)
                {
                    hit.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    hit.transform.GetComponent<RaycastData>().isRaycasted = true;
                }
            }
        }
    }

    void SetDescription(PathData path)
    {
        textMeshProUGUIs[1].text = "Name: " + path.pathName;
        textMeshProUGUIs[2].text = "Description: " + path.pathDescription;
        textMeshProUGUIs[3].text = "Difficulty: " + path.pathDifficulty.ToString();
        textMeshProUGUIs[4].text = "Reward: " + path.rewardName;
        textMeshProUGUIs[5].text = "Reward Ammount: " + path.rewardAmmount.ToString();
    }

    void NullDescription()
    {
        textMeshProUGUIs[1].text = null;
        textMeshProUGUIs[2].text = null;
        textMeshProUGUIs[3].text = null;
        textMeshProUGUIs[4].text = null;
        textMeshProUGUIs[5].text = null;
    }
}
