using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CylinderSoundPlayerMachine : MonoBehaviour
{
    public GameObject CylinderToActivateTheMachine;
    public SpriteRenderer Renderer;
    public Sprite SpriteMachineWithCylinder;
    public bool HasCylinder;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "HasCylinder", out bool hasCylinder))
        {
            HasCylinder = hasCylinder;
        }

        if (HasCylinder)
        {
            //Play music
            ActivateMusic();

            //Update sprite to the machine that has the cylinder
            //Destroy the Cylinder gameobject already present in the scene (Only destroy the cylinder that is needed to activate this machine)
            UseSpriteMachineWithCylinder();
        }   
    }

    private void Update()
    {
        if (HasCylinder)
        {
            return;
        }

        RaycastHit2D[] rayHitInfo = Physics2D.RaycastAll(transform.position, Vector2.right, 3f, LayerMask.GetMask("Obstacle"));
        for (int i = 0; i < rayHitInfo.Length; i++)
        {
            RaycastHit2D rayHit = rayHitInfo[i];

            if (rayHit.transform.parent && rayHit.transform.parent.gameObject == this.gameObject && rayHit.transform.gameObject != this.gameObject)//Check if de root.gameobject werkt
            {
                //Check if the rayhit is not colliding with the machine itself (The machine belongs to the obstacle layer aswell)
                continue;
            }

            if (rayHit.transform.gameObject != null && rayHit.transform.gameObject == CylinderToActivateTheMachine)
            {
                //Collided with the cylidner that activates this machine
                HasCylinder = true;
                UseSpriteMachineWithCylinder();
                ActivateMusic();
            }
        }
    }
    public void ActivateMusic()
    {
        //Active music here
    }

    public void UseSpriteMachineWithCylinder()
    {
        SaveHandler.SaveLevel(this.name, "HasCylinder", true);
        Renderer.sprite = SpriteMachineWithCylinder;
        if (CylinderToActivateTheMachine)
        {
            Destroy(CylinderToActivateTheMachine);
        }
    }
}
