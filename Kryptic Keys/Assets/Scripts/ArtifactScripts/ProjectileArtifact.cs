using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArtifact : ArtifactBase
{
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected int projectileAmount = 1;
    [SerializeField]
    protected float projectileAngleRandomness = 0.1f;

    public int projectileMultiplier = 1;
    public float projectileSpeedMultiplier = 1;
    


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
                spawnedProjectile.GetComponent<IProjectile>().playerProjectile = true;
                ProjectileScript projectileScript = spawnedProjectile.GetComponent<ProjectileScript>();
                int damage = Mathf.FloorToInt((float)projectileScript.GetDamage() * setMultiplier);
                projectileScript.SetDamage(damage);
                float projectileSpeed = projectileScript.projectileSpeed;
                Vector2 direction = playerController.GetPlayerMovementDirection().normalized;

                //direction *= Random.Range(-projectileAngleRandomness, projectileAngleRandomness);
                direction.x += Random.Range(-projectileAngleRandomness, projectileAngleRandomness);
                direction.y += Random.Range(-projectileAngleRandomness, projectileAngleRandomness);
                direction *= playerController.transform.right;
                Vector3 direction3D = new Vector3(direction.x, direction.y /*0*/, 0);
                spawnedProjectile.GetComponent<Rigidbody2D>().AddForce((playerController.transform.up + direction3D) * projectileSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        
    }

    public override void DeactivateArtifact(DynamicPlayerController playerController)
    {
        base.DeactivateArtifact(playerController);
    }
}
