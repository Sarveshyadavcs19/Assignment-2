using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class BookDetails
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookEdition { get; set; }
        public string PublisherName { get; set; } 
        public int Price { get; set; }

        public BookDetails() { }

        public BookDetails(int bookId, string bookName,  string bookEdition, string publisherName, int Price)
        {
            BookId = bookId;
            BookName = bookName;
            PublisherName = publisherName;
            BookEdition = bookEdition;
            Price = Price;
        }
    }
   
}
