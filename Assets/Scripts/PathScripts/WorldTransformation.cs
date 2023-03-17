using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject alertPrefab;
    private GameObject alert;
    

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

    private void LateUpdate()
    {
        if (isAnimationRunning)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("Path"))
            {
                if (hitInfo.transform.GetComponent<PathData>().isCompleted)
                {
                    return;
                }
                onTileHover(hitInfo);
            }
            else if (hitInfo.transform.CompareTag("ResetFlags") && currentTile)
            {
                if (!hitInfo.transform.GetComponent<RaycastData>().isRaycasted)
                {
                    ResetFlags();
                }
            }
        }
        else if (currentTile != null)
        {
            ResetFlags();
        }
    }

    private void Update()
    {
        if (isAnimationRunning)
        {
            OnAnimationRunning();
        }
    }

    private void OnAnimationRunning()
    {
        targetDirection = currentTile.transform.position - cameraHolder.position;
        float singleStep = multiplier * Time.deltaTime;

        Quaternion rotation = Quaternion.LookRotation(targetDirection);

        if (Mathf.Abs(Quaternion.Dot(cameraHolder.rotation, rotation)) > 0.9999f)
        {

            isAnimationRunning = false;
            ResetFlags();
            return;
        }

        cameraHolder.rotation = Quaternion.Lerp(cameraHolder.rotation, rotation, singleStep);
    }

    private void onTileHover(RaycastHit hitInfo)
    {
        if (!hitInfo.transform.GetComponent<RaycastData>().isRaycasted)
        {
            hitInfo.transform.GetComponent<RaycastData>().isRaycasted = true;
            //hitInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            currentTile = hitInfo.transform;
            SetDescription(hitInfo.transform.GetComponent<PathData>());
            //if (alert == null)
            //{
            //    alert = Instantiate(alertPrefab);
            //}
            //alert.transform.position = currentTile.transform.position;
            //alert.transform.LookAt(cameraHolder.position);
        }
    }

    void ResetFlags()
    {
        //currentTile.localScale = new Vector3(1f, 1f, 1f);
        currentTile.GetComponent<RaycastData>().isRaycasted = false;
        currentTile = null;
        NullDescription();
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
