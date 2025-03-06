using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtifactBase : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler
{
    [SerializeField]
    protected string artifactName;
    [SerializeField]
    protected string artifactDescription;
    [SerializeField]
    protected ArtifactKeycode artifactKeycode;
    [SerializeField]
    protected float artifactCooldown = 3.0f;
    [SerializeField]
    protected bool hasCooldown = false;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject dragDropObject;

    [HideInInspector]
    public Transform lastParent;
    [HideInInspector]
    public ArtifactHolderUI lastHolder;

    protected virtual void Awake()
    {
        /// Change later to be player specific when item is picked up
        //DynamicPlayerController playerController = FindObjectOfType<DynamicPlayerController>();
        //AddThisArtifactToKeyboard(playerController);
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        dragDropObject = GameObject.FindGameObjectWithTag("DragDrop");
    }

    public virtual void ActivateArtifact(DynamicPlayerController playerController)
    {

    }

    public virtual bool TryAddToPlayerKeyboard(DynamicPlayerController playerController)
    {
        if (artifactKeycode != ArtifactKeycode.NONE)
        {
            bool artifactAdded = playerController.AddArtifactToKeyboard(this, artifactKeycode);
            if (artifactAdded)
            {
                Debug.Log(artifactName + " added to keyboard on key " + artifactKeycode.ToString());
                return true;
            }
            else
            {
                Debug.Log(artifactKeycode.ToString() + " key already holds an artifact");
                return false;
                //playerController.AddArtifactToInventory(this);
            }
        }
        else
        {
            return false;
        }    
    }

    public virtual void AddThisArtifactToKeyboard(DynamicPlayerController playerController)
    {
        if (artifactKeycode != ArtifactKeycode.NONE)
        {
            bool artifactAdded = playerController.AddArtifactToKeyboard(this, artifactKeycode);
            if (artifactAdded)
            {
                Debug.Log(artifactName + " added to keyboard on key " + artifactKeycode.ToString());
            }
            else
            {
                Debug.Log(artifactKeycode.ToString() + " key already holds an artifact");
                playerController.AddArtifactToInventory(this);
            }
        }
        else
        {
            playerController.AddArtifactToInventory(this);
        }
    }

    #region Drag and Drop

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        lastHolder = transform.GetComponentInParent<ArtifactHolderUI>();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        lastParent = transform.parent;
        dragDropObject.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        transform.SetParent(dragDropObject.transform);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(lastParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 

    }

    

    #endregion
}
