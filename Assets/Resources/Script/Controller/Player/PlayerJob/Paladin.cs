using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� (����) Ŭ����
/// </summary>
public class Paladin : PlayerController
{
    protected override void DieState()
    {
        base.DieState();
        // ��� Effect ���
        SoundManager.Instance.PlayAudio("MaleDeath", SoundPlayType.Single);
    }

    #region �ִϸ��̼ǿ� ���� ���� Event
    /// <summary>
    /// �÷��̾�(Paladin) ���ݽ� ����Ǵ� ����
    /// </summary>
    private void SoundAttack()
    {
        SoundManager.Instance.PlayAudio("PaladinAttack",SoundPlayType.Multi);
    }
    #endregion
}
