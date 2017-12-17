using UnityEngine;
using VillageKeeper.FSM;
using VillageKeeper.Game;
using VillageKeeper.UI;
using VillageKeeper.Audio;
using VillageKeeper.Data;
using Shibari;

namespace VillageKeeper
{
    public class Core : MonoBehaviour
    {
        public static Core Instance { get; private set; }

        public StateMachine FSM { get; private set; }

        public Data.Data Data { get; private set; }

        public System.Random Random { get; private set; }

        public GameController GameManager { get; private set; }

        public AudioManager AudioManager { get; private set; }

        public Monster Monster { get; private set; }

        public Archer Archer { get; private set; }

        public ControlsScript Controls { get; private set; }

        public MainMenuScript MainMenu { get; private set; }

        public BuildingsAreaScript BuildingsArea { get; private set; }

        private void Awake()
        {
            Instance = this;

            Random = new System.Random();
            
            FSM = new StateMachine();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Monster = FindObjectOfType(typeof(Monster)) as Monster;
            Archer = FindObjectOfType(typeof(Archer)) as Archer;
            Controls = FindObjectOfType(typeof(ControlsScript)) as ControlsScript;
            MainMenu = FindObjectOfType(typeof(MainMenuScript)) as MainMenuScript;

            BuildingsArea = FindObjectOfType<BuildingsAreaScript>();

            Model.Init();

            Data = new Data.Data();
            Data.Init();

            GameManager = transform.Find("Game").GetComponent<GameController>();
            AudioManager = transform.Find("Audio").GetComponent<AudioManager>();

            GameManager.Init();
            AudioManager.Init();

            FSM.Event(StateMachineEvents.GameInitialized);
        }
    }
}