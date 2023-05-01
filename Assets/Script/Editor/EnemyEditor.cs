using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    protected SerializedProperty enemyType;
    protected SerializedProperty bulletPrefab;
    protected SerializedProperty fireRate;
    protected SerializedProperty attackRange;
    protected SerializedProperty maxHp;
    protected SerializedProperty attackDamage;
    protected SerializedProperty maxSpeed;
    protected SerializedProperty minSpeed;
    protected SerializedProperty turnDirectionDamp;
    protected SerializedProperty distanceThreshold;
    protected SerializedProperty deadParticleSystem;
    
    protected virtual void OnEnable()
    {
        enemyType = serializedObject.FindProperty("enemyType");
        maxHp = serializedObject.FindProperty("maxHp");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        fireRate = serializedObject.FindProperty("fireRate");
        attackRange = serializedObject.FindProperty("attackRange");
        attackDamage = serializedObject.FindProperty("attackDamage");
        maxSpeed = serializedObject.FindProperty("maxSpeed");
        minSpeed = serializedObject.FindProperty("minSpeed");
        turnDirectionDamp = serializedObject.FindProperty("turnDirectionDamp");
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
        EditorGUILayout.PropertyField(turnDirectionDamp);
        EditorGUILayout.PropertyField(distanceThreshold);
        EditorGUILayout.PropertyField(deadParticleSystem);
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
