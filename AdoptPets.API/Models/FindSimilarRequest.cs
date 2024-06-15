namespace AdoptPets.API.Models
{
    public class FindSimilarRequest
    {
        public string ImageUrl { get; set; }
        public string Label { get; set; }
        public int K { get; set; } = 3;
    }
}
