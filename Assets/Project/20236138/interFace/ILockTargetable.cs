using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�[�Q�b�g�Ɏw�肷�邽�߂̃C���^�[�t�F�[�X
public interface ILockTargetable
{
    public Transform GetTransform { get; }
    public Vector3 GetTokenPosition { get; }
    public bool GetIsView { get; }

}
