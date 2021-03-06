﻿// using System.Collections.Generic;
// using Entitas;
// using UnityEngine;
//
// namespace Code.Scenes.BattleScene.ECS.Systems
// {
//     public class DeathAnimationSystem : ReactiveSystem<ServerGameEntity>
//     {
//         public DeathAnimationSystem(Contexts contexts) : base(contexts.game)
//         {
//         }
//
//         protected override ICollector<ServerGameEntity> GetTrigger(IContext<ServerGameEntity> context)
//         {
//             var matcher = GameMatcher.AllOf(ServerGameMatcher.Destroyed, GameMatcher.View);
//             return context.CreateCollector(matcher);
//         }
//
//         protected override bool Filter(GameEntity entity)
//         {
//             return entity.isDestroyed && entity.hasView;
//         }
//
//         protected override void Execute(List<ServerGameEntity> entities)
//         {
//             foreach (var e in entities)
//             {
//                 if (e.hasAnimatorController)
//                 {
//                     //TODO
//                 }
//                 else
//                 {
//                     e.view.gameObject.GetComponent<Renderer>().enabled = false;
//                 }
//             }
//         }
//     }
// }