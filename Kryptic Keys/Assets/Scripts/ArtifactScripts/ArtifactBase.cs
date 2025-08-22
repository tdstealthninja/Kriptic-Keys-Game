using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtifactBase : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler
{

    #region Protected Variables
    [SerializeField]
    protected string artifactName;
    [SerializeField]
    protected string artifactDescription;
    [SerializeField]
    protected ArtifactKeycode artifactKeycode;
    [SerializeField]
    protected List<ArtifactKeycode> adjecentKeycodes = new List<ArtifactKeycode>();
    [SerializeField]
    protected float artifactCooldown = 3.0f;
    [SerializeField]
    protected float cooldownTimer = 0f;
    [SerializeField]
    protected float cooldownSpeed = 0.1f;
    [SerializeField]
    protected float cooldownMultiplier = 1f;
    [SerializeField]
    protected bool hasCooldown = false;
    [SerializeField]
    protected bool pauseActivation = false;
    [SerializeField]
    protected bool canBeActivated = true;
    [SerializeField]
    protected Color defaultColor;
    [SerializeField]
    protected Color pressedColor;
    //[SerializeField]
    //protected Button button;
    #endregion

    #region Private Variables
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject dragDropObject;
    private Image image;
    #endregion

    #region Public Variables
    [HideInInspector]
    public Transform lastParent;
    [HideInInspector]
    public ArtifactHolderUI lastHolder;
    
    public bool isPressed = false;
    #endregion

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
        if (!pauseActivation)
        {
            if (!isPressed)
            {
                isPressed = true;
                ArtifactPressed();
            }

            if (hasCooldown && cooldownTimer <= 0)
            {
                cooldownTimer = artifactCooldown;
            }
        }

    }

    public virtual void DeactivateArtifact(DynamicPlayerController playerController)
    {
        if (!pauseActivation)
        {
            if (isPressed)
        {
            isPressed = false;
            ArtifactReleased();
        }
        }
        
    }

    protected virtual void Update()
    {
        if (!pauseActivation)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= cooldownSpeed * cooldownMultiplier;
                canBeActivated = false;
            }
            if (cooldownTimer < 0)
            {
                canBeActivated = true;
            }
        }
    }

    public void ArtifactPressed()
    {
        image.color = pressedColor;
        Debug.Log(this.artifactName + " pressed");
    }

    public void ArtifactReleased()
    {
        image.color = defaultColor;
        Debug.Log(this.artifactName + " released");
    }

    public void ToggleArtifactActivation(bool toggle)
    {
        pauseActivation = toggle;
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
    /// <summary>
    /// Get adjecent keys around keycode (letter).
    /// </summary>
    /// <param name="keycode"></param>
    public void SetAdjecentKeycodes(ArtifactKeycode keycode)
    {
        //List<ArtifactKeycode> adjecentKeycodes = new List<ArtifactKeycode>();
        adjecentKeycodes.Clear();
        int keynum = (int)keycode;
        int keyadjecent = 0;

        if (keycode != ArtifactKeycode.NONE)
        {
            //Top row
            if (keynum < 10)
            {
                //Left
                keyadjecent = keynum - 1;
                if (keyadjecent >= 0 && keyadjecent <= 9)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Right
                keyadjecent = keynum + 1;
                if (keyadjecent >= 0 && keyadjecent <= 9)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

                //Down
                keyadjecent = keynum + 10;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

                //Down left
                keyadjecent = keynum + 11;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

                //Down right
                keyadjecent = keynum + 9;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
            }
            //Bottom row
            else if (keynum > 19)
            {
                //Left
                keyadjecent = keynum - 1;
                if (keyadjecent >= 20 && keyadjecent <= 26)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Right
                keyadjecent = keynum + 1;
                if (keyadjecent >= 20 && keyadjecent <= 26)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Up
                keyadjecent = keynum - 10;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

                //Up left
                keyadjecent = keynum - 11;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

                //Up right
                keyadjecent = keynum - 9;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }

            }
            //Middle row
            else
            {
                //Left
                keyadjecent = keynum - 1;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Right
                keyadjecent = keynum + 1;
                if (keyadjecent >= 10 && keyadjecent <= 18)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Up
                keyadjecent = keynum - 10;
                if (keyadjecent >= 0 && keyadjecent <= 9)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Down
                keyadjecent = keynum + 10;
                if (keyadjecent >= 20 && keyadjecent <= 26)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Up left
                keyadjecent = keynum - 11;
                if (keyadjecent >= 0 && keyadjecent <= 9)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Down left
                keyadjecent = keynum + 11;
                if (keyadjecent >= 20 && keyadjecent <= 26)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Up right
                keyadjecent = keynum - 9;
                if (keyadjecent >= 0 && keyadjecent <= 9)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
                //Down right
                keyadjecent = keynum + 9;
                if (keyadjecent >= 20 && keyadjecent <= 26)
                {
                    adjecentKeycodes.Add((ArtifactKeycode)keyadjecent);
                }
            }

        }

    }

    #region Drag and Drop

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        lastHolder = transform.GetComponentInParent<ArtifactHolderUI>();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        lastParent = transform.parent;
        dragDropObject.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        transform.SetParent(dragDropObject.transform);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(lastParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 

    }

    

    #endregion
}
