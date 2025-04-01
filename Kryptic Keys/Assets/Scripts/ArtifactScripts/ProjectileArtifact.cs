using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArtifact : ArtifactBase
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private int projectileAmount = 1;
    [SerializeField]
    private float projectileAngleRandomness = 0.1f;

    public int projectileMultiplier = 1;
    public float projectileSpeed = 600f;
    


    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivateArtifact(DynamicPlayerController playerController)
    {
        base.ActivateArtifact(playerController);
        if (canBeActivated)
        {
            GameObject spawnedProjectile;

            if (projectilePrefab)
            {
                spawnedProjectile = Instantiate(projectilePrefab, playerController.projectileSpawnpoint.transform.position, Quaternion.identity);
                //Vector2 direction = playerController.GetPlayerMovementDirection();
                //direction.x += Random.Range(-projectileAngleRandomness, projectileAngleRandomness);
                spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(playerController.transform.up * projectileSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        
    }

    public override void DeactivateArtifact(DynamicPlayerController playerController)
    {
        base.DeactivateArtifact(playerController);
    }
}
