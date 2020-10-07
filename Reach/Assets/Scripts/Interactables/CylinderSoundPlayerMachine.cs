using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CylinderSoundPlayerMachine : MonoBehaviour
{
    public AudioSource AudioSource;
    public GameObject CylinderToActivateTheMachine;
    public SpriteRenderer Renderer;
    public Sprite SpriteMachineWithCylinder;
    public bool HasCylinder;

    public bool StraigthTubeIsPlaced, CornerTubeIsPlaced;

    public bool MachineIsActivated;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Corner Pipe (Empty Gameobject)", "CornerPipePlaced", out bool cornerTubeIsPlaced))
        {
            CornerTubeIsPlaced = cornerTubeIsPlaced;
        }

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Straight Pipe (Empty Gameobject)", "StraightPipePlaced", out bool straightTubeIsPlaced))
        {
            StraigthTubeIsPlaced = straightTubeIsPlaced;
        }      

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "HasCylinder", out bool hasCylinder))
        {
            HasCylinder = hasCylinder;
        }

        if (HasCylinder && CornerTubeIsPlaced && StraigthTubeIsPlaced)
        {
            MachineIsActivated = true;
            //Play music
            ActivateMusic();

            //Update sprite to the machine that has the cylinder
            //Destroy the Cylinder gameobject already present in the scene (Only destroy the cylinder that is needed to activate this machine)
            UseSpriteMachineWithCylinder();
        }   
    }

    private void Update()
    {
        if (!MachineIsActivated && HasCylinder && CornerTubeIsPlaced && StraigthTubeIsPlaced)
        {
            MachineIsActivated = true;
            UseSpriteMachineWithCylinder();
            ActivateMusic();
        }

        if (MachineIsActivated)
        {
            return;
        }

        if (!CornerTubeIsPlaced && SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Corner Pipe (Empty Gameobject)", "CornerPipePlaced", out bool cornerTubeIsPlaced))
        {
            CornerTubeIsPlaced = cornerTubeIsPlaced;
        }

        if (!StraigthTubeIsPlaced && SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Straight Pipe (Empty Gameobject)", "StraightPipePlaced", out bool straightTubeIsPlaced))
        {
            StraigthTubeIsPlaced = straightTubeIsPlaced;
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
            }
        }
    }
    public void ActivateMusic()
    {
        //Active music here
        AudioSource.loop = true;
        AudioSource.Play();
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
