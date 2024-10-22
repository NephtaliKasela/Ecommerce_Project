using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce_Project.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string FullName { get; set; } // United States Dollar, Euro, British Pound Sterling, etc ...
        public string ShortName { get; set; } // USD, EUR, GBP, etc ...
        public string Symbol { get; set; }  // United States Dollar (USD) $, €, £, etc ...
    }
}
