using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContractConfig))]
public class ContractConfigInspector : Editor
{
    ContractConfig config;
    // Basic Details
    SerializedProperty contractTitle;
    SerializedProperty contractType;
    SerializedProperty contractDetails;
    SerializedProperty runDistance;
    SerializedProperty associatedFaction;
    SerializedProperty unlockPrerequisite;

    // Non-friendlies Parameters
    SerializedProperty difficultyLevel;

    SerializedProperty securityLevel;
    SerializedProperty securityUnits;
    SerializedProperty hasSecurityBoss;
    SerializedProperty potentialSecurityBoss;

    SerializedProperty creatureLevel;
    SerializedProperty creatures;
    SerializedProperty hasCreatureBoss;
    SerializedProperty potentialCreatureBoss;

    SerializedProperty debrisLevel;
    SerializedProperty debris;

    // Rewards
    SerializedProperty lootLevel;
    SerializedProperty creditRewardLevel;
    SerializedProperty lootDrops;
    SerializedProperty specialLootDrops;

    // Misc.
    SerializedProperty pickUps;
    SerializedProperty pickUpDropRates;

    private void OnEnable()
    {
        // Basic Details
        contractTitle = serializedObject.FindProperty("contractTitle");
        contractType = serializedObject.FindProperty("contractType");
        contractDetails = serializedObject.FindProperty("contractDetails");
        runDistance = serializedObject.FindProperty("runDistance");
        associatedFaction = serializedObject.FindProperty("associatedFaction");
        unlockPrerequisite = serializedObject.FindProperty("unlockPrerequisite");

        // Non-friendlies Parameters
        difficultyLevel = serializedObject.FindProperty("difficultyLevel");

        securityLevel = serializedObject.FindProperty("securityLevel");
        securityUnits = serializedObject.FindProperty("securityUnits");
        hasSecurityBoss = serializedObject.FindProperty("hasSecurityBoss");
        potentialSecurityBoss = serializedObject.FindProperty("potentialSecurityBoss");

        creatureLevel = serializedObject.FindProperty("creatureLevel");
        creatures = serializedObject.FindProperty("creatures");
        hasCreatureBoss = serializedObject.FindProperty("hasCreatureBoss");
        potentialCreatureBoss = serializedObject.FindProperty("potentialCreatureBoss");

        debrisLevel = serializedObject.FindProperty("debrisLevel");
        debris = serializedObject.FindProperty("debris");

        // Rewards
        lootLevel = serializedObject.FindProperty("lootLevel");
        creditRewardLevel = serializedObject.FindProperty("creditRewardLevel");
        lootDrops = serializedObject.FindProperty("lootDrops");
        specialLootDrops = serializedObject.FindProperty("specialLootDrops");

        // Pick-up drop rate properties
        pickUps = serializedObject.FindProperty("pickUps");
        pickUpDropRates = serializedObject.FindProperty("pickUpDropRates");

    }

    public override void OnInspectorGUI()
    {

        // Basic Details
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(contractTitle);
        EditorGUILayout.PropertyField(contractType);
        EditorGUILayout.PropertyField(contractDetails, GUILayout.MinHeight(150));
        EditorGUILayout.PropertyField(runDistance);
        EditorGUILayout.PropertyField(associatedFaction);
        EditorGUILayout.PropertyField(unlockPrerequisite);
        if (unlockPrerequisite.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < unlockPrerequisite.arraySize; i++)
            {
                string label =
                    ObjectNames.NicifyVariableName(((UnlockPrerequisiteType)i).ToString());
                EditorGUILayout.PropertyField(
                    unlockPrerequisite.GetArrayElementAtIndex(i),
                    new GUIContent(label)
                    );
            }
            EditorGUI.indentLevel--;
        }

        // Non-Friendlies Parameters
        EditorGUILayout.PropertyField(difficultyLevel);

        //// Security Units
        EditorGUILayout.PropertyField(securityLevel);
        EditorGUILayout.PropertyField(securityUnits);
        if (securityUnits.isExpanded)
        {
            ShowExpandedBasicList(securityUnits);
        }
        EditorGUILayout.PropertyField(hasSecurityBoss);
        if (hasSecurityBoss.boolValue)
        {
            EditorGUILayout.PropertyField(potentialSecurityBoss);
            if (potentialSecurityBoss.isExpanded)
            {
                ShowExpandedBasicList(potentialSecurityBoss);
            }
        }

        //// Creatures
        EditorGUILayout.PropertyField(creatureLevel);
        EditorGUILayout.PropertyField(creatures);
        if (creatures.isExpanded)
        {
            ShowExpandedBasicList(creatures);
        }
        EditorGUILayout.PropertyField(hasCreatureBoss);
        if (hasCreatureBoss.boolValue)
        {
            EditorGUILayout.PropertyField(potentialCreatureBoss);
            if (potentialCreatureBoss.isExpanded)
            {
                ShowExpandedBasicList(potentialCreatureBoss);
            }
        }

        //// Debris
        EditorGUILayout.PropertyField(debrisLevel);
        EditorGUILayout.PropertyField(debris);
        if (debris.isExpanded)
        {
            ShowExpandedBasicList(debris);
        }

        // Rewards
        EditorGUILayout.PropertyField(lootLevel);
        EditorGUILayout.PropertyField(creditRewardLevel);
        EditorGUILayout.PropertyField(lootDrops);
        if (lootDrops.isExpanded)
        {
            ShowExpandedBasicList(lootDrops);
        }
        EditorGUILayout.PropertyField(specialLootDrops);
        if (specialLootDrops.isExpanded)
        {
            ShowExpandedBasicList(specialLootDrops);
        }


        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        // Misc
        DrawArrayWithDropRates(pickUps, pickUpDropRates);
    }

    private void ShowExpandedBasicList(SerializedProperty list)
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(
                list.GetArrayElementAtIndex(i),
                new GUIContent($"Element {i}")
            );
        }
        EditorGUI.indentLevel--;
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
        if (GUILayout.Button(new GUIContent("-", "Delete"), EditorStyles.miniButton))
        {
            int prevSize = list.arraySize;
            list.DeleteArrayElementAtIndex(index - 1);      // Clears index only
            if (list.arraySize == prevSize)
            {
                list.DeleteArrayElementAtIndex(index - 1);  // Deletes index
            }
        }
        if (GUILayout.Button(new GUIContent("+", "Duplicate"), EditorStyles.miniButton))
        {
            list.InsertArrayElementAtIndex(index);
            list.DeleteArrayElementAtIndex(index);
        }
        EditorGUILayout.EndHorizontal();
    }
}
