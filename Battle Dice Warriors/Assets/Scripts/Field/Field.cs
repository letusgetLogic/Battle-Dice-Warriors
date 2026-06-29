using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2Int Index { get; private set; }
    public GameObject Obstacle { get; private set; }


    /// <summary>
    /// Sets the field index and the name.
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(Vector2Int index)
    {
        this.Index = index;
        gameObject.name = $"Field {index.x} / {index.y}";
    }

    /// <summary> 
    /// Sets the reference to obstacle. 
    /// </summary>
    /// <param name="obstacle"></param>
    /// <exception cref="System.Exception"></exception>
    public void SetOstacle(GameObject obstacle)
    {
        if (Obstacle != null)
            Debug.Log($"! Exception: Field {Index.x} / {Index.y} has {obstacle.name}");

        Obstacle = obstacle;
    }

    /// <summary>
    /// Sets the obstacle to null.
    /// </summary>
    public void SetOstacleNull()
    {
        if (Obstacle == null)
            return;

        Obstacle = null;
    }

    /// <summary>
    /// Gets the enemy character.
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    public GameObject EnemyObject(PlayerType currentPlayer)
    {
        if (Obstacle == null)
            return null;

        if (!Obstacle.CompareTag("Character"))
            return null;

        var character = Obstacle.GetComponent<Character>();
        if (character.Player.PlayerType == currentPlayer)
            return null;

        return Obstacle;
    }

    /// <summary>
    /// Sets the component enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public void SetComponentEnabled(Component component, bool value)
    {
        if (component is Behaviour behaviour)
        {
            behaviour.enabled = value;
        }
    }
}

