namespace INSS.FIP.Web.ViewModels
{
    public class InfoPagesViewModel
    {
        public InfoPagesViewModel()
        {
            Breadcrumbs = new List<BreadcrumbItemViewModel>();
        }

        public List<BreadcrumbItemViewModel> Breadcrumbs { get; set; }
    }
}