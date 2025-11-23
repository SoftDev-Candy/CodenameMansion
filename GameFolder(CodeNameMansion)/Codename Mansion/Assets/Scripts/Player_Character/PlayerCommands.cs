using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using FMOD.Studio;
public class PlayerCommands: MonoBehaviour
{

    [SerializeField] private Queue<ICommand> m_CommandQueue;

    private InputActionMap playerActionMap;
    [SerializeField] private NavMeshAgent agent;
    private ICommand currentTask;
    private InputAction createTask;
    [SerializeField] private Animator animator;
    private LayerMask commandRayLayer;
    public bool isDead;
    private bool isMobileDevice;


    //AUDIO
    private EventInstance playerFootsteps;
    private float parameterValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_CommandQueue = new Queue<ICommand>();
        playerActionMap = InputSystem.actions.FindActionMap("Player");
        createTask = playerActionMap.FindAction("CreateTask");
        createTask.performed += context => CreateTasks();

        if(Application.platform == RuntimePlatform.Android)
        {
            isMobileDevice = true;
        }

        commandRayLayer = LayerMask.GetMask("Clickable", "UI");
    }

    private void Start()
    {
        parameterValue = 1;
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        playerFootsteps.start();
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteTask();
        UpdateSound();
    }

    private void UpdateSound()
    {
        PLAYBACK_STATE playBackState;
        playerFootsteps.getPlaybackState(out playBackState);

        if (agent.velocity.magnitude > 0.1f) // Player is moving
        {
            playerFootsteps.setParameterByName("FootstepGround", 1f);
        }
        else // Player stopped moving
        {
            playerFootsteps.setParameterByName("FootstepGround", 0f);
        }
        
    }


    public void ClearCurrentTasks()
    {
        m_CommandQueue.Clear();
        agent.ResetPath();
    }

    private void CreateTasks()
    {
        if (GameManager.instance.isItemMenuOn|| EventSystem.current.IsPointerOverGameObject() || isDead)
            return;

        m_CommandQueue.Clear();
        currentTask = null;
        MoveToDestination moveToDestination = new MoveToDestination(GetGoalPosition(), agent, animator);
        m_CommandQueue.Enqueue(moveToDestination);
        ItemBase item = GetPressedItem();

        if (item != null)
        {
            switch(item.GetItemType())
            {
                case ItemType.PICKUP:
                    PickUpItemCommand pickUpitem = new PickUpItemCommand(item);
                    m_CommandQueue.Enqueue(pickUpitem);
                    break;
                case ItemType.INTERACT:
                    InteractItem interactItem = new InteractItem(item);
                    m_CommandQueue.Enqueue(interactItem);
                    break;
                case ItemType.BOTH:
                    InteractItem bothInteractitem = new InteractItem(item);
                    PickUpItemCommand bothPickUpItemCommand = new PickUpItemCommand(item);
                    m_CommandQueue.Enqueue(bothInteractitem);
                    m_CommandQueue.Enqueue(bothPickUpItemCommand);
                    break;
            }

        }
        currentTask = m_CommandQueue.Dequeue();
    }

    public void CreateDragAndDropTask(ItemBase DNBItem, ItemData item)
    {
        m_CommandQueue.Clear();
        currentTask = null;
        MoveToDestination moveToDestination = new MoveToDestination(DNBItem.transform.position, agent, animator);
        m_CommandQueue.Enqueue(moveToDestination);
        DragAndDropCommand dragAndDrop = new DragAndDropCommand(DNBItem, item);
        m_CommandQueue.Enqueue(dragAndDrop);
        currentTask = m_CommandQueue.Dequeue();
    }

    public void CreateShootTask(EnemyCombat enemy,int totalDamage)
    {
        m_CommandQueue.Clear();
        currentTask = null;
        ShootCommand shootCommand = new ShootCommand(enemy, totalDamage);
        m_CommandQueue.Enqueue(shootCommand);
        currentTask = m_CommandQueue.Dequeue();
    }

    private void ExecuteTask()
    {
        if (currentTask == null)
            return;

        switch (currentTask.GetTaskState())
        {
            case TaskStates.WAITING:
                currentTask.execute();
                break;
            case TaskStates.RUNNING:
                currentTask.CheckTaskCondition();
                break;
            case TaskStates.COMPLETED:
                if(m_CommandQueue.Count > 0)
                {
                    currentTask = m_CommandQueue.Dequeue();
                }
                else
                {
                    currentTask = null;
                }
                break;
        }
    }

    private Vector3 GetGoalPosition()
    {
        Ray ray;
        if (isMobileDevice)
        {
            ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
        }
        else
        {
            ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        }
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, commandRayLayer))
        {
            return hit.point;
        }
        return transform.position;
    }

    private ItemBase GetPressedItem()
    {
        Ray ray;
        if(isMobileDevice)
        {
            ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
        }
        else
        {
            ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, commandRayLayer))
        {
            Debug.Log(hit.collider.gameObject.GetComponent<ItemBase>());
            return hit.collider.gameObject.GetComponent<ItemBase>();
        }
        return null;
    }
}
