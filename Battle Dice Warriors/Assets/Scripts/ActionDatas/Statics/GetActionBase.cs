using System;
using UnityEngine;

public static class GetActionBase
{
    /// <summary>
    /// Creates an instance of ActionBase.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ActionBase Create(ActionPanel actionPanel, GameObject characterObject)
    {
        switch (actionPanel.ActionData.ActionType)
        {
            case ActionType.None:
                return null;

            case ActionType.Move:
                return new Movement(actionPanel, characterObject);

            case ActionType.Attack:
                return CreateAttackChild(actionPanel, characterObject);

            case ActionType.Defend:
                return CreateDefendChild(actionPanel, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.Create()");
    }

    /// <summary>
    /// Create an instance of Attack attach to the weapon.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    private static Attack CreateAttackChild(ActionPanel actionPanel, GameObject characterObject)
    {
        switch (actionPanel.ActionData.WeaponType)
        {
            case WeaponType.None:
                throw new System.Exception("GetActionBase.CreateAttackChild() " +
                    "-> data.WeaponType = None");

            case WeaponType.Sword:
                return new MeleeBehaviour(actionPanel, characterObject, WeaponType.Sword);

            case WeaponType.Bow:
                return new RangeBehaviour(actionPanel, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.CreateAttackChild()");
    }

    /// <summary>
    /// Create an instance of Defend attach to the weapon.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    private static Defend CreateDefendChild(ActionPanel actionPanel, GameObject characterObject)
    {
        switch (actionPanel.ActionData.WeaponType)
        {
            case WeaponType.None:
                throw new System.Exception("\"GetActionBase.CreateDefendChild() " +
                    "-> data.WeaponType = None");

            case WeaponType.Shield:
                return new ShieldBehaviour(actionPanel, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.CreateDefendChild()");
    }

}

