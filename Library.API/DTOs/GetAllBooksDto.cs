namespace Library.API.DTOs
{
    public class GetAllBooksDto
    {
        public string Id { get; set; }
        //public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        //public Rating Rating { get; set; }
        public bool IsAvailable { get; set; }
    }
}
