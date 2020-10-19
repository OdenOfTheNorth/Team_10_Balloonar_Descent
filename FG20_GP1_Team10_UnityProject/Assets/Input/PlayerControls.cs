// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerOne"",
            ""id"": ""168c8df1-a6aa-4d8a-92d3-664345f1b0d1"",
            ""actions"": [
                {
                    ""name"": ""WindControl"",
                    ""type"": ""Value"",
                    ""id"": ""f26a6714-236e-464a-b886-8f7eb3d1b713"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TogglePickup"",
                    ""type"": ""Button"",
                    ""id"": ""fab225fa-676f-43d6-ac82-27814b0200be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuitApplication"",
                    ""type"": ""Button"",
                    ""id"": ""dca6e430-2376-4f12-be8b-42c3fb2dc97f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""958a06f4-9116-46ac-940c-1b9d448d4db1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc037060-eecb-49ed-af7a-3cc778f8b692"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6e914899-064c-41a8-8e95-c04c6990e5f3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""962d7e8c-37b0-4b60-9273-22a23d6772ee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""984697a4-1ff6-4995-9639-217fe9855a50"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""5c91c003-386d-4332-86fc-8d106086b139"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""06c5ab94-bc53-4c75-86b2-1aed2a2bde7e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8f982d79-8afd-4f75-a0f1-733111088c13"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""24c1fc3c-220e-419a-ab6b-41877c044d57"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4953ed4b-c6f8-48ac-ac0a-8ba0e17275e1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WindControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""019c9caf-d628-40fd-958e-94f9b8c1a50b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""072e4a87-886a-4a74-8b2b-ce44ae837f75"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuitApplication"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerOne
        m_PlayerOne = asset.FindActionMap("PlayerOne", throwIfNotFound: true);
        m_PlayerOne_WindControl = m_PlayerOne.FindAction("WindControl", throwIfNotFound: true);
        m_PlayerOne_TogglePickup = m_PlayerOne.FindAction("TogglePickup", throwIfNotFound: true);
        m_PlayerOne_QuitApplication = m_PlayerOne.FindAction("QuitApplication", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerOne
    private readonly InputActionMap m_PlayerOne;
    private IPlayerOneActions m_PlayerOneActionsCallbackInterface;
    private readonly InputAction m_PlayerOne_WindControl;
    private readonly InputAction m_PlayerOne_TogglePickup;
    private readonly InputAction m_PlayerOne_QuitApplication;
    public struct PlayerOneActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerOneActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @WindControl => m_Wrapper.m_PlayerOne_WindControl;
        public InputAction @TogglePickup => m_Wrapper.m_PlayerOne_TogglePickup;
        public InputAction @QuitApplication => m_Wrapper.m_PlayerOne_QuitApplication;
        public InputActionMap Get() { return m_Wrapper.m_PlayerOne; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerOneActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerOneActions instance)
        {
            if (m_Wrapper.m_PlayerOneActionsCallbackInterface != null)
            {
                @WindControl.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnWindControl;
                @WindControl.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnWindControl;
                @WindControl.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnWindControl;
                @TogglePickup.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnTogglePickup;
                @TogglePickup.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnTogglePickup;
                @TogglePickup.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnTogglePickup;
                @QuitApplication.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnQuitApplication;
                @QuitApplication.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnQuitApplication;
                @QuitApplication.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnQuitApplication;
            }
            m_Wrapper.m_PlayerOneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WindControl.started += instance.OnWindControl;
                @WindControl.performed += instance.OnWindControl;
                @WindControl.canceled += instance.OnWindControl;
                @TogglePickup.started += instance.OnTogglePickup;
                @TogglePickup.performed += instance.OnTogglePickup;
                @TogglePickup.canceled += instance.OnTogglePickup;
                @QuitApplication.started += instance.OnQuitApplication;
                @QuitApplication.performed += instance.OnQuitApplication;
                @QuitApplication.canceled += instance.OnQuitApplication;
            }
        }
    }
    public PlayerOneActions @PlayerOne => new PlayerOneActions(this);
    public interface IPlayerOneActions
    {
        void OnWindControl(InputAction.CallbackContext context);
        void OnTogglePickup(InputAction.CallbackContext context);
        void OnQuitApplication(InputAction.CallbackContext context);
    }
}
