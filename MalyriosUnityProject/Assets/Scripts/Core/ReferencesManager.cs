using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Malyrios.Dialogue;
using NPCs;
using TMPro;
using UnityEngine;

public class ReferencesManager : MonoBehaviour
{
    
    /* Because it is not effective to use GameObject.Find() or FindGameObjectWithTag/FindObjectOfType very often, this ReferencesManager should help
     * by linking all important References here, so that i can get them with i.e. like this:
     * ReferencesManager.instance.interactableText;
     * Also useful to link References in Prefabs like in PuzzleStation. Here the puzzleWindow, itemSlotsParent are referenced from the base scene, and can be reached in a clean way with the ReferencesManager
     */
    
    public static ReferencesManager Instance;


    public GameObject player;
    public TextMeshProUGUI interactableText;
    public DialogueManager dialogueManager;
    public new CinemachineVirtualCamera camera;
    public GameData gameData;
    public LevelManager levelManager;
    public NPCManager npcManager;
    public QuestLogWindow questLogWindow;
    public GameObject puzzleWindow;
    public Transform itemSlotsParent;
    public DynamicWidth dynamicPuzzleWindowWidth;
    public List<GameObject> logicSymbols;
    public GameObject inventoryUI;
    public PlayerAttack playerAttack;
    public EquipmentSlot weaponSlot;
    public EquipmentSlot headArmorSlot;
    public EquipmentSlot bodyArmorSlot;
    public EquipmentSlot handArmorSlot;
    public EquipmentSlot feetArmorSlot;
    public StatsWindow statsWindow;
    public SaveActiveItems saveActiveItems;
    public AudioSource[] huntressAttackSound;
    public AudioSource huntressSpearSpawnSound;

    private void Awake()
    {
        // Set the static instance variable to this instance of the script
        Instance = this;
    }
}
