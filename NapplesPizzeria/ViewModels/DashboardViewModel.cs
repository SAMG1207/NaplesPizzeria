using NapplesPizzeria.Models;

namespace NapplesPizzeria.ViewModels
{
    public class DashboardViewModel
    {

        public Dictionary<int, bool> TableState { get; set; }
        public List <MtabProduct> Products { get; set; }

        public List<MtabOrder> Orders { get; set; }

        public List <MtabService> Services { get; set; }
        public List<MtabCategory> Category { get; set; }
    }
}
