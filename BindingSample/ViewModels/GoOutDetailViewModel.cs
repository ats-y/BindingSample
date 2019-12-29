using System;
using System.ComponentModel;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class GoOutDetaiViewModel : INotifyPropertyChanged
    {
        public GoOut GoOut { get; private set; }

        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();

        public ReactiveCommand DetailTapCommand { get; set; } = new ReactiveCommand();

        public DateTime EventTime { get => GoOut.EventTime; }
        public string Content { get => GoOut.Content; }
        public string Comment { get => GoOut.Comment; }

        public GoOutDetaiViewModel(GoOut goOut)
        {
            GoOut = goOut;
            SetBackgroundColor();
            DetailTapCommand.Subscribe(x => OnDetailTapCommand());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnDetailTapCommand()
        {
            // 状態遷移。
            switch (GoOut.Status)
            {
                case GoOut.EStatus.Plan:
                    GoOut.Status = GoOut.EStatus.Checked;
                    break;
                case GoOut.EStatus.Checked:
                    GoOut.Status = GoOut.EStatus.OnHold;
                    break;
                default:
                    GoOut.Status = GoOut.EStatus.Plan;
                    break;
            }

            // 状態に合わせて背景色変更。
            SetBackgroundColor();
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
        }

        private void SetBackgroundColor()
        {
            switch (GoOut.Status)
            {
                case GoOut.EStatus.Checked:
                    BackgroundColor.Value = Color.Orange;
                    break;
                case GoOut.EStatus.OnHold:
                    BackgroundColor.Value = Color.DarkGray;
                    break;
                default:
                    BackgroundColor.Value = Color.Transparent; // 透明
                    break;
            }
        }
    }
}
