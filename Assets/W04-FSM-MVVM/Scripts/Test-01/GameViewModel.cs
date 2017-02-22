using UnityEngine;

namespace Wirune.W04.Test01
{
    public class GameViewModel : MvvmBehaviour
    {
        [SerializeField] private GameModel m_Model;

        [Header("View")]
        [SerializeField] private MenuView m_MenuView;
        [SerializeField] private HUDView m_HUDView;
        [SerializeField] private PauseView m_PauseView;

        public GameModel Model { get { return m_Model; } }

        public MenuView MenuView { get { return m_MenuView; } }
        public HUDView HUDView { get { return m_HUDView; } }
        public PauseView PauseView { get { return m_PauseView; } }
        public Fsm<GameViewModel> Fsm { get; private set; }

        private void Awake()
        {
            Fsm = new Fsm<GameViewModel>(this);

            Fsm.CreateState<GameInitialState>(GameStateID.Initial);
            Fsm.CreateState<GamePlayState>(GameStateID.Play);
            Fsm.CreateState<GamePauseState>(GameStateID.Pause);
            Fsm.CreateState<GameEndState>(GameStateID.End);

            Fsm.ChangeState(GameStateID.Initial);
        }
    }
}

