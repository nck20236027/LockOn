using System;
using UnityEngine;

/// <summary>
/// Unity のオブジェクトを Serializable として扱いながらインターフェース参照を安全に取得するためのラッパー。
/// - T1: 参照したいインターフェース型
/// - T2: UnityEngine.Object を継承し T1 を実装する型
/// Inspector にオブジェクトを割り当て、Interface プロパティを通じてキャスト済みのインターフェースを取得する。
/// </summary>
[Serializable]
public class GenericInterfaceWrapper<T1, T2> where T2 : UnityEngine.Object, T1
{
    private T1 _targetInterface;

    [SerializeField]
    private T2 _attachedObject;

    /// <summary>
    /// Inspector に割り当てたオブジェクトを指定インターフェースとして返す。
    /// キャストに失敗した場合はエディタログにエラーを出力する（Editor 時）。
    /// </summary>
    public T1 Interface
    {
        get
        {
            if (!_attachedObject)
            {
#if UNITY_EDITOR
                Debug.LogError("No AttachedObject");
#endif
            }

            try
            {
                _targetInterface ??= (T1)_attachedObject;
            }
            catch (InvalidCastException)
            {
#if UNITY_EDITOR
                Debug.LogError($"There is no correct script in {_attachedObject}");
#endif
            }

            return _targetInterface;
        }
    }
}
