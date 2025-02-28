using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArtifact : ArtifactBase
{
    [SerializeField]
    private float movementSpeedMultiplyer = 1;
    [SerializeField]
    private int priority = 1;
    [SerializeField]
    private MoveDirection moveDirection;
    [SerializeField]
    private bool canHoverOverHazard = false;



    public override void ActivateArtifact(DynamicPlayerController playerController)
    {
        //base.ActivateArtifact(playerController);

        MoveVelocity moveVelocity;
        moveVelocity.direction = moveDirection;

        switch (moveDirection)
        {
            case MoveDirection.UP:
                moveVelocity.move = Vector2.up * movementSpeedMultiplyer;
                playerController.QueueMovement(moveVelocity, priority);
                break;
            case MoveDirection.DOWN:
                moveVelocity.move = Vector2.down * movementSpeedMultiplyer;
                playerController.QueueMovement(moveVelocity, priority);
                break;
            case MoveDirection.LEFT:
                moveVelocity.move = Vector2.left * movementSpeedMultiplyer;
                playerController.QueueMovement(moveVelocity, priority);
                break;
            case MoveDirection.RIGHT:
                moveVelocity.move = Vector2.right * movementSpeedMultiplyer;
                playerController.QueueMovement(moveVelocity, priority);
                break;
            default:
                break;
        }
    }

}

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public struct MoveVelocity
{
    public Vector2 move;
    public MoveDirection direction;

    //public MoveVelocity(Vector2 vector2, MoveDirection direction)
    //{
    //    this.move = vector2;
    //    this.direction = direction;
    //}
}
