using System.Data.SqlClient;
using Assignment_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    public class BookController : Controller
    {
        private string connectionString = "Data Source= LAPTOP-G5DH5D7B\\SQLEXPRESS; Database= BookData; Integrated Security= True; TrustServerCertificate=True;";

        public ActionResult Index()
        {
            List<BookDetails> books = GetBooks();
            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookDetails book)
        {
            if (ModelState.IsValid)
            {
                AddBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookDetails book = GetBookById(id);
            if (book == null)
            {
                return RedirectToAction("Edit");
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookDetails book)
        {
            if (ModelState.IsValid)
            {
                UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            BookDetails Book = GetBookById(id);
            if (Book == null)
            {
                return RedirectToAction("Index");
            }

            return View("Details", Book); 
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookDetails book = GetBookById(id);
            if (book == null)
            {
                return RedirectToAction("Delete");
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            DeleteBook(id);
            return RedirectToAction("Index");
        }

        private List<BookDetails> GetBooks()
        {
            List<BookDetails> books = new List<BookDetails>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM BookTable";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int bookId = reader.GetInt32(0);
                                string bookName = reader.GetString(1);
                                string bookEdition = reader.GetString(2);
                                string publisherName = reader.GetString(3);
                                int price = reader.GetInt32(4);

                                BookDetails book = new BookDetails
                                {
                                    BookId = bookId,
                                    BookName = bookName,
                                    BookEdition = bookEdition,
                                    PublisherName = publisherName,
                                    Price = price
                                };

                                books.Add(book);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error: {ex.Message}";
                }
            }

            return books;
        }

        private BookDetails GetBookById(int id)
        {
            BookDetails book = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM BookTable WHERE BookId = @BookId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int bookId = reader.GetInt32(0);
                                string bookName = reader.GetString(1);
                                string bookEdition = reader.GetString(2);
                                string publisherName = reader.GetString(3);
                                int price = reader.GetInt32(4);

                                book = new BookDetails
                                {
                                    BookId = bookId,
                                    BookName = bookName,
                                    BookEdition = bookEdition,
                                    PublisherName = publisherName,
                                    Price = price
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    ViewBag.Error = $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {

                    ViewBag.Error = $"Error: {ex.Message}";
                }
            }

            return book;
        }

        private void AddBook(BookDetails book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO BookTable (BookId, BookName, BookEdition, PublisherName, Price) " +
                                   "VALUES (@BookId, @BookName, @BookEdition, @PublisherName, @Price)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", book.BookId);
                        command.Parameters.AddWithValue("@BookName", book.BookName);
                        command.Parameters.AddWithValue("@BookEdition", book.BookEdition);
                        command.Parameters.AddWithValue("@PublisherName", book.PublisherName);
                        command.Parameters.AddWithValue("@Price", book.Price);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error: {ex.Message}";
                }
            }
        }

        private void UpdateBook(BookDetails book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE BookTable " +
                                   "SET BookName = @BookName, BookEdition = @BookEdition, " +
                                   "PublisherName = @PublisherName, Price = @Price " +
                                   "WHERE BookId = @BookId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", book.BookId);
                        command.Parameters.AddWithValue("@BookName", book.BookName);
                        command.Parameters.AddWithValue("@BookEdition", book.BookEdition);
                        command.Parameters.AddWithValue("@PublisherName", book.PublisherName);
                        command.Parameters.AddWithValue("@Price", book.Price);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error: {ex.Message}";
                }
            }
        }

        private void DeleteBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM BookTable WHERE BookId = @BookId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    ViewBag.Error = $"SQL Error: {ex.Message}";
                }
            }
        }
    }
}
