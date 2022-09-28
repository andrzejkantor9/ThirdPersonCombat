using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using System;

//TODO check in runtime if conditional methods work
//TODO expose logCategory and frequency to editor / text file / in cleaner way
    //and move enums out of there
//TODO add option to configure text colors as well
//TODO move OnGui to here
//TODO TryGetUnityObject - try to make it return generic typ
//TODO GetGameObjectName - check if unityEngine.object can be passed instead

//TODO create self intantiating debug menu that allows changing flags in runtime
//TODO add flag to enable debug logging in production
    //with warning confirm window
//TODO make log not needing to be on game object - spawn itself instead?
//TODO try getting variable name without passing string name
namespace TPCombat.Debug
{
    public enum LogCategory : byte
    {
        Animation,
        Widgets,
        HUD,
        Combat,
        Camera,
        Enemies,
        Managers,
        Materials,
        Menus,
        Music,
        Voices,
        SFX,
        VFX,
        Collisions,
        Terrain,
        Saving,
        Input,
        Movement,
        Settings,
        Inventory,
        Abilities,
        ResourcesSystems,
        NPCs
    }

    public enum LogFrequency : byte
    {
        EveryFrame,
        MostFrames,
        Frequent,
        Regular,
        Rare
    }

    public enum LogDetails : byte
    {
        Deep,
        Medium,
        Basic
    }

    public class CustomLogger : MonoBehaviour
    {
        private const int IM_GUI_FONT_SIZE = 25;

    #region LogConfig
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        private static readonly Dictionary<LogCategory, bool> _logCategoryEnabled = new Dictionary<LogCategory, bool>()
        {
            {LogCategory.Menus, true},
            {LogCategory.Widgets, true},
            {LogCategory.HUD, true},
            {LogCategory.Camera, true},
            {LogCategory.Music, true},
            {LogCategory.Voices, true},
            {LogCategory.SFX, true},
            {LogCategory.VFX, true},
            {LogCategory.Materials, true},
            {LogCategory.Collisions, true},
            {LogCategory.Terrain, true},
            {LogCategory.Saving, true},
            {LogCategory.Input, true},
            {LogCategory.Movement, true},
            {LogCategory.Settings, true},
            {LogCategory.Inventory, true},
            {LogCategory.Abilities, true},
            {LogCategory.ResourcesSystems, true},
            {LogCategory.Enemies, true},
            {LogCategory.NPCs, true},
            {LogCategory.Animation, true},
            {LogCategory.Managers, true},
            {LogCategory.Combat, true}
        };

        private static readonly Dictionary<LogFrequency, bool> _logFrequencyEnabled = new Dictionary<LogFrequency, bool>()
        {
            {LogFrequency.EveryFrame, true},
            {LogFrequency.MostFrames, true},
            {LogFrequency.Frequent, true},
            {LogFrequency.Regular, true},
            {LogFrequency.Rare, true}
        };

        private static readonly Dictionary<LogDetails, bool> _logDetailsEnabled = new Dictionary<LogDetails, bool>()
        {
            {LogDetails.Deep, true},
            {LogDetails.Medium, true},
            {LogDetails.Basic, true}           
        };
#endif
    #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
    #region ImGUI
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        private GUIStyle textStyle = new GUIStyle();

        static string[] DebugMessages = new string[20];
        static int lastMessageIndex =0;
        static float lastTime = 0f;

        private void Awake()
        {
            textStyle.fontSize = IM_GUI_FONT_SIZE;
        }

        // private void OnGUI()
        // {
        //     ProcessGUIDebugLogs();
        // }

        private static void AddGUIMessage(string messages)
        {
            if(lastMessageIndex == 19)
            {
                // TempMessages = DebugMessages;
                for (int i = 0;i < DebugMessages.Length-1 ;i++)
                {
                    // System.Array.Copy(TempMessages, i+1, DebugMessages, i, TempMessages.Length - i - 1);
                    DebugMessages[i] = DebugMessages[i+1];
                }
            }
            DebugMessages[lastMessageIndex] = messages;
            ++lastMessageIndex;
            lastMessageIndex = Mathf.Clamp(lastMessageIndex, 0, 19);
            lastTime = Time.time;
        }

        public static void ProcessGUIDebugLogs(GUIStyle guiStyle)
        {
            if(lastTime + 5f <= Time.time)
            {
                if(DebugMessages.Length > 0)
                {
                    System.Array.Clear(DebugMessages, 0, DebugMessages.Length);
                    lastMessageIndex=0;
                }
            }
            else
            {
                for(int i=0; i<DebugMessages.Length; ++i)
                {
                    GUI.Label(new Rect(5, 30 + i*20, 1000, 25), DebugMessages[i], guiStyle);
                }
            }
        }
#endif
    #endregion

    #region Logging
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void Log(
            string message,
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true, 
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
            => Log(message, (object)null, logCategory, logFrequency, logDetails, logToScreen, stackInfo, filePath);

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void Log<T>(
            string message, 
            T contextObject, 
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true, 
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
        {
            if(!_logCategoryEnabled[logCategory] || !_logFrequencyEnabled[logFrequency] || !_logDetailsEnabled[logDetails]) 
                return;

            UnityEngine.Object unityObject = TryGetUnityObject(contextObject);
            if(stackInfo)
            {
                filePath = GetFileFromPath(filePath);

                UnityEngine.Debug.Log($"{message}, <color=teal>object: {unityObject?.name}, called at: {filePath}</color>", unityObject);
            }
            else
            {
                UnityEngine.Debug.Log($"{message}, <color=teal>object: {unityObject?.name}</color>", unityObject);
            }

            if(logToScreen)
                AddGUIMessage(message);
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void LogWarning(
            string message,
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true, 
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
            => LogWarning(message, (object)null, logCategory, logFrequency, logDetails, logToScreen, stackInfo, filePath);

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void LogWarning<T>(
            string message,
            T contextObject, 
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true,
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
        {
            if(!_logCategoryEnabled[logCategory] || !_logFrequencyEnabled[logFrequency] || !_logDetailsEnabled[logDetails]) return;

            UnityEngine.Object unityObject = TryGetUnityObject(contextObject);
            string warningMessage = "<color=yellow>" + message + "</color>";
            if(stackInfo)
            {
                filePath = GetFileFromPath(filePath);

                UnityEngine.Debug.LogWarning($"{warningMessage}, <color=teal>object: {unityObject?.name}, called at: {filePath}</color>", unityObject);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"{warningMessage}, <color=teal>object: {unityObject?.name}</color>", unityObject);
            }

            if(logToScreen)
                AddGUIMessage(message);
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void LogError(
            string message,
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true, 
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
            => LogError(message, (object)null, logCategory, logFrequency, logDetails, logToScreen, stackInfo, filePath);

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void LogError<T>(
            string message,
            T contextObject, 
            LogCategory logCategory, 
            LogFrequency logFrequency,
            LogDetails logDetails,
            bool logToScreen = true, 
            bool stackInfo = true,
            [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
        {
            if(!_logCategoryEnabled[logCategory] || !_logFrequencyEnabled[logFrequency] || !_logDetailsEnabled[logDetails]) return;

            UnityEngine.Object unityObject = TryGetUnityObject(contextObject);
            string warningMessage = "<color=red>" + message + "</color>";
            if(stackInfo)
            {
                filePath = GetFileFromPath(filePath);

                UnityEngine.Debug.LogError($"{warningMessage}, <color=teal>object: {unityObject?.name}, called at: {filePath}</color>", unityObject);
            }
            else
            {
                UnityEngine.Debug.LogError($"{warningMessage}, <color=teal>object: {unityObject?.name}</color>", unityObject);
            }
            
            if(logToScreen)
                AddGUIMessage(message);
        }
        #endregion

        #region Assertions
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void AssertNotNull<T, U>(T variableToCheck, string variableToCheckName, U scriptThis) 
            where T : Component
        {
            string scriptName = GetScriptNameWithoutNamespace(scriptThis);
            string gameObjectName = GetGameObjectName(scriptThis);
            
            UnityEngine.Assertions.Assert.IsNotNull
            (
                variableToCheck, 
                $"{variableToCheckName} is null, <color=teal>Object: {gameObjectName}, Script: {scriptName}</color>"
            );
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void AssertTrue<T>(bool conditionValue, string messageIfFalse, T scriptThis) 
        {
            string scriptName = GetScriptNameWithoutNamespace(scriptThis);
            string gameObjectName = GetGameObjectName(scriptThis);
            
            UnityEngine.Assertions.Assert.IsTrue
            (
                conditionValue, 
                $"{messageIfFalse}, <color=teal>Object: {gameObjectName}, Script: {scriptName}</color>"
            );
        }

        private static string GetFileFromPath(string filePath)
        {
            filePath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            return filePath;
        }

        private static string GetScriptNameWithoutNamespace(System.Object script)
        {
            string scriptName = script.GetType().ToString();
            scriptName = scriptName.Substring(scriptName.LastIndexOf('.') + 1);

            return scriptName;
        }

        private static UnityEngine.Object TryGetUnityObject<T>(T possibleUnityObject)
        {
            if(possibleUnityObject == null)
                return null;

            return possibleUnityObject.GetType().IsSubclassOf(typeof(UnityEngine.Object)) ? (possibleUnityObject as UnityEngine.Object) : null;
        }

        private static string GetGameObjectName(System.Object script)
        {
           return script.GetType().IsSubclassOf(typeof(MonoBehaviour)) ? (script as MonoBehaviour).gameObject.name : "not a game object";
        }
    #endregion
    }
}
