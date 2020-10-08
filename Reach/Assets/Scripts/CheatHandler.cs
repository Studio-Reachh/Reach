using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CheatHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private DeliveryTubeReceived deliveryTubeReceived;
    public GameObject Room05TubePopupGameObject;

    public void ClearWholeScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        print("Cheating " + currentSceneName);

        if (currentSceneName == "Room01")
        {
            ClearSceneRoom01();
        }
        else if (currentSceneName == "Room02")
        {
            ClearSceneRoom02();
        }
        else if (currentSceneName == "Room03")
        {
            ClearSceneRoom03();
        }
        else if (currentSceneName == "Room04")
        {
            ClearSceneRoom04();
        }
        else if (currentSceneName == "Room05")
        {
            ClearSceneRoom05();
        }
        else if (currentSceneName == "Room06")
        {
            ClearSceneRoom06();
        }
        else if (currentSceneName == "Room07")
        {
            ClearSceneRoom07();
        }
        else if (currentSceneName == "Room08")
        {
            ClearSceneRoom08();
        }
        else if (currentSceneName == "Room09")
        {
            ClearSceneRoom09();
        }

    }

    #region Island 1 rooms
    private void ClearSceneRoom01()
    {
        //Ceiling hole
        Item woodenPlankItem = Resources.Load<Item>("ScriptableObjects/WoodenPlank");
        CeillingHole ceillingHole = FindObjectOfType<CeillingHole>();
        if (ceillingHole)
        {
            ceillingHole.Interact(woodenPlankItem);
        }

        //PickupWoodenPlank
        DroppedItem woodenplankInteractable = FindObjectOfType<DroppedItem>();
        if (woodenplankInteractable)
        {
            woodenplankInteractable.Interact(null);
            Inventory.RemoveItem(woodenplankInteractable.Item);
        }
    }

    private void ClearSceneRoom02()
    {
        Item oarItem = Resources.Load<Item>("ScriptableObjects/Oar");
        Item pipeItem = Resources.Load<Item>("ScriptableObjects/LoosePipe");
        List<DroppedItem> listAllDroppedItemsInScene = FindObjectsOfType<DroppedItem>().ToList();

        DroppedItem paddleDroppedItem = listAllDroppedItemsInScene.FirstOrDefault(i => i.Item == oarItem);
        if (paddleDroppedItem)
        {
            paddleDroppedItem.Interact(null);
        }

        DroppedItem droppedPipe = listAllDroppedItemsInScene.FirstOrDefault(i => i.Item == pipeItem);
        if (droppedPipe)
        {
            droppedPipe.Interact(null);
            Inventory.RemoveItem(droppedPipe.Item);
        }

        InteractableThatNeedsItem fixedPipe = FindObjectOfType<InteractableThatNeedsItem>();
        if (fixedPipe)
        {
            fixedPipe.Interact(pipeItem);
        }

        MorsecodeMachinePopup machinePopup = FindObjectOfType<MorsecodeMachinePopup>();
        if (machinePopup)
        {
            machinePopup.UITextSend.text = machinePopup.MorsecodeSendMessage;
            StartCoroutine(machinePopup.CheckMorseMessage());
        }
    }

    private void ClearSceneRoom03()
    {
        Item oarItem = Resources.Load<Item>("ScriptableObjects/Oar");
        InteractableAndSceneTransition boat = FindObjectOfType<InteractableAndSceneTransition>();
        if (boat)
        {
            boat.Interact(oarItem);
            Inventory.RemoveItem(oarItem);
        }
    }
    #endregion

    #region Island 2 rooms
    private void ClearSceneRoom04()
    {
        DroppedItem tubeDroppedItem = FindObjectOfType<DroppedItem>();
        if (tubeDroppedItem)
        {
            tubeDroppedItem.Interact(null);
            Inventory.RemoveItem(tubeDroppedItem.Item);
        }

        DeliveryTube deliveryTube = FindObjectOfType<DeliveryTube>();
        if (deliveryTube)
        {
            deliveryTube.Interact(deliveryTube.ItemNeeded);
            Inventory.RemoveItem(deliveryTube.ItemNeeded);
        }

        if (deliveryTubeReceived)
        {
            deliveryTubeReceived.HasCollided = true;
        }

        if (Room05TubePopupGameObject)
        {
            Room05TubePopupGameObject.SetActive(true);
        }
    }

    private void ClearSceneRoom05()
    {
        TiltCylinder tiltCylinder = FindObjectOfType<TiltCylinder>();
        if (tiltCylinder)
        {
            Destroy(tiltCylinder.gameObject);
        }
    }

    private void ClearSceneRoom06()
    {
        GameObject barriersGO = GameObject.Find("Barriers");
        if (barriersGO)
        {
            Destroy(barriersGO);
        }
    }

    private void ClearSceneRoom07()
    {
        DroppedItem leverDroppedItem = FindObjectOfType<DroppedItem>();
        if (leverDroppedItem)
        {
            leverDroppedItem.Interact(null);
            Inventory.RemoveItem(leverDroppedItem.Item);
        }

        OnOffLever onOffLever = FindObjectOfType<OnOffLever>();
        if (onOffLever)
        {
            onOffLever.IsOn = true;
            onOffLever.HasLever = true;

            if (!onOffLever.IsOn)
            {
                onOffLever.LeverSpriteRenderer.transform.position = new Vector3(-2.217f, 3.167f, -2);
            }
            else
            {
                onOffLever.LeverSpriteRenderer.transform.position = new Vector3(-2.217f, 2.647f, -2);
            }

            onOffLever.AddLever();

            SaveHandler.SaveLevel(onOffLever.gameObject.name, "IsOn", onOffLever.IsOn);
            SaveHandler.SaveLevel(onOffLever.gameObject.name, "HasLever", onOffLever.HasLever);
        }

        MovingBarrier movingBarrier = FindObjectOfType<MovingBarrier>();
        if (movingBarrier)
        {
            movingBarrier.IsAbove = true;
            SaveHandler.SaveLevel(movingBarrier.gameObject.name, "IsAbove", true);
            movingBarrier.SetBarrierPos();
        }

        TicTacToMachinePopup ticTacToMachine = FindObjectOfType<TicTacToMachinePopup>();
        if (ticTacToMachine)
        {
            SaveHandler.SaveLevel(ticTacToMachine.gameObject.name, "IsSolved", true);
            ticTacToMachine.IsSolved = true;
            ticTacToMachine.OpenHatch();
            for (int i = 0; i < ticTacToMachine._solution.Length; i++)
            {
                ticTacToMachine.Textboxes[i].text = ticTacToMachine._solution[i].ToString();
            }
        }

        MovingLadder movingLadder = FindObjectOfType<MovingLadder>();
        if (movingLadder)
        {
            movingLadder.transform.position = new Vector3(movingLadder._leftPos.position.x, movingLadder.transform.position.y, movingLadder.transform.position.z);
        }
    }

    private void ClearSceneRoom08()
    {
        CylinderSoundPlayerMachine cylinderSoundPlayerMachine = FindObjectOfType<CylinderSoundPlayerMachine>();
        if (cylinderSoundPlayerMachine)
        {
            cylinderSoundPlayerMachine.HasCylinder = true;
            cylinderSoundPlayerMachine.CornerTubeIsPlaced = true;
            cylinderSoundPlayerMachine.StraigthTubeIsPlaced = true;
            cylinderSoundPlayerMachine.MachineIsActivated = true;

            cylinderSoundPlayerMachine.ActivateMusic();
            cylinderSoundPlayerMachine.UseSpriteMachineWithCylinder();
        }
    }

    private void ClearSceneRoom09()
    {
        InteractableAndSceneTransition endDoor = FindObjectOfType<InteractableAndSceneTransition>();
        if (endDoor)
        {
            endDoor.IsDoorLocked = false;
            endDoor.Interact(null);
        }
    }

    private float currentHoldItem = 0;
    private float maxHoldTime = 2.5f;
    private bool isOnMouseDown;

    private void Update()
    {
        if (isOnMouseDown)
        {
            currentHoldItem += Time.deltaTime;
            if (currentHoldItem >= maxHoldTime)
            {
                ClearWholeScene();
                currentHoldItem = 0;
                isOnMouseDown = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOnMouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOnMouseDown = false;
    }

    #endregion
}
