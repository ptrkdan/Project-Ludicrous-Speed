using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContractConfig))]
public class ContractConfigInspector : Editor
{
    ContractConfig config;
    SerializedProperty contractTitle;
    SerializedProperty contractDetails;
    SerializedProperty runDistance;
    SerializedProperty contractLootLevel;
    SerializedProperty contractDifficultyLevel;

    SerializedProperty pickUps;
    SerializedProperty pickUpDropRates;
    SerializedProperty contractRewards;
    SerializedProperty contractRewardDropRates;

    private void OnEnable()
    {
        contractTitle = serializedObject.FindProperty("contractTitle");
        contractDetails = serializedObject.FindProperty("contractDetails");
        runDistance = serializedObject.FindProperty("runDistance");

        // Run Parameter properties
        contractLootLevel = serializedObject.FindProperty("contractLootLevel");
        contractDifficultyLevel = serializedObject.FindProperty("contractDifficultyLevel");

        // Pick-up drop rate properties
        pickUps = serializedObject.FindProperty("availablePickUps");
        pickUpDropRates = serializedObject.FindProperty("availablePickUpDropRates");

        // Contract reward drop rate properties
        contractRewards = serializedObject.FindProperty("contractRewards");
        contractRewardDropRates = serializedObject.FindProperty("contractRewardDropRates");

    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(contractTitle);

        EditorGUILayout.PropertyField(contractDetails, GUILayout.MinHeight(150));
        EditorGUILayout.PropertyField(runDistance);

        // Run Parameters
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Run Parameters", EditorStyles.boldLabel);
        EditorGUILayout.IntSlider(contractLootLevel, 0, 10, new GUIContent("Loot Level"));
        EditorGUILayout.IntSlider(contractDifficultyLevel, 0, 10, new GUIContent("Difficulty Level"));

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pick-Ups", EditorStyles.boldLabel);
        DrawArrayWithDropRates(pickUps, pickUpDropRates);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Contract Rewards", EditorStyles.boldLabel);
        DrawArrayWithDropRates(contractRewards, contractRewardDropRates);

        //EditorGUILayout.PropertyField(pickUpDropRates);
        //if (pickUpDropRates.isExpanded)
        //{
        //    EditorGUI.indentLevel++;
        //    for (int i = 0; i < pickUpDropRates.arraySize; i++)
        //    {
        //        EditorGUILayout.Slider(pickUpDropRates.GetArrayElementAtIndex(i), 0f, 100f,
        //            new GUIContent("Drop Rate " + i.ToString()));
        //    }
        //    EditorGUI.indentLevel--;
        //}

        //// Contract Rewards
        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("Contract Rewards", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(contractRewards);
        //if (contractRewards.isExpanded)
        //{
        //    EditorGUI.indentLevel++;

        //    EditorGUI.BeginChangeCheck();
        //    EditorGUILayout.PropertyField(contractRewards.FindPropertyRelative("Array.size"));
        //    for (int i = 0; i < contractRewards.arraySize; i++)
        //    {
        //        EditorGUILayout.PropertyField(
        //            contractRewards.GetArrayElementAtIndex(i),
        //            new GUIContent("Pick-up " + i.ToString()));
        //    }

        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        contractRewardDropRates.arraySize = contractRewards.arraySize;
        //    }
        //    EditorGUI.indentLevel--;

        //}

        //EditorGUILayout.PropertyField(contractRewardDropRates);
        //if (contractRewardDropRates.isExpanded)
        //{
        //    EditorGUI.indentLevel++;
        //    for (int i = 0; i < contractRewardDropRates.arraySize; i++)
        //    {
        //        EditorGUILayout.Slider(contractRewardDropRates.GetArrayElementAtIndex(i), 0f, 100f,
        //            new GUIContent("Drop Rate " + i.ToString()));
        //    }
        //    EditorGUI.indentLevel--;
        //}

    }

    private void DrawArrayWithDropRates(SerializedProperty objList, SerializedProperty dropRateList)
    {
        EditorGUILayout.PropertyField(objList);
        if (objList.isExpanded)
        {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(objList.FindPropertyRelative("Array.size"));
            dropRateList.arraySize = objList.arraySize;
            for (int i = 0; i < objList.arraySize; i++)
            {
                EditorGUILayout.PropertyField(objList.GetArrayElementAtIndex(i));

                EditorGUI.indentLevel++;
                EditorGUILayout.Slider(dropRateList.GetArrayElementAtIndex(i), 0f, 100f,
                    new GUIContent("Drop rate:"));
                EditorGUI.indentLevel--;

            }

            ShowArrayControls(objList, objList.arraySize);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }


            EditorGUI.indentLevel--;
        }
    }

    private void ShowArrayControls(SerializedProperty list, int index)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("-", "Delete"), EditorStyles.miniButtonLeft))
        {
            int prevSize = list.arraySize;
            list.DeleteArrayElementAtIndex(index - 1);      // Clears index only
            if (list.arraySize == prevSize)
            {
                list.DeleteArrayElementAtIndex(index - 1);  // Deletes index
            }
        }
        if (GUILayout.Button(new GUIContent("+", "Duplicate"), EditorStyles.miniButtonRight))
        {
            list.InsertArrayElementAtIndex(index);
            list.DeleteArrayElementAtIndex(index);
        }
        EditorGUILayout.EndHorizontal();
    }


    // Drag and Drop tester
    public void DragAndDropGUI<T>(SerializedProperty objArray)
    {
        Event currentEvent = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0f, 20f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, $"+ {typeof(T)}");

        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(currentEvent.mousePosition))
                {
                    return;
                }

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObjs in DragAndDrop.objectReferences)
                    {
                        Debug.Log(draggedObjs);
                    }
                }
                break;
        }
    }
}
