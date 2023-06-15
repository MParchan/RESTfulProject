using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var userHMAC = new HMACSHA512();
            var users = new User[]
            {
                new User
                {
                    Email="user@user.pl",
                    PasswordHash=userHMAC.ComputeHash(Encoding.UTF8.GetBytes("User1234")),
                    PasswordSalt = userHMAC.Key
                }
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var incomeCategories = new IncomeCategory[]
            {
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Salary"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Pension"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Social benefits"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Own business"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Property rental"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Investments"
                },
                new IncomeCategory
                {
                    UserId=-1,
                    Name="Others"
                },
            };
            foreach (IncomeCategory ic in incomeCategories)
            {
                context.IncomeCategories.Add(ic);
            }
            context.SaveChanges();

            var expenditureCategory = new ExpenditureCategory[]
            {
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="House and bills"
                },
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="Food"
                },
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="Transport"
                },
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="Clothes"
                },
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="Health and beauty"
                },
                new ExpenditureCategory
                {
                    UserId=-1,
                    Name="Others"
                },
            };
            foreach (ExpenditureCategory ec in expenditureCategory)
            {
                context.ExpenditureCategories.Add(ec);
            }
            context.SaveChanges();

            var incomes = new Income[]
            {
                new Income
                {
                    IncomeCategoryId=1,
                    UserId=1,
                    Amount= 3500,
                    Comment="Salary lol",
                    Date = new DateTime(2023, 1, 1),
                },
                new Income
                {
                    IncomeCategoryId=1,
                    UserId=1,
                    Amount= 3900,
                    Comment="Salary",
                    Date = new DateTime(2022, 12, 1),
                },
                new Income
                {
                    IncomeCategoryId=2,
                    UserId=1,
                    Amount= 1200,
                    Comment="Pension",
                    Date = new DateTime(2022, 7, 13),
                },
                new Income
                {
                    IncomeCategoryId=6,
                    UserId=1,
                    Amount= 2000,
                    Comment="investments",
                    Date = new DateTime(2023, 1, 30),
                },
                new Income
                {
                    IncomeCategoryId=6,
                    UserId=1,
                    Amount= 2000,
                    Comment="investments",
                    Date = new DateTime(2022, 11, 10),
                },
                new Income
                {
                    IncomeCategoryId=1,
                    UserId=1,
                    Amount= 1000,
                    Comment="Salary",
                    Date = new DateTime(2022, 03, 1),
                },
            };
            foreach (Income i in incomes)
            {
                context.Incomes.Add(i);
            }
            context.SaveChanges();

            var expenditures = new Expenditure[]
            {
                new Expenditure
                {
                    ExpenditureCategoryId=1,
                    UserId=1,
                    Amount= 1599.99M,
                    Comment="bills",
                    Date = new DateTime(2023, 1, 13),
                },
                new Expenditure
                {
                    ExpenditureCategoryId=1,
                    UserId=2,
                    Amount= 5000,
                    Comment="Salary",
                    Date = new DateTime(2023, 12, 1),
                },
                new Expenditure
                {
                    ExpenditureCategoryId=2,
                    UserId=1,
                    Amount= 150,
                    Comment="Kebab",
                    Date = new DateTime(2022, 2, 1),
                },
                new Expenditure
                {
                    ExpenditureCategoryId=3,
                    UserId=1,
                    Amount= 320.5M,
                    Comment="taxi",
                    Date = new DateTime(2022, 1, 16),
                },
                new Expenditure
                {
                    ExpenditureCategoryId=3,
                    UserId=1,
                    Amount= 580.4M,
                    Comment="uber",
                    Date = new DateTime(2022, 10, 10),
                },
                new Expenditure
                {
                    ExpenditureCategoryId=1,
                    UserId=1,
                    Amount= 3001.55M,
                    Comment="Salary",
                    Date = new DateTime(2022, 09, 18),
                },
            };
            foreach (Expenditure e in expenditures)
            {
                context.Expenditures.Add(e);
            }
            context.SaveChanges();
        }
    }
}
