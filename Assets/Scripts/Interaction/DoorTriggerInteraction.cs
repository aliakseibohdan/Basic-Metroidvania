using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorTriggerInteraction : TriggerInteractionBase
{
    public new GameObject Player { get; set; }
    public new bool CanInteract { get; set; }

    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    [Header("Spawn TO")]
    [SerializeField] private DoorToSpawnAt DoorToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("THIS Door")]
    public DoorToSpawnAt CurrentDoorPosition;

    private void Start()
    {
        // Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Interact();
            }
        }
    }

    public override void Interact()
    {
        SceneSwapManager.SwapSceneFromDoorUse(_sceneToLoad, DoorToSpawnTo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CanInteract = true;

        /*if (collision.gameObject == Player)
        {
            CanInteract = true;
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanInteract = false;

        /*if (collision.gameObject == Player)
        {
            CanInteract = false;
        }*/
    }
}