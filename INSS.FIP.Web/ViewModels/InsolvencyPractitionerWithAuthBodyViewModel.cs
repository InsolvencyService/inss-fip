namespace INSS.FIP.Web.ViewModels
{
    public class InsolvencyPractitionerWithAuthBodyViewModel
    {
        public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

        public int PageNumber { get; set; }

        public InsolvencyPractitionerViewModel InsolvencyPractitioner { get; set; }

        public AuthBodyViewModel AuthBody { get; set; }

        public InsolvencyPractitionerWithAuthBodyViewModel()
        {
            Breadcrumbs = new List<BreadcrumbItemViewModel>();
            InsolvencyPractitioner = new InsolvencyPractitionerViewModel();
            AuthBody = new AuthBodyViewModel();
        }
    }
}