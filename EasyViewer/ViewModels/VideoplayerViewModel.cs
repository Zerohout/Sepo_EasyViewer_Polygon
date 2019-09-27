﻿namespace EasyViewer.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Windows.Input;
    using MainMenu.ViewModels;
    using Models.FilmModels;
    using Views;
    using static Helpers.SystemVariables;
    using Screen = Caliburn.Micro.Screen;

    public partial class VideoPlayerViewModel : Screen
    {
        public VideoPlayerViewModel()
        {

        }

        public VideoPlayerViewModel(MainMenuViewModel mmvm, VideoPlayerMode currentMode, int? epCount,  
            List<Episode> checkedEpisodes = null, Episode currentEpisode = null)
        {
            MMVM = mmvm;
            MainTimer.Elapsed += MainTimer_Elapsed;
            MainTimer.Interval = 500;

            if (currentMode == VideoPlayerMode.Viewing)
            {
                WatchingEpisodesCount = epCount;
                CheckedEpisodes = checkedEpisodes;
                StartViewing();
            }
            else
            {
                CurrentEpisode = currentEpisode;
                StartPreview();
            }
        }

        protected override void OnInitialize()
        {
            HotReg.RegisterGlobalHotkey(() =>
            {
                IsEpisodeSkipped = true;
                PlayNextEpisode();
            }, Keys.Right, ModifierKeys.Control);
            base.OnInitialize();
        }

        protected override void OnViewLoaded(object view)
        {
            if (view is VideoPlayerView playerView)
            {
                playerView.Left = AppVal.WS.VPStartupPos.X;
                playerView.Top = AppVal.WS.VPStartupPos.Y;
            }

            base.OnViewLoaded(view);
        }
    }
}
