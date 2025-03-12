using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class _DB_EditorMethods : MonoBehaviour
{
    [MenuItem("_DEBUG/InvokeCoinCollect")]
    static void invokeCoinCollect()
    {
        QuestHandler.Instance.AddProgress("Collect", 1);
    }
    [MenuItem("_DEBUG/InvokeEnemyKill")]
    static void invokeEnemyKill()
    {
        QuestHandler.Instance.AddProgress("Kill", 1);
    }
    
}
