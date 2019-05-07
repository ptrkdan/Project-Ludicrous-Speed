using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ContractConfig))]
public class ContractConfigInspector : Editor
{
    ContractConfig config;
    SerializedProperty pickUps;
    SerializedProperty pickUpDropRates;
    SerializedProperty contractRewards;
    SerializedProperty contractRewardDropRates;

    private void OnEnable()
    {
        config = (ContractConfig)target;
        pickUps = serializedObject.FindProperty("availablePickUps");
        pickUpDropRates = serializedObject.FindProperty("availablePickUpDropRates");
        contractRewards = serializedObject.FindProperty("contractRewards");
        contractRewardDropRates = serializedObject.FindProperty("contractRewardDropRates");
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.TextField("Title", config.GetContractTitle());
        EditorGUILayout.PrefixLabel("Details");
        EditorGUILayout.TextArea(config.GetContractDetails());
        EditorGUILayout.IntField("Run Distance", config.GetRunDistance());

        // Pick Ups
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pick-Ups", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(pickUps);
        if (pickUps.isExpanded)
        {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(pickUps.FindPropertyRelative("Array.size"));
            for (int i = 0; i < pickUps.arraySize; i++)
            {
                EditorGUILayout.PropertyField(
                    pickUps.GetArrayElementAtIndex(i), 
                    new GUIContent("Pick-up " + i.ToString()));
            }

            if (EditorGUI.EndChangeCheck())
            {
                pickUpDropRates.arraySize = pickUps.arraySize;
            }
            EditorGUI.indentLevel--;
            
        }

        EditorGUILayout.PropertyField(pickUpDropRates);
        if (pickUpDropRates.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < pickUpDropRates.arraySize; i++)
            {
                EditorGUILayout.Slider(pickUpDropRates.GetArrayElementAtIndex(i), 0f, 100f,
                    new GUIContent("Drop Rate " + i.ToString()));
            }
            EditorGUI.indentLevel--;
        }

        // Contract Rewards
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Contract Rewards", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(contractRewards);
        if (contractRewards.isExpanded)
        {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(contractRewards.FindPropertyRelative("Array.size"));
            for (int i = 0; i < contractRewards.arraySize; i++)
            {
                EditorGUILayout.PropertyField(
                    contractRewards.GetArrayElementAtIndex(i),
                    new GUIContent("Pick-up " + i.ToString()));
            }

            if (EditorGUI.EndChangeCheck())
            {
                contractRewardDropRates.arraySize = contractRewards.arraySize;
            }
            EditorGUI.indentLevel--;

        }

        EditorGUILayout.PropertyField(contractRewardDropRates);
        if (contractRewardDropRates.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < contractRewardDropRates.arraySize; i++)
            {
                EditorGUILayout.Slider(contractRewardDropRates.GetArrayElementAtIndex(i), 0f, 100f,
                    new GUIContent("Drop Rate " + i.ToString()));
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
