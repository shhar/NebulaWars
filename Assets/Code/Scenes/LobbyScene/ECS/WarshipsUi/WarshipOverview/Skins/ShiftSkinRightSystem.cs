using Code.Common;
using Entitas;
using System.Collections.Generic;
using Plugins.submodules.SharedCode.Logger;

namespace Code.Scenes.LobbyScene.ECS.WarshipsUi.WarshipOverview.Skins
{
    /// <summary>
    /// Отвечает за листание скинов
    /// </summary>
    public class ShiftSkinRightSystem : ReactiveSystem<LobbyUiEntity>
    {
        private readonly LobbyUiContext lobbyUiContext;
        private readonly UiSoundsManager lobbySoundsManager;
        private readonly ILog log = LogManager.CreateLogger(typeof(ShiftSkinRightSystem));

        public ShiftSkinRightSystem(Contexts contexts, UiSoundsManager lobbySoundsManager) 
            : base(contexts.lobbyUi)
        {
            lobbyUiContext = contexts.lobbyUi;
            this.lobbySoundsManager = lobbySoundsManager;
        }

        protected override ICollector<LobbyUiEntity> GetTrigger(IContext<LobbyUiEntity> context)
        {
            return context.CreateCollector(LobbyUiMatcher.ShiftSkinRight);
        }

        protected override bool Filter(LobbyUiEntity entity)
        {
            return entity.messageShiftSkinRight;
        }

        protected override void Execute(List<LobbyUiEntity> entities)
        {
            var model = lobbyUiContext.warshipOverviewCurrentSkinModel;
            int currentSkinIndex = model.skinIndex;
            int skinsCount =  model.warshipDto.Skins.Count;
            if (currentSkinIndex == 0)
            {
                log.Warn("Нельзя сдвинуть скин вправо. Сейчас показывается крайний левый.");
            }
            else
            {
                lobbyUiContext.ReplaceWarshipOverviewCurrentSkinModel(--model.skinIndex, model.warshipDto);
                lobbySoundsManager.PlayWarshipChangingRight();
            }
        }
    }
}