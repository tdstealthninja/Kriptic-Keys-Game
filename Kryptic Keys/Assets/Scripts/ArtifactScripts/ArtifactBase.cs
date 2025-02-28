using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBase : MonoBehaviour
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

    protected virtual void Awake()
    {
        DynamicPlayerController playerController = FindObjectOfType<DynamicPlayerController>();
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
                playerController.heldArtifacts.Add(this);
            }
        }
        else
        {
            playerController.heldArtifacts.Add(this);
        }
    }

    public virtual void ActivateArtifact(DynamicPlayerController playerController)
    {

    }

}
