using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : EnemyEditor
{
    protected SerializedProperty sprite;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        sprite = serializedObject.FindProperty("sprite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(sprite);  
        EditorGUILayout.PropertyField(enemyType);  
        
        if (enemyType.enumValueIndex == (int)Enemy.EnemyType.Range)
        {
            EditorGUILayout.PropertyField(bulletPrefab);
            EditorGUILayout.PropertyField(fireRate);
            EditorGUILayout.PropertyField(attackRange);
        }
        
        EditorGUILayout.PropertyField(maxHp);   
        EditorGUILayout.PropertyField(attackDamage);
        EditorGUILayout.PropertyField(maxSpeed);
        EditorGUILayout.PropertyField(minSpeed);
        EditorGUILayout.PropertyField(turnDirectionDamp);
        EditorGUILayout.PropertyField(distanceThreshold);
        EditorGUILayout.PropertyField(deadParticleSystem);
        
        
        serializedObject.ApplyModifiedProperties();
    }
}

