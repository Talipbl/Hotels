using Entity.Concrete;

namespace WebUI.Models
{
    public class ListHotelsViewModel
    {
        public List<Hotel> Hotels { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
    }
}
