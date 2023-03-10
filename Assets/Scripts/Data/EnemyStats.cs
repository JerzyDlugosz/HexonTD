using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public float damage;
    public float reward;
    public float spawnSpeed;

    public int enemyId;

    public Slider hpSlider;

    public EnemyTypes enemyType;
    public int spawnNumber;

    [Range(0.01f, 10)]
    public float armor = 1;
}


//[CustomEditor(typeof(EnemyStats))]
//public class EnemyStatsEditor : Editor
//{
//    void OnInspectorGUI()
//    {
//        EnemyStats enemyStats = (EnemyStats)target;
        
//        if (enemyStats.enemyType == EnemyTypes.Multiply)
//        {
//            enemyStats.spawnNumber = EditorGUILayout.IntField("int", enemyStats.spawnNumber);
//        }
//    }
//}