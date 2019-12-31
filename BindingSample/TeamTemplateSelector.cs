using BindingSample.ViewModels;
using Xamarin.Forms;

namespace BindingSample
{
    public class TeanTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MultipleTemplate { get; set; }
        public DataTemplate SingleTemplate { get; set; }

        public TeanTemplateSelector()
        {
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            TeamViewModel teamVm = (TeamViewModel)item;
            if( teamVm.EmployeeSafetyVms.Count == 1)
            {
                return SingleTemplate;
            }
            return MultipleTemplate;
        }
    }
}
