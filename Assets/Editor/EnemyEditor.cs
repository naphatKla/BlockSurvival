using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    SerializedProperty enemyType;
    SerializedProperty bulletPrefab;
    SerializedProperty fireRate;
    SerializedProperty attackRange;
    SerializedProperty maxHp;
    SerializedProperty attackDamage;
    SerializedProperty maxSpeed;
    SerializedProperty minSpeed;
    SerializedProperty distanceThreshold;
    SerializedProperty deadParticleSystem;
    
    private void OnEnable()
    {
        enemyType = serializedObject.FindProperty("enemyType");
        maxHp = serializedObject.FindProperty("maxHp");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        fireRate = serializedObject.FindProperty("fireRate");
        attackRange = serializedObject.FindProperty("attackRange");
        attackDamage = serializedObject.FindProperty("attackDamage");
        maxSpeed = serializedObject.FindProperty("maxSpeed");
        minSpeed = serializedObject.FindProperty("minSpeed");
        distanceThreshold = serializedObject.FindProperty("distanceThreshold");
        deadParticleSystem = serializedObject.FindProperty("deadParticleSystem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
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
        EditorGUILayout.PropertyField(distanceThreshold);
        EditorGUILayout.PropertyField(deadParticleSystem);
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
