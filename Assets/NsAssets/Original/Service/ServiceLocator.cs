using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator<T> where T : class,IServiceClass
{
    private static Dictionary<Type, T> instanceDic = new();

    /// <summary>
    /// インスタンスの登録
    /// </summary>
    /// <param name="instance">登録するインスタンス</param>
    /// <param name="isOverWrite">上書きするかどうか</param>
    public static void Register(T instance, bool isOverWrite = false)
    {
        if (!instanceDic.TryAdd(typeof(T), instance) && isOverWrite)
        {
            instanceDic[typeof(T)] = instance;
        }
    }

    /// <summary>
    /// 登録確認用メソッド
    /// </summary>
    /// <returns></returns>
    public static bool IsRegistered()
    {
        return instanceDic.ContainsKey(typeof(T));
    }

    /// <summary>
    /// 対応するインスタンスを返す
    /// </summary>
    /// <returns></returns>
    public static T GetInstance()
    {
        try
        {
            return instanceDic[typeof(T)];
        }
        catch
        {
            Debug.LogError($"ServiceLocator: {typeof(T)} のインスタンスが登録されていません");
            throw;
        }
    }

    public static void RemoveInstance(T removeInstance)
    {
        T instance = instanceDic[typeof(T)];

        //削除指令が登録されたインスタンスと同一なら
        if (removeInstance == instance)
        {
            Debug.Log($"Remove:{removeInstance}");

            //削除
            instanceDic.Remove(typeof(T));
        }
    }
}