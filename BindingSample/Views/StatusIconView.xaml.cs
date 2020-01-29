using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using static BindingSample.Models.Safety;

namespace BindingSample.Views
{
    public partial class StatusIconView : ContentView
    {
        /// <summary>
        /// 本人状態種別
        /// </summary>
        public EStatus Status { get; set; }

        public StatusIconView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            switch (Status)
            {
                case EStatus.Safe:
                    StatusText.Text = "無事";
                    Frame.BackgroundColor = Color.Aquamarine;
                    break;
                case EStatus.Injury:
                    StatusText.Text = "怪我";
                    Frame.BackgroundColor = Color.IndianRed;
                    break;
                case EStatus.Sick:
                    StatusText.Text = "病気";
                    Frame.BackgroundColor = Color.HotPink;
                    break;
                default:
                    StatusText.Text = "不明";
                    Frame.BackgroundColor = Color.LightSlateGray;
                    break;
            }
        }
    }
}
