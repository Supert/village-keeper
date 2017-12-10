using UnityEngine;
using VillageKeeper.FSM;
using VillageKeeper.Game;
using VillageKeeper.UI;
using VillageKeeper.Audio;
using VillageKeeper.Locale;
using VillageKeeper.Data;
using VillageKeeper.Balance;
using Shibari;

namespace VillageKeeper
{
    public class Core : MonoBehaviour
    {
        public static Core Instance { get; private set; }

        public StateMachine FSM { get; private set; }

        public Localization Localization { get; private set; }
        
        public ResourceData ResourceData { get; private set; }
        public SavedData SavedData { get; private set; }
        public CommonData CommonData { get; private set; }
        public GameData GameData { get; private set; }
        
        public GameController GameManager { get; private set; }
        public AudioManager AudioManager { get; private set; }

        public Monster Monster { get; private set; }

        public ArcherScript Archer { get; private set; }

        public ControlsScript Controls { get; private set; }

        public MainMenuScript MainMenu { get; private set; }



        public BuildingsAreaScript BuildingsArea { get; private set; }

        void Awake()
        {
            Instance = this;

            FSM = new StateMachine();

            Localization = new Localization();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Monster = FindObjectOfType(typeof(Monster)) as Monster;
            Archer = FindObjectOfType(typeof(ArcherScript)) as ArcherScript;
            Controls = FindObjectOfType(typeof(ControlsScript)) as ControlsScript;
            MainMenu = FindObjectOfType(typeof(MainMenuScript)) as MainMenuScript;

            BuildingsArea = FindObjectOfType<BuildingsAreaScript>();

            Model.Init();

            ResourceData = Model.Get<ResourceData>("Resources");
            SavedData = Model.Get<SavedData>("Saved");
            CommonData = Model.Get<CommonData>("Common");
            GameData = Model.Get<GameData>("Game");

            Model.DeserializeData("Saved", Resources.Load<TextAsset>("Data/VillageKeeper.Balance.GameData").text);

            GameManager = transform.Find("Game").GetComponent<GameController>();
            AudioManager = transform.Find("Audio").GetComponent<AudioManager>();

            GameManager.Init();
            AudioManager.Init();

            FSM.Event(StateMachineEvents.GameInitialized);
        }
    }
}