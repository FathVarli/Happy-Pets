using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.DataAccess.Context
{
    public class DbInitializer
    {
        public static byte[] CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        public static void Initialize(PostgresqlContext context)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();


            if (!context.OperationClaims.Any())
            {
                var rols = new OperationClaim[]
             {
                new OperationClaim
                {
                    Id=1,
                    Name = "User"
                },
                new OperationClaim
                {
                    Id=2,
                    Name = "Admin"
                },
             };

                foreach (OperationClaim r in rols)
                {
                    context.OperationClaims.Add(r);
                }
                context.SaveChanges();
            }


            if (!context.PetTypes.Any())
            {
                var types = new PetType[]
                {
                    new PetType
                    {
                        Id = 1,
                        Name = "Kedi"
                    },
                    new PetType
                    {
                        Id=2,
                        Name = "Köpek"
                    },
                    new PetType
                    {
                        Id=3,
                        Name="Ortak"
                    }
                };

                foreach (PetType t in types)
                {
                    context.PetTypes.Add(t);
                }
                context.SaveChanges();
            }

            if (!context.Vaccines.Any())
            {
                var vaccines = new Vaccine[]
                {
                    new Vaccine
                    {
                        Id = 1,
                        Name = "İç ve Dış Parazit",
                        isRepetitive = true,
                        MinWeek = 6,
                        MaxWeek = 8,
                        PetTypeId = 3,
                        RepetitiveMonthTime=3
                    },
                    new Vaccine
                                    {
                                        Id = 2,
                                        Name = "Kuduz",
                                        isRepetitive = true,
                                        MinWeek = 14,
                                        MaxWeek = 16,
                                        PetTypeId = 3,
                                        RepetitiveMonthTime=12
                                    },
new Vaccine
                    {
                        Id = 3,
                        Name = "Karma I",
                        isRepetitive = true,
                        MinWeek = 8,
                        MaxWeek = 10,
                        PetTypeId = 3,
                        RepetitiveMonthTime=12
                    },
 new Vaccine
                    {
                        Id = 4,
                        Name = "Karma II",
                        isRepetitive = false,
                        MinWeek = 10,
                        MaxWeek = 12,
                        PetTypeId = 3,
                        RepetitiveMonthTime=0
                    },
 new Vaccine
                    {
                        Id = 5,
                        Name = "Lösemi I",
                        isRepetitive = true,
                        MinWeek = 9,
                        MaxWeek = 11,
                        PetTypeId = 1,
                        RepetitiveMonthTime=12
                    },
   new Vaccine
                    {
                        Id = 6,
                        Name = "Lösemi II",
                        isRepetitive = true,
                        MinWeek = 11,
                        MaxWeek = 13,
                        PetTypeId = 1,
                        RepetitiveMonthTime=12
                    },
     new Vaccine
                    {
                        Id = 7,
                        Name = "Bronshine I",
                        isRepetitive = true,
                        MinWeek = 9,
                        MaxWeek = 11,
                        PetTypeId = 2,
                        RepetitiveMonthTime=12
                    },
          new Vaccine
                    {
                        Id = 8,
                        Name = "Bronshine II",
                        isRepetitive = false,
                        MinWeek = 11,
                        MaxWeek = 13,
                        PetTypeId = 2,
                        RepetitiveMonthTime=0
                    },
                    new Vaccine
                    {
                        Id = 9,
                        Name = "Corona I",
                        isRepetitive = true,
                        MinWeek = 10,
                        MaxWeek = 12,
                        PetTypeId = 2,
                        RepetitiveMonthTime=12
                    },
                            new Vaccine
                    {
                        Id = 10,
                        Name = "Corona II",
                        isRepetitive = false,
                        MinWeek = 12,
                        MaxWeek = 14,
                        PetTypeId = 2,
                        RepetitiveMonthTime=0
                    },
                                       new Vaccine
                    {
                        Id = 11,
                        Name = "Lyme",
                        isRepetitive = false,
                        MinWeek = 12,
                        MaxWeek = 14,
                        PetTypeId = 2,
                        RepetitiveMonthTime=12
                    },
                };
                foreach (Vaccine v in vaccines)
                {
                    context.Vaccines.Add(v);
                }
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User
                    {
                        Id = 1,
                        FirstName = "FATİH MEHMET",
                        LastName = "VARLI",
                        Email="fath.varl@gmail.com",
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456")),
                        PasswordSalt = hmac.Key,
                        CityId=1,
                        DistrictId = 1,
                        Phone = "+905434375794"

                    }
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }

            if (!context.UserOperationClaims.Any())
            {
                var userOperationClaims = new UserOperationClaim[]
          {
                new UserOperationClaim
                {

                    UserId = 1,
                    OperationClaimId = 2
                },

          };

                foreach (UserOperationClaim kr in userOperationClaims)
                {
                    context.UserOperationClaims.Add(kr);
                }
                context.SaveChanges();
            }

        }
    }
}
