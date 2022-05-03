using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 (전사) 클래스
/// </summary>
public class Paladin : PlayerController
{
    protected override void DieState()
    {
        base.DieState();
        // 사망 Effect 출력
        SoundManager.Instance.PlayAudio("MaleDeath", SoundPlayType.Single);
    }

    #region 애니메이션에 들어가는 사운드 Event
    /// <summary>
    /// 플레이어(Paladin) 공격시 재생되는 사운드
    /// </summary>
    private void SoundAttack()
    {
        SoundManager.Instance.PlayAudio("PaladinAttack",SoundPlayType.Multi);
    }
    #endregion
}
