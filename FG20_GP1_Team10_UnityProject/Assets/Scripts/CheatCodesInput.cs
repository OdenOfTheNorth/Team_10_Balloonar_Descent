// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CheatCodesInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CheatCodesInputScript : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CheatCodesInputScript()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CheatCodesInput"",
    ""maps"": [
        {
            ""name"": ""Cheats"",
            ""id"": ""7f05fb89-e1b0-4790-b48a-51ff8d4d92ff"",
            ""actions"": [
                {
                    ""name"": ""InfiniteHealth"",
                    ""type"": ""Button"",
                    ""id"": ""97f5bb05-7ee2-4f8f-bc7b-f62e8c2585e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoclipMode"",
                    ""type"": ""Button"",
                    ""id"": ""dfc6ec95-11df-4194-9b7c-80f782dac41d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleChunkCheatVisibility"",
                    ""type"": ""Button"",
                    ""id"": ""271fd6fa-3b02-46eb-9572-b2d0451d483e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cd3b1c70-cf22-420c-8261-c9717e67b66c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InfiniteHealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30e2262b-553c-4011-b487-eab09024dd60"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoclipMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0a11804-f1fc-4a1e-b604-02e043fa9d4e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleChunkCheatVisibility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Cheats
        m_Cheats = asset.FindActionMap("Cheats", throwIfNotFound: true);
        m_Cheats_InfiniteHealth = m_Cheats.FindAction("InfiniteHealth", throwIfNotFound: true);
        m_Cheats_NoclipMode = m_Cheats.FindAction("NoclipMode", throwIfNotFound: true);
        m_Cheats_ToggleChunkCheatVisibility = m_Cheats.FindAction("ToggleChunkCheatVisibility", throwIfNotFound: true);
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

    // Cheats
    private readonly InputActionMap m_Cheats;
    private ICheatsActions m_CheatsActionsCallbackInterface;
    private readonly InputAction m_Cheats_InfiniteHealth;
    private readonly InputAction m_Cheats_NoclipMode;
    private readonly InputAction m_Cheats_ToggleChunkCheatVisibility;
    public struct CheatsActions
    {
        private @CheatCodesInputScript m_Wrapper;
        public CheatsActions(@CheatCodesInputScript wrapper) { m_Wrapper = wrapper; }
        public InputAction @InfiniteHealth => m_Wrapper.m_Cheats_InfiniteHealth;
        public InputAction @NoclipMode => m_Wrapper.m_Cheats_NoclipMode;
        public InputAction @ToggleChunkCheatVisibility => m_Wrapper.m_Cheats_ToggleChunkCheatVisibility;
        public InputActionMap Get() { return m_Wrapper.m_Cheats; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatsActions set) { return set.Get(); }
        public void SetCallbacks(ICheatsActions instance)
        {
            if (m_Wrapper.m_CheatsActionsCallbackInterface != null)
            {
                @InfiniteHealth.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnInfiniteHealth;
                @InfiniteHealth.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnInfiniteHealth;
                @InfiniteHealth.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnInfiniteHealth;
                @NoclipMode.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoclipMode;
                @NoclipMode.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoclipMode;
                @NoclipMode.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoclipMode;
                @ToggleChunkCheatVisibility.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleChunkCheatVisibility;
                @ToggleChunkCheatVisibility.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleChunkCheatVisibility;
                @ToggleChunkCheatVisibility.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleChunkCheatVisibility;
            }
            m_Wrapper.m_CheatsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InfiniteHealth.started += instance.OnInfiniteHealth;
                @InfiniteHealth.performed += instance.OnInfiniteHealth;
                @InfiniteHealth.canceled += instance.OnInfiniteHealth;
                @NoclipMode.started += instance.OnNoclipMode;
                @NoclipMode.performed += instance.OnNoclipMode;
                @NoclipMode.canceled += instance.OnNoclipMode;
                @ToggleChunkCheatVisibility.started += instance.OnToggleChunkCheatVisibility;
                @ToggleChunkCheatVisibility.performed += instance.OnToggleChunkCheatVisibility;
                @ToggleChunkCheatVisibility.canceled += instance.OnToggleChunkCheatVisibility;
            }
        }
    }
    public CheatsActions @Cheats => new CheatsActions(this);
    public interface ICheatsActions
    {
        void OnInfiniteHealth(InputAction.CallbackContext context);
        void OnNoclipMode(InputAction.CallbackContext context);
        void OnToggleChunkCheatVisibility(InputAction.CallbackContext context);
    }
}
