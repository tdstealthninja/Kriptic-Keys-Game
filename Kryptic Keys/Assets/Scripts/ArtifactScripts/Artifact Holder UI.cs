using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtifactHolderUI : MonoBehaviour, IDropHandler
{
    public ArtifactKeycode keyCode;
    public ArtifactBase artifact;
    public Image image;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnArtifact()
    {
        if (artifact != null)
        {
            Instantiate(artifact, gameObject.transform);
        }
    }

    #region Drag and Drop

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null && artifact == null)
        {
            artifact = eventData.pointerDrag.GetComponent<ArtifactBase>();
            KeyboardArtifactManager.UpdateArtifactEvent?.Invoke(this);
            artifact.lastHolder.artifact = null;
            KeyboardArtifactManager.UpdateArtifactEvent?.Invoke(artifact.lastHolder);
            artifact.lastHolder = this;
            
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            artifact.lastParent = transform;
        }

        
    }

    #endregion 
}
