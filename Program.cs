using System.Diagnostics;
using System.Numerics;
using System.Security.Claims;
using static assignment_2.Program;
using static System.Reflection.Metadata.BlobBuilder;

namespace assignment_2
{
    internal class Program
    {   //1-Create a struct called Book 
        public struct Book
        {
            public string Title;
            public string Author;
            double Price;
            public Book(string Title, string Author, double Price)
            {
                this.Title = Title;
                this.Author = Author;
                this.Price = Price;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"Book details:\nbook title:{this.Title}\nbook author:{this.Author}\nbook price:{this.Price}$");
            }
        }
        //2-Create a class BankAccount
        //public class BankAccount {
        //    public string AccountHolder;
        //    public double balance;
        //     public BankAccount(string AccountHolder, double balance)
        //    {
        //        this.AccountHolder = AccountHolder;
        //        this.balance = balance;
        //    }
        //    public void Deposit(double amount)
        //    {
        //        if (amount > 0)
        //        {
        //            this.balance += amount;
        //        }
        //    }
        //   public void Withdraw(double amount)
        //    {
        //        if(balance >= amount)
        //        {
        //            this.balance -= amount;
        //            Console.WriteLine("Withdraw succesfully");
        //        }
        //        else
        //        {
        //            Console.WriteLine("balance is not enough");
        //        }
        //    }
        //    public void DisplayBalance()
        //    {
        //        Console.WriteLine($"account holder name is {this.AccountHolder} and balance={this.balance}$");
        //    }

        //}
        //3-Create Library Class
        public class Library
        {
            private string name;
            private string address;
            private double enterFees;
            public string[] BooksNamesArray = new string[1];
            public int numberOfBooks = 0;
            public string Name {
                get { return name; }
                set { name = value; }
            }
            public string Address
            {
                get { return address; }
                set { address = value; }
            }
            public double EnterFees
            {
                get { return this.enterFees; }
                set
                {
                    if (value > 0)
                    {
                        enterFees = value;
                    }
                    else
                    {
                        Console.WriteLine("Fees must be a positive number!");
                    }
                }
            }
            public Library(string name, string address, double fees) {
                this.Name = name;
                this.Address = address;
                this.EnterFees = fees;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"library details:\nname is {Name},address is {Address},fees={EnterFees}$");
            }
            public string this[int index]
            {
                get
                {
                    if (index >= 0 && index < numberOfBooks)
                        return BooksNamesArray[index];
                    throw new IndexOutOfRangeException("Invalid book index.");
                }
                set
                {
                    if (index < 0) throw new IndexOutOfRangeException("Invalid book index.");

                    if (index >= BooksNamesArray.Length)
                    {
                        Array.Resize(ref BooksNamesArray, index + 1);
                    }
                    BooksNamesArray[index] = value;

                    if (index >= numberOfBooks)
                        numberOfBooks = index + 1;
                }
            }


            public void AddBook(string bookName)
            {
                if (numberOfBooks >= BooksNamesArray.Length)
                {
                    Array.Resize(ref BooksNamesArray, BooksNamesArray.Length + 1);
                }
                BooksNamesArray[numberOfBooks] = bookName;
                numberOfBooks++;
            }

            public void DisplayAllBooks()
            {
                Console.WriteLine("Books in libray:");
                foreach (string bookName in BooksNamesArray) { Console.WriteLine(bookName); }
            }
        }
        //Q4
        public class BankAccount
        {

            protected double balance;
            public BankAccount(double balance)
            {
                this.balance = balance;
            }
            public void Deposit(double amount)
            {
                if (amount > 0)
                {
                    this.balance += amount;
                }
            }
            public void Deposit(double amount, double bonus)
            {
                if (amount > 0)
                {
                    this.balance = this.balance + amount + bonus;
                }
            }
            public void DisplayBalance()
            {
                Console.WriteLine($"balance={this.balance}$");
            }
            public static BankAccount operator +(BankAccount a1, BankAccount a2)
            {
                double newBalance = a1.balance + a2.balance;

                return new BankAccount(newBalance);
            }
            public static bool operator >(BankAccount a1, BankAccount a2)
            {
                return a1.balance > a2.balance;
            }

            public static bool operator <(BankAccount a1, BankAccount a2)
            {
                return a1.balance < a2.balance;
            }

            public static explicit operator double(BankAccount account)
            {
                return account.balance;
            }
        }
        public class SavingsAccount : BankAccount
        {
            public SavingsAccount(double balance) : base(balance)
            {
            }
            public void AddInterest(double rate)
            {
                if (rate > 0)
                {
                    double interset = this.balance * (rate / 100);
                    this.balance += interset;
                }
            }
        }
        public class CheckingAccount : BankAccount
        {
            public CheckingAccount(double balance) : base(balance) { }
            public void Withdraw(double amount)
            {
                if (balance >= amount)
                {
                    this.balance -= amount;
                    Console.WriteLine("Withdraw succesfully");
                }
                else
                {
                    Console.WriteLine("balance is not enough");
                }
            }
        }
        //Q5
        public class BaseClass
        {
            public virtual void DisplayMessage()
            {
                Console.WriteLine("Message from BaseClass");
            }
            public void DisplayMessage2()
            {
                Console.WriteLine("Message from BaseClass");
            }
        }
        public class DerivedClass1 : BaseClass
        {
            public override void DisplayMessage()
            {
                Console.WriteLine("Message from DerivedClass1");
            }
        }
        public class DerivedClass2 : BaseClass
        {
            public new void DisplayMessage2()
            {
                Console.WriteLine("Message from DerivedClass2");
            }
        }
        //override:
        //The parent method is rewritten.
        //new
        //The parent method is removed and a new method is created.
        //Q6
        public class Duration
        {
            public int Hours;
            public int Minutes;
            public int Seconds;

            public Duration(int hours, int minutes, int seconds)
            {
                Hours = hours;
                Minutes = minutes;
                Seconds = seconds;
            }

            public override string ToString()
            {
                return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
            }

            public override bool Equals(object obj)
            {
                if (obj is Duration other)
                {
                    return this.Hours == other.Hours &&
                           this.Minutes == other.Minutes &&
                           this.Seconds == other.Seconds;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Hours, Minutes, Seconds);
            }
        }

        //Q7
        public static class Maths
        {
            public static int Add(int num1, int num2)
            {
                return num1 + num2;
            }
            public static int Subtract(int num1, int num2)
            {
                return num1 - num2;
            }

            public static int Multiply(int num1, int num2)
            {
                return num1 * num2;
            }
            public static int Divide(int num1, int num2)
            {
                if (num2 != 0)
                {
                    return num1 / num2;
                }
                else if (num1 == 0)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
        //Q8
        public abstract class Discount
        {
            public string Name { get; set; }
            public abstract decimal CalculateDiscount(decimal price, int quantity);
        }

        public class PercentageDiscount : Discount
        {
            public decimal Percentage { get; set; }

            public PercentageDiscount(decimal percentage)
            {
                Name = "Percentage Discount";
                Percentage = percentage;
            }

            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                decimal discountAmount = price * quantity * (Percentage / 100);
                return discountAmount;
            }
        }


        public class FlatDiscount : Discount
        {
            public decimal FlatAmount { get; set; }

            public FlatDiscount(decimal flatAmount)
            {
                Name = "Flat Discount";
                FlatAmount = flatAmount;
            }

            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                decimal discountAmount = FlatAmount * Math.Min(quantity, 1);
                return discountAmount;
            }
        }

        public class BuyOneGetOneDiscount : Discount
        {
            public BuyOneGetOneDiscount()
            {
                Name = "Buy One Get One Discount";
            }

            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                if (quantity <= 1)
                    return 0;

                decimal pairs = Math.Floor(quantity / 2m); 
                decimal discountAmount = (price / 2) * pairs;
                return discountAmount;
            }
        }


        static void Main(string[] args)
        {
            #region Q1: 
            //Book[] books = new Book[2];
            //string Title;
            //string Author;
            //double Price;
            //for (int i = 0; i < books.Length; i++)
            //{
            //    Console.Write($"Enter title for book #{i + 1}:");
            //    Title = Console.ReadLine();
            //    Console.Write($"Enter author for book #{i + 1}:");
            //    Author = Console.ReadLine();
            //    while (true)
            //    {
            //        Console.Write($"Enter price for book #{i + 1}:");
            //        if (double.TryParse(Console.ReadLine(), out Price))
            //        {
            //            books[i] = new Book(Title, Author, Price); break;
            //        }
            //        else
            //        {
            //            Console.WriteLine("invalid input");
            //        }
            //    }
            //}
            //foreach (Book book in books)
            //{
            //    book.DisplayInfo();
            //}

            #endregion
            #region Q2: 
            //Console.Write("Enter account holder name:");
            //string name=Console.ReadLine();
            //Console.Write("Enter balance:");
            //double balance,amount;
            //BankAccount bankAccount=null;
            //if (double.TryParse(Console.ReadLine(), out balance)) { 
            //if(balance >= 0)
            //    {
            //         bankAccount = new BankAccount(name,balance);
            //    }
            //    else
            //    {
            //        Console.WriteLine("invaild input");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("invaild input");
            //}
            //Console.Write("To deposit, enter the amount:");
            //if (bankAccount !=null)
            //{
            //    if (double.TryParse(Console.ReadLine(), out amount))
            //    {
            //        if (amount >= 0)
            //        {
            //            bankAccount.Deposit(amount);
            //            Console.WriteLine($"new balance after deposit={bankAccount.balance}$");
            //        }
            //        else
            //        {
            //            Console.WriteLine("invalid amount");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("invalid amount");
            //    }
            //}
            //Console.Write("\nTo withdraw, enter the amount:");
            //if (bankAccount != null)
            //{
            //    if (double.TryParse(Console.ReadLine(), out amount))
            //    {
            //        if (amount >= 0)
            //        {
            //            bankAccount.Withdraw(amount);
            //            Console.WriteLine($"new balance after withdraw={bankAccount.balance}$");
            //        }
            //        else
            //        {
            //            Console.WriteLine("invalid amount");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("invalid amount");
            //    }
            //}

            #endregion
            #region Q3:
            //Library lib1 = new Library("Central City Library", "123 Main Street, Cairo", 50.0);
            //Library lib2 = new Library("Green Valley Library", "45 Garden Road, Alexandria", 30.5);
            //lib1.DisplayInfo();
            //lib2.DisplayInfo();
            //lib1[0] = "Clean Code";
            //lib1.AddBook("The Pragmatic Programmer");
            //lib2.AddBook("Head First Programming");
            //lib2.AddBook("Introduction to the Algorithms (CLRS)");
            //Console.WriteLine("book1 in lib1 is "+lib1[0]);
            //Console.WriteLine("book2 in lib1 is " + lib1[1]);
            //Console.WriteLine("book1 in lib2 is " + lib2[0]);
            //Console.WriteLine("book2 in lib2 is " + lib2[1]);
            //lib1.DisplayAllBooks();
            //lib2.DisplayAllBooks();
            #endregion
            #region Q4:
            //SavingsAccount acc=new SavingsAccount(1000);
            //CheckingAccount acc2 = new CheckingAccount(2000);
            //acc.Deposit(300);
            //acc.Deposit(300, 100);
            //if (acc> acc2)
            //{
            //    Console.WriteLine("Account 1 has a higher balance.");
            //}
            //else
            //{
            //    Console.WriteLine("Account 2 has a higher balance.");
            //}
            //BankAccount newAcc= acc+acc2;
            //double balance = (double)acc;
            #endregion
            #region Q5:
            //BaseClass obj = new DerivedClass1();
            //obj.DisplayMessage(); // override

            //DerivedClass2 obj2 = new DerivedClass2();
            //obj2.DisplayMessage2(); // new

            #endregion
            #region Q6: 
            //var d1 = new Duration(1, 30, 45);
            //var d2 = new Duration(1, 30, 45);
            //Console.WriteLine(d1); 
            //Console.WriteLine(d1.Equals(d2)); 
            //Console.WriteLine(d1.GetHashCode()); 

            #endregion
            #region Q7:
            //Console.WriteLine("Add="+Maths.Add(5, 6));
            //Console.WriteLine("Multiply="+Maths.Multiply(3,4));
            //Console.WriteLine("Subtract=" + Maths.Subtract(10,15));
            //Console.WriteLine("Divide=" + Maths.Divide(15,3));
            //Console.WriteLine("Divide=" + Maths.Divide(15, 0));

            #endregion
            #region Q8:

            #endregion
        }
    }
}

