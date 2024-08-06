using System;
using System.Collections.Generic;

// Book class
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfCopies { get; set; }
}

// Member class
public class Member
{
    public int MemberID { get; set; }
    public string Name { get; set; }
    public List<Book> BorrowedBooks { get; set; }

    public Member(int id, string name)
    {
        MemberID = id;
        Name = name;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        BorrowedBooks.Add(book);
        Console.WriteLine($"{Name} borrowed {book.Title}");
    }

    public void ReturnBook(Book book)
    {
        BorrowedBooks.Remove(book);
        Console.WriteLine($"{Name} returned {book.Title}");
    }
}

// Librarian class
public class Librarian
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    private List<Book> _books;
    private List<Member> _members;

    public Librarian(int id, string name)
    {
        EmployeeID = id;
        Name = name;
        _books = new List<Book>();
        _members = new List<Member>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
        Console.WriteLine($"Added {book.Title} by {book.Author}");
    }

    public void RemoveBook(Book book)
    {
        _books.Remove(book);
        Console.WriteLine($"Removed {book.Title} by {book.Author}");
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in the library:");
        foreach (var book in _books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Copies: {book.NumberOfCopies}");
        }
    }

    public void RegisterMember(Member member)
    {
        _members.Add(member);
        Console.WriteLine($"Registered new member: {member.Name} (ID: {member.MemberID})");
    }

    public void DisplayMembers()
    {
        Console.WriteLine("Members in the library:");
        foreach (var member in _members)
        {
            Console.WriteLine($"Name: {member.Name}, ID: {member.MemberID}");
        }
    }

    public void ProcessBorrow(Member member, Book book)
    {
        if (_books.Contains(book))
        {
            member.BorrowBook(book);
        }
        else
        {
            Console.WriteLine($"Sorry, {book.Title} is not available.");
        }
    }

    public void ProcessReturn(Member member, Book book)
    {
        if (member.BorrowedBooks.Contains(book))
        {
            member.ReturnBook(book);
        }
        else
        {
            Console.WriteLine($"{member.Name} did not borrow {book.Title}.");
        }
    }
}

// Example usage
public class Program
{
    static void Main(string[] args)
    {
        // Create a librarian
        var librarian = new Librarian(1, "Jane Doe");

        // Create some books
        var book1 = new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", NumberOfCopies = 5 };
        var book2 = new Book { Title = "1984", Author = "George Orwell", NumberOfCopies = 3 };
        var book3 = new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", NumberOfCopies = 2 };

        // Add books to the library
        librarian.AddBook(book1);
        librarian.AddBook(book2);
        librarian.AddBook(book3);

        // Create some members
        var member1 = new Member(1, "John Doe");
        var member2 = new Member(2, "Jane Smith");

        // Register members
        librarian.RegisterMember(member1);
        librarian.RegisterMember(member2);

        // Process book borrowing and returning
        librarian.ProcessBorrow(member1, book1);
        librarian.ProcessBorrow(member2, book2);
        librarian.ProcessReturn(member1, book1);
        librarian.ProcessBorrow(member1, book3);

        // Display books and members
        librarian.DisplayBooks();
        librarian.DisplayMembers();
    }
}
