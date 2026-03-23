using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum Emotion
{
    Neutral,
    Happy,
    Scared,
    Panic
}

public class Villager : MonoBehaviour
{
    #region Location References
    [Header("Relevant locations")]
    [SerializeField] private GameObject home;
    public GameObject Home { get => home; set => home = value; }
    [SerializeField] private GameObject workPlace;
    public GameObject WorkPlace { get => workPlace; set => workPlace = value; }
    [SerializeField] private GameObject tavern;
    public GameObject Tavern { get => tavern; }
    #endregion
    private VillagerUtlities.Job Job;

    [SerializeField] private List<ScheduleStep> Schedule;
    public ScheduleStep currentStep {get; private set;}
    public bool IsMoving;
    [SerializeField] private NavMeshAgent agent;
    private Emotion currentEmotion;
    public Emotion CurrentEmotion { get => currentEmotion; set => currentEmotion = value; }

    private float calmDownTimer;
    private float calmDownTimerMax = 100;
    private bool isCurrentlyCalmingDown;
    public bool IsCurrentlyCalmingDown { get => isCurrentlyCalmingDown; set => isCurrentlyCalmingDown = value; }

    #region State Machine Variables

    public VillagerStateMachine StateMachine { get; set; }
    public VillagerIdleState IdleState { get; set; }
    public VillagerScheduleState ScheduleState { get; set; }
    public VillagerDisruptionState DisruptionState { get; set; }
    #endregion

    [SerializeField] private VillagerTime villagerTime;

    [SerializeField] private bool debugMode = false;
    public bool DebugMode { get => debugMode; set => debugMode = value; }

    [SerializeField] private GameObject debugPanel;


    private void Awake()
    {
        villagerTime.OnHourChanged.AddListener(ChooseTimeStepInSchedule);
        StateMachine = new VillagerStateMachine();
        ScheduleState = new VillagerScheduleState(this, StateMachine);
        IdleState = new VillagerIdleState(this, StateMachine);
        DisruptionState = new VillagerDisruptionState(this, StateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Schedule = FillSchedule();
        Job = new VillagerUtlities.Job("Backer", workPlace);
        calmDownTimer = 0;
        IsCurrentlyCalmingDown = false;
        CurrentEmotion = Emotion.Neutral;
        StateMachine.Initialize(ScheduleState);
    }


    private void Update()
    {
        CheckEmotion();
        StateMachine.CurrentVillagerState.FrameUpdate();
        ChangeDebugPanelActivity(DebugMode);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentVillagerState.PhysicsUpdate();
    }

    private void CheckEmotion()
    {
        if (CurrentEmotion == Emotion.Panic && calmDownTimer < calmDownTimerMax && !IsCurrentlyCalmingDown)
        {
            calmDownTimer = calmDownTimerMax;
            IsCurrentlyCalmingDown = true;
        }
        if (CurrentEmotion == Emotion.Panic && IsCurrentlyCalmingDown)
        {
            calmDownTimer -= 20.0f * Time.deltaTime;
            //Debug.Log(calmDownTimer);
        }
        if (calmDownTimer <= 0.0f && IsCurrentlyCalmingDown)
        {
            currentEmotion = Emotion.Neutral;
            IsCurrentlyCalmingDown = false;
            calmDownTimer = 0;
        }
    }

    private void ChangeDebugPanelActivity(bool debug)
    {
        if (debug)
        {
            debugPanel.SetActive(true);
            debugPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConstructVillagerLog();
        }
        else
        {
            debugPanel.SetActive(false);
        }
    }

    private string ConstructVillagerLog()
    {
        string log = "";
        log += FillLogLine("State", StateMachine.CurrentVillagerState.ToString());
        log += FillLogLine("Schedule Step", currentStep.ToString());
        log += FillLogLine("Is moving", IsMoving.ToString());
        log += FillLogLine("Emotion", CurrentEmotion.ToString());
        log += FillLogLine("Destination", agent.destination.ToString());
        return log;
    }

    private string FillLogLine(string header, string data)
    {
        return header + ": " + data + "\n";
    }

    private void CreateNewRandomDestinationPoint()
    {
        Vector3 currentDestination;

        currentDestination = new Vector3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-50, 50));
        Debug.Log(currentDestination.ToString());
        agent.SetDestination(currentDestination);
    }

    private void ChooseTimeStepInSchedule()
    {
        currentStep = Schedule[villagerTime.currentHour - 1];
        ScheduleState.ChangeScheduleStep(currentStep);
    }

    public void MoveVillager(Vector3 target)
    {
        //Debug.Log("Started moving towards: " + target);
        agent.SetDestination(target);
        IsMoving = true;
    }

    public void StopVillagerMovement()
    {
        //Debug.Log("Stopped moving");
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        IsMoving = false;
    }

    private void CheckForDanger(Collider other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            //Debug.Log("Watch out! There is something dangerous!");
            currentEmotion = Emotion.Panic;
            IsCurrentlyCalmingDown = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CheckForDanger(other);
    }


    void OnTriggerStay(Collider other)
    {
        CheckForDanger(other);
    }


    private static List<ScheduleStep> FillSchedule(int maxScheduleCount = 3)
    {
        int randomSchedule = Random.Range(0, maxScheduleCount);
        return randomSchedule switch
        {
            0 => new List<ScheduleStep>
                {
                    ScheduleStep.Sleep, //0
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Eat,   //5
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Leisure,
                    ScheduleStep.Eat,   //10
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Leisure,  //15
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Eat,
                    ScheduleStep.Eat,   //20
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep  //23
                },
            1 => new List<ScheduleStep>
                {
                    ScheduleStep.Sleep, //0
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,   //5
                    ScheduleStep.Sleep,
                    ScheduleStep.Eat,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,   //10
                    ScheduleStep.Work,
                    ScheduleStep.Eat,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,  //15
                    ScheduleStep.Work,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Eat,   //20
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Sleep  //23
                },
            2 => new List<ScheduleStep>
                {
                    ScheduleStep.Work, //0
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Eat,   //5
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,   //10
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Eat,  //15
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Eat,   //20
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Work  //23
                },
            _ => new List<ScheduleStep>
                {
                    ScheduleStep.Sleep, //0
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,
                    ScheduleStep.Sleep,   //5
                    ScheduleStep.Sleep,
                    ScheduleStep.Eat,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,   //10
                    ScheduleStep.Work,
                    ScheduleStep.Eat,
                    ScheduleStep.Work,
                    ScheduleStep.Work,
                    ScheduleStep.Work,  //15
                    ScheduleStep.Work,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Eat,   //20
                    ScheduleStep.Leisure,
                    ScheduleStep.Leisure,
                    ScheduleStep.Sleep  //23
                },
        };
    }
}
/* void CreateBehaviorTree()
    {
            rootNode = new SelectorNode(new System.Collections.Generic.List<Node>
        {
            //Disruption
            new SelectorNode(new System.Collections.Generic.List<Node>
            {
                new DisruptionCondition(),
                new SelectorNode(new System.Collections.Generic.List<Node>
                {

                })
            }),
            //Scedule
            new SelectorNode(new System.Collections.Generic.List<Node>
            {
                //eating
                new SelectorNode(new System.Collections.Generic.List<Node>
                {
                   new EatStepInScheduleCondition(currentStep),
                   new EatAction()
                }),
                //sleeping
                new SelectorNode(new System.Collections.Generic.List<Node>
                {
                   new SleepStepInScheduleCondition(currentStep),
                   new SleepAction()
                }),
                //working
                new SelectorNode(new System.Collections.Generic.List<Node>
                {
                   new WorkStepInScheduleCondition(currentStep),
                   new WorkAction()
                }),
                //fun times
                new SelectorNode(new System.Collections.Generic.List<Node>
                {
                   new FreeTimeStepInScheduleCondition(currentStep),
                   new FreeTimeAction()
                })
            }),

        });
    }
     */