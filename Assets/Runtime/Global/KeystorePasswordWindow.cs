#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class KeystorePasswordWindow : EditorWindow
{
    private string keystorePass = "";
    private string keyAliasName = "";
    private string keyAliasPass = "";

    private const string KeystorePassKey = "KeystorePassword";
    private const string KeyAliasNameKey = "KeyAliasName";
    private const string KeyAliasPassKey = "KeyAliasPass";

    [MenuItem("NFTGCO/Keystore Password Setter")]
    public static void ShowWindow()
    {
        GetWindow<KeystorePasswordWindow>("Keystore Password Setter");
    }

    private void OnEnable()
    {
        keystorePass = EditorPrefs.GetString(KeystorePassKey, "NFTGCODEV");
        keyAliasName = EditorPrefs.GetString(KeyAliasNameKey, "nftgco");
        keyAliasPass = EditorPrefs.GetString(KeyAliasPassKey, "NFTGCODEV");
    }

    private void OnGUI()
    {
        GUILayout.Label("Set Android Keystore Passwords", EditorStyles.boldLabel);

        keystorePass = EditorGUILayout.TextField("Keystore Password", keystorePass);
        keyAliasName = EditorGUILayout.TextField("Key Alias Name", keyAliasName);
        keyAliasPass = EditorGUILayout.TextField("Key Alias Password", keyAliasPass);

        if (GUILayout.Button("Set Passwords"))
        {
            SetKeystorePasswords();
        }
    }

    private void SetKeystorePasswords()
    {
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyAliasName;
        PlayerSettings.Android.keyaliasPass = keyAliasPass;

        EditorPrefs.SetString(KeystorePassKey, keystorePass);
        EditorPrefs.SetString(KeyAliasNameKey, keyAliasName);
        EditorPrefs.SetString(KeyAliasPassKey, keyAliasPass);
        
        Debug.Log($"{nameof(KeystorePasswordWindow)}: Keystore passwords set successfully!");
    }
}
#endif