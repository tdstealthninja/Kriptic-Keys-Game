using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField]
    protected Color defaultColor;
    [SerializeField]
    protected Color pressedColor;
    //[SerializeField]
    //protected Button button;


    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject dragDropObject;
    private Image image;
    


    [HideInInspector]
    public Transform lastParent;
    [HideInInspector]
    public ArtifactHolderUI lastHolder;
    
    public bool isPressed = false;


    protected virtual void Awake()
    {
        /// Change later to be player specific when item is picked up
        //DynamicPlayerController playerController = FindObjectOfType<DynamicPlayerController>();
        //AddThisArtifactToKeyboard(playerController);
        image = GetComponent<Image>();
        //button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        dragDropObject = GameObject.FindGameObjectWithTag("DragDrop");
    }

    public virtual void ActivateArtifact(DynamicPlayerController playerController)
    {
        if (!isPressed)
        {
            isPressed = true;
            ArtifactPressed();
        }
    }

    public virtual void DeactivateArtifact(DynamicPlayerController playerController)
    {
        if (isPressed)
        {
            isPressed = false;
            ArtifactReleased();
        }
    }

    

    public void ArtifactPressed()
    {
        image.color = pressedColor;
    }

    public void ArtifactReleased()
    {
        image.color = defaultColor;
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
