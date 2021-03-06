﻿using Plugins.submodules.SharedCode.LagCompensation;

namespace Code.Scenes.BattleScene.Experimental.Prediction
{
    public interface ISnapshotBuffer
    {
        int GetLength();
        SnapshotWithLastInputId GetNewestSnapshot();
        int GetNewestTickNumber();
        void Add(SnapshotWithLastInputId snapshotArg);
        int? GetTickNumberByTime(float matchTime);
        float GetPenultimateSnapshotTickTime();
    }
}