using BindingSample.ViewModels;
using Xamarin.Forms;

namespace BindingSample
{
    public class GoOutTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MultipleTemplate { get; set; }
        public DataTemplate SingleTemplate { get; set; }

        public GoOutTemplateSelector()
        {
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            GoOutViewModel goOutVm = (GoOutViewModel)item;
            if( goOutVm.Details.Count == 1)
            {
                return SingleTemplate;
            }
            return MultipleTemplate;
        }
    }
}
