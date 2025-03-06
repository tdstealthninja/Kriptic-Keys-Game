using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using Utils;
using UnityEngine.Events;

public class KeyboardArtifactManager : MonoBehaviour
{

    //[SerializeField]
    //private SerializedDictionary<ArtifactKeycode, ArtifactHolderUI> artifactKeys = new SerializedDictionary<ArtifactKeycode, ArtifactHolderUI>();

    [SerializeField]
    private DynamicPlayerController playerController;

    public static UnityEvent<ArtifactHolderUI> UpdateArtifactEvent;



    // Start is called before the first frame update
    void Start()
    {
        if (UpdateArtifactEvent == null)
            UpdateArtifactEvent = new UnityEvent<ArtifactHolderUI>();

        UpdateArtifactEvent.AddListener(UpdateArtifact);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateArtifact(ArtifactHolderUI artifactHolder)
    {
        if (playerController)
        {
            if (artifactHolder.artifact)
            {
                playerController.AddArtifactToKeyboard(artifactHolder.artifact, artifactHolder.keyCode);
            }
            else
            {
                playerController.RemoveArtifactFromKeyboard(artifactHolder.keyCode);
            }
        }
        
    }
}
