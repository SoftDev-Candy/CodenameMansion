using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference mainTheme { get; set; }
    [field: SerializeField] public EventReference mainMenuTheme { get; set; }

    [field: Header("Main Menu SFX")]
    [field: SerializeField] public EventReference click { get; set; }
    [field: SerializeField] public EventReference hover { get; set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; set; }
    [field: SerializeField] public EventReference playerDeath { get; set; }


    [field: Header("Monster SFX")]
    [field: SerializeField] public EventReference monsterDead { get; set; }
    [field: SerializeField] public EventReference monsterKillingPlayer { get; set; }
    [field: SerializeField] public EventReference monsterFootstep { get; set; }

    [field: Header("Chain SFX")]
    [field: SerializeField] public EventReference chain { get; set; }

    [field: Header("General SFX")]
    [field: SerializeField] public EventReference pickUp { get; set; }
    [field: SerializeField] public EventReference pickUpKey { get; set; }
    [field: SerializeField] public EventReference boltCutterPickUp { get; set; }

    [field: Header("Puzzel SFX")]
    [field: SerializeField] public EventReference cakeElevatorCrash { get; set; }
    [field: SerializeField] public EventReference handleCake { get; set; }
    [field: SerializeField] public EventReference handleButton { get; set; }


    [field: Header("Door SFX")]
    [field: SerializeField] public EventReference openDoor { get; set; }
    [field: SerializeField] public EventReference closeDoor { get; set; }
    [field: SerializeField] public EventReference locks { get; set; }

    [field: Header("Closet Door")]
    [field: SerializeField] public EventReference closetDoor { get; set; }

    [field: Header("Inventory SFX")]
    [field: SerializeField] public EventReference mergeSucces { get; set; }
    [field: SerializeField] public EventReference mergeFailed { get; set; }

    [field: Header("Gun SFX")]
    [field: SerializeField] public EventReference gunShooting { get; set; }
    [field: SerializeField] public EventReference gunLoading { get; set; }
    [field: SerializeField] public EventReference gunPickUp { get; set; }
    [field: SerializeField] public EventReference ammoPickUp { get; set; }

    [field: Header("Ingredients SFX")]
    [field: SerializeField] public EventReference ingredient { get; set; }
    [field:SerializeField] public EventReference grabBowl { get; set; }

    [field: Header("Stove SFX")]
    [field: SerializeField] public EventReference stoveHandle {  get; set; }

    public static FMODEvents instance {  get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
