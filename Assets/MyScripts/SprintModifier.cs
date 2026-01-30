using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

namespace MyScripts
{
    /// <summary>
    /// Modifica a velocidade do Continuous Move Provider baseado em input de sprint.
    /// </summary>
    public class SprintModifier : MonoBehaviour
    {
        [Header("Referências")] [SerializeField] [Tooltip("O Continuous Move Provider que será modificado.")]
        ContinuousMoveProvider m_MoveProvider;
        [SerializeField]
        private XRInteractionSimulator m_simulator;

        [Header("Configurações de Velocidade")] [SerializeField] [Tooltip("Velocidade normal de caminhada.")]
        float m_WalkSpeed = 2.5f;

        [SerializeField] [Tooltip("Velocidade ao correr (sprint).")] 
        float m_SprintSpeed = 5f;

        [Header("Input")] [SerializeField] [Tooltip("Input que ativa o sprint quando pressionado.")]
        private InputActionReference m_SprintInput;

        /// <summary>
        /// Velocidade normal de caminhada.
        /// </summary>
        public float walkSpeed
        {
            get => m_WalkSpeed;
            set => m_WalkSpeed = value;
        }

        /// <summary>
        /// Velocidade ao correr.
        /// </summary>
        public float sprintSpeed
        {
            get => m_SprintSpeed;
            set => m_SprintSpeed = value;
        }

        /// <summary>
        /// Input do sprint.
        /// </summary>
        public InputActionReference sprintInput
        {
            get => m_SprintInput;
            set => m_SprintInput = value;
        }

        void OnEnable()
        {
            m_SprintInput.action.performed += OnSprintPressed;
            m_SprintInput.action.canceled += OnSprintReleased;
            //m_SprintInput.action.canceled += OnSprintPressed;
            
            // Se não foi atribuído manualmente, tenta encontrar o Move Provider
            if (m_MoveProvider == null)
                m_MoveProvider = GetComponent<ContinuousMoveProvider>();

            if (m_MoveProvider == null)
            {
                Debug.LogError("Continuous Move Provider não encontrado! Atribua manualmente no Inspector.", this);
                enabled = false;
            }
        }

        private void OnSprintReleased(InputAction.CallbackContext obj)
        {
            m_MoveProvider.moveSpeed = m_WalkSpeed;
            m_simulator.translateXSpeed = m_WalkSpeed;
            m_simulator.translateYSpeed = m_WalkSpeed;
            m_simulator.translateZSpeed = m_WalkSpeed;
        }

        private void OnSprintPressed(InputAction.CallbackContext obj)
        {
            m_MoveProvider.moveSpeed = m_SprintSpeed;
            m_simulator.translateXSpeed = m_SprintSpeed;
            m_simulator.translateYSpeed = m_SprintSpeed;
            m_simulator.translateZSpeed = m_SprintSpeed;
        }

        void OnDisable()
        {
            // Desabilita o input action
            m_SprintInput.action.performed -= OnSprintPressed;
            m_SprintInput.action.canceled -= OnSprintReleased;
        }
    }
}