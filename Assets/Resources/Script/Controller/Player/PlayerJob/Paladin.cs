using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� (����) Ŭ����
/// </summary>
public class Paladin : PlayerController
{
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
