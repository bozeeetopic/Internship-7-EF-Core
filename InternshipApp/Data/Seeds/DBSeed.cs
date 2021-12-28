using Microsoft.EntityFrameworkCore;
using Data.Entities.Enums;
using Data.Entities.Models;
using System;
using System.Collections.Generic;

namespace Data.Seeds
{
    public class DBSeed
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Member>()
                .HasData(new List<Member>
                {
                    new Member
                    {
                        Id = 1,
                        Name = "Ivo",
                        Surname = "Sanader",
                        Username = "HDZ",
                        Password = "",
                        ReputationPoints = 20000,
                        isOrganiser = true
                    },
                    new Member
                    {
                        Id = 2,
                        Name = "Marjan",
                        Surname = "Marjanovic",
                        Username = "asdasd",
                        Password = "",
                        ReputationPoints = 10000,
                        isOrganiser = false
                    },
                    new Member
                    {
                        Id = 3,
                        Name = "Ivo",
                        Surname = "Ivic",
                        Username = "asfffassf",
                        Password = "",
                        ReputationPoints = 0,
                        isOrganiser = false
                    },
                    new Member
                    {
                        Id = 4,
                        Name = "Pero",
                        Surname = "Peric",
                        Username = "sd",
                        Password = "",
                        ReputationPoints = 0,
                        isOrganiser = false
                    }
                });

            builder.Entity<Post>()
                .HasData(new List<Post>
                {
                    new Post
                    {
                        Id = 1,
                        AuthorId = 1,
                        Header = "Hehe",
                        Date = new DateTime(2020, 2, 12),
                        Text = "Hehehehehehehehehehe",
                        Domain = Domain.General
                    },
                    new Post
                    {
                        Id = 2,
                        AuthorId = 1,
                        Header = "Hoho",
                        Date = new DateTime(2020, 3, 12),
                        Text = "hohohohhohohohohohohoho",
                        Domain = Domain.General
                    },
                    new Post
                    {
                        Id = 3,
                        AuthorId = 2,
                        Header = "Nešto smisleno",
                        Date = new DateTime(2020, 4, 12),
                        Text = "Lorem ipsum or sumtin",
                        Domain = Domain.Marketing
                    }
                }) ;

            builder.Entity<Comment>()
                .HasData(new List<Comment>
                {
                    new Comment
                    {
                        Id = 1,
                        AuthorId = 1,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        PostId = 1,
                        CommentId = null
                    },
                    new Comment
                    {
                        Id = 2,
                        AuthorId = 2,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        PostId = 1,
                        CommentId = 1
                    },
                    new Comment
                    {
                        Id = 3,
                        AuthorId = 3,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        PostId = 1,
                        CommentId = 2
                    },
                    new Comment
                    {
                        Id = 4,
                        AuthorId = 1,
                        Date = new DateTime(2020, 5, 12),
                        Text = "hohohohohoho",
                        PostId = 2,
                        CommentId = null
                    }
                });
        }
    }
}
