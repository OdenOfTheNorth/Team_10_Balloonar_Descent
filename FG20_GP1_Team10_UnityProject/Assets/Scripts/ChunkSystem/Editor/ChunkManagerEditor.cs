using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ChunkManager))]
public class ChunkManagerEditor : Editor
{
    private const string spawnChanceHelpText = "The chance for a specific chunk to spawn is based on how high its " +
                                               "spawn chance value is in relation to the sum of all chunks spawn " +
                                               "chance values. When a chunk is spawned, its spawn chance value is " +
                                               "reduced to 0 and all other chunks have their spawn chance value " +
                                               "increased. If a chunk has a spawn chance value of 0 when a different " +
                                               "chunk is spawned, its spawn chance value will be set to the base spawn " +
                                               "chance. If a chunk has a spawn chance value other than 0 when a " +
                                               "different chunk is spawned, its spawn chance value will increase by " +
                                               "the spawn chance increment value.";
    
    private SerializedProperty baseSpawnChanceProperty;
    private SerializedProperty chunkQueueProperty;
    private ReorderableList chunkQueueReorderable;

    private bool chunkQueueContainsNull = false;

    private void OnEnable()
    {
        baseSpawnChanceProperty = serializedObject.FindProperty("baseSpawnChance");
        chunkQueueProperty = serializedObject.FindProperty("chunkQueue");
        chunkQueueReorderable = new ReorderableList(serializedObject, chunkQueueProperty, true, true, true, true);

        chunkQueueReorderable.drawHeaderCallback += DrawChunkQueueHeader;
        chunkQueueReorderable.drawElementCallback += DrawChunkQueueElement;
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty property = serializedObject.GetIterator();
        bool goOn = true;
        
        #region Boilerplate for iteration of SerializedProperty
        property.Next(true);
        property.NextVisible(true);
        goOn = property.NextVisible(false);
        #endregion

        while (goOn)
        {
            if (SerializedProperty.EqualContents(property, chunkQueueProperty))
            {
                //EditorGUILayout.PropertyField(property);
                chunkQueueReorderable.DoLayoutList();

                if (chunkQueueContainsNull)
                {
                    EditorGUILayout.HelpBox("Chunk queue currently contains one or more null (\"none\")-elements, which will cause fatal errors", MessageType.Error);
                }
                chunkQueueContainsNull = false;
            }
            else
            {
                if (SerializedProperty.EqualContents(property, baseSpawnChanceProperty))
                {
                    EditorGUILayout.HelpBox(spawnChanceHelpText, MessageType.Info);
                }
                EditorGUILayout.PropertyField(property);
            }

            goOn = property.NextVisible(false);
        }

        if (serializedObject.hasModifiedProperties)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void DrawChunkQueueHeader(Rect rect)
    {
        EditorGUI.PrefixLabel(rect, new GUIContent("Chunk queue"));
    }

    private void DrawChunkQueueElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = chunkQueueProperty.GetArrayElementAtIndex(index);

        rect.y += 1.0f;
        rect.height -= 2.0f;
        EditorGUI.PropertyField(rect, element, new GUIContent("Queue position: " + index));

        if (element.objectReferenceValue == null)
        {
            chunkQueueContainsNull = true;
        }
    }
    
    
}
