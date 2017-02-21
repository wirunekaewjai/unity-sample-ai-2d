using UnityEngine;

namespace Wirune.W04.Test02
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameModel m_GameModel;

        [Space]
        [SerializeField] private MenuView m_MenuView;
        [SerializeField] private HUDView m_HUDView;
        [SerializeField] private PauseView m_PauseView;

        public GameModel GameModel { get { return m_GameModel; } }

        public MenuView MenuView { get { return m_MenuView; } }
        public HUDView HUDView { get { return m_HUDView; } }
        public PauseView PauseView { get { return m_PauseView; } }

        public Fsm<GameController> Fsm { get; private set; }

        private void Awake()
        {
            Fsm = new Fsm<GameController>(this);

            Fsm.CreateState<GameInitialState>(GameStateID.Initial);
            Fsm.CreateState<GamePlayState>(GameStateID.Play);
            Fsm.CreateState<GamePauseState>(GameStateID.Pause);
            Fsm.CreateState<GameEndState>(GameStateID.End);

            Fsm.ChangeState(GameStateID.Initial);
        }
    }
}

