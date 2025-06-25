using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    bool playerProjectile { get; set; }
    bool canCombo { get; set; }
    

    void OnHit();

    void DestroyProjectle();

    void Combo(ProjectileScript other);
}
