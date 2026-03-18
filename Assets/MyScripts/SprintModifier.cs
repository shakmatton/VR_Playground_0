using System;
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

        private void Start()
        {
            m_MoveProvider.moveSpeed = m_WalkSpeed;
            
            m_simulator.translateXSpeed = m_WalkSpeed;
            m_simulator.translateYSpeed = m_WalkSpeed;
            m_simulator.translateZSpeed = m_WalkSpeed;
        }

        // Continuous Move Provider sofre override dos valores de SprintModifier quando tecla Ctrl é ativada 
        
        private void OnSprintReleased(InputAction.CallbackContext obj)
        {
            // não esquecer: m_MoveProvider (Óculos) / m_simulator (Simulador XR)
            
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

/*
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

namespace MyScripts
{
    public class SprintModifier : MonoBehaviour
    {
        [Header("Referências")] 
        [SerializeField] private ContinuousMoveProvider m_MoveProvider;
        
        [Tooltip("Opcional: Usado para testar velocidade no teclado/simulador.")]
        [SerializeField] private XRInteractionSimulator m_Simulator;

        [Header("Configurações de Velocidade")] 
        [SerializeField] private float m_WalkSpeed = 2.5f;
        [SerializeField] private float m_SprintSpeed = 5f;

        [Header("Input")] 
        [SerializeField] private InputActionReference m_SprintInput;

        void Awake()
        {
            // Tenta encontrar o Move Provider se não estiver atribuído
            if (m_MoveProvider == null)
                m_MoveProvider = GetComponent<ContinuousMoveProvider>();
            
            // Tenta encontrar o simulador se estiver no editor e não estiver atribuído
            if (m_Simulator == null)
                m_Simulator = Object.FindAnyObjectByType<XRInteractionSimulator>();
        }

        void OnEnable()
        {
            if (m_SprintInput != null && m_SprintInput.action != null)
            {
                m_SprintInput.action.Enable(); // ESSENCIAL
                m_SprintInput.action.performed += OnSprintPressed;
                m_SprintInput.action.canceled += OnSprintReleased;
            }

            if (m_MoveProvider == null)
            {
                Debug.LogError("Continuous Move Provider não encontrado!", this);
                enabled = false;
                return;
            }

            // Garante que começa na velocidade de caminhada
            SetSpeed(m_WalkSpeed);
        }

        private void OnSprintPressed(InputAction.CallbackContext obj)
        {
            SetSpeed(m_SprintSpeed);
        }

        private void OnSprintReleased(InputAction.CallbackContext obj)
        {
            SetSpeed(m_WalkSpeed);
        }

        private void SetSpeed(float speed)
        {
            if (m_MoveProvider != null)
                m_MoveProvider.moveSpeed = speed;

            // Aplica ao simulador (Device Simulator) se ele existir na cena
            if (m_Simulator != null)
            {
                m_Simulator.translateXSpeed = speed;                
                m_Simulator.translateYSpeed = speed;
                m_Simulator.translateZSpeed = speed;
            }
        }

        void OnDisable()
        {
            if (m_SprintInput != null && m_SprintInput.action != null)
            {
                m_SprintInput.action.performed -= OnSprintPressed;
                m_SprintInput.action.canceled -= OnSprintReleased;
                m_SprintInput.action.Disable();
            }
        }
    }
}
*/