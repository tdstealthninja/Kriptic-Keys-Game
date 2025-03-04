using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AYellowpaper.SerializedCollections;
using Utils;

public class DynamicPlayerController : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField]
    private SerializedDictionary<ArtifactKeycode, ArtifactBase> artifactKeys = new SerializedDictionary<ArtifactKeycode, ArtifactBase>();

    private PriorityQueue<MoveVelocity, int> movementPriorityQueue = new PriorityQueue<MoveVelocity, int>();

    private List<MoveDirection> moveDirections = new List<MoveDirection>();

    private PlayerInput playerInput;

    private Vector2 playerMovement = Vector2.zero;

    [SerializeField]
    private float baseMoveSpeed = 1f;

    #endregion

    #region Public Variables 

    public List<ArtifactBase> heldArtifacts = new List<ArtifactBase>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Key Pressed Checks

        if (playerInput.actions["W"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.W);
        }

        if (playerInput.actions["A"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.A);
        }

        if (playerInput.actions["S"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.S);
        }

        if (playerInput.actions["D"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.D);
        }

        if (playerInput.actions["Q"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.Q);
        }

        if (playerInput.actions["X"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.X);
        }

        #endregion

        //If a movement based artifact has been used, the action will have been added to the queue by now and the player will try to move
        // Otherwise if there are no movements queued, the Move function won't be called
        if (movementPriorityQueue.Count > 0)
            Move();
    }

    void ActivateArtifact(ArtifactKeycode keycode)
    {
        ArtifactBase artifact;
        if (artifactKeys.TryGetValue(keycode,out artifact))
        {
            artifact.ActivateArtifact(this);
        }
    }

    public void QueueMovement(MoveVelocity movement, int priority)
    {
        movementPriorityQueue.Enqueue(movement, priority);
    }

    void Move()
    {
        MoveVelocity movement;
        int currentPriority;
        while (movementPriorityQueue.TryDequeue(out movement, out currentPriority))
        {
            if (moveDirections.Contains(movement.direction))
            {
                Debug.Log(movement.direction + " movement blocked");
                continue;
            }

            playerMovement += movement.move;
            moveDirections.Add(movement.direction);
        }
        
        transform.Translate(playerMovement * baseMoveSpeed * Time.deltaTime);
        playerMovement = Vector2.zero;
        movementPriorityQueue.Clear();
        moveDirections.Clear();
    }

    public bool AddArtifactToKeyboard(ArtifactBase artifact, ArtifactKeycode keycode)
    {
        return artifactKeys.TryAdd(keycode, artifact);
    }

}


public enum ArtifactKeycode
{
    Q,W,E,R,T,Y,U,I,O,P,
    A,S,D,F,G,H,J,K,L,
    Z,X,C,V,B,N,M,
    UpArrow, DownArrow, LeftArrow, RightArrow,
    NONE
}