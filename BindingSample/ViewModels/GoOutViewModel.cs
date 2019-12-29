using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class GoOutViewModel : BindableBase
    {
        public string Title { get; set; }
        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();

        protected ObservableCollection<GoOutDetaiViewModel> _details = new ObservableCollection<GoOutDetaiViewModel>();
        public ObservableCollection<GoOutDetaiViewModel> Details
        {
            get
            {
                return _details;
            }
            set
            {
                if (_details != null)
                {
                    foreach (GoOutDetaiViewModel detail in _details)
                    {
                        detail.PropertyChanged -= Detail_PropertyChanged;
                    }
                }

                _details = value;

                if(_details != null)
                {
                    foreach(GoOutDetaiViewModel detail in _details)
                    {
                        detail.PropertyChanged += Detail_PropertyChanged;
                    }
                }
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( _details.All(x => x.GoOut.Status != GoOut.EStatus.Plan))
            {
                BackgroundColor.Value = Color.Orange;
            } else
            {
                BackgroundColor.Value = Color.Transparent;
            }
        }

        public GoOutViewModel()
        {
        }

    }
}
