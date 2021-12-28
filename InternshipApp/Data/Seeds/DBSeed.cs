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
                        IsOrganiser = true
                    },
                    new Member
                    {
                        Id = 2,
                        Name = "Marjan",
                        Surname = "Marjanovic",
                        Username = "asdasd",
                        Password = "",
                        ReputationPoints = 10000,
                        IsOrganiser = false
                    },
                    new Member
                    {
                        Id = 3,
                        Name = "Ivo",
                        Surname = "Ivic",
                        Username = "asfffassf",
                        Password = "",
                        ReputationPoints = 0,
                        IsOrganiser = false
                    },
                    new Member
                    {
                        Id = 4,
                        Name = "Pero",
                        Surname = "Peric",
                        Username = "sd",
                        Password = "",
                        ReputationPoints = 0,
                        IsOrganiser = false
                    }
                });

            builder.Entity<Resource>()
                .HasData(new List<Resource>
                {
                    new Resource
                    {
                        Id = 1,
                        AuthorId = 1,
                        Header = "Hehe",
                        Date = new DateTime(2020, 2, 12),
                        Text = "Hehehehehehehehehehe",
                        Domain = Domain.General
                    },
                    new Resource
                    {
                        Id = 2,
                        AuthorId = 1,
                        Header = "Hoho",
                        Date = new DateTime(2020, 3, 12),
                        Text = "hohohohhohohohohohohoho",
                        Domain = Domain.General
                    },
                    new Resource
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
                        Id = 4,
                        AuthorId = 1,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        ResourceId = 1,
                        CommentId = null
                    },
                    new Comment
                    {
                        Id = 5,
                        AuthorId = 2,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        ResourceId = 1,
                        CommentId = 4
                    },
                    new Comment
                    {
                        Id = 6,
                        AuthorId = 3,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        ResourceId = 1,
                        CommentId = 5
                    },
                    new Comment
                    {
                        Id = 7,
                        AuthorId = 1,
                        Date = new DateTime(2020, 5, 12),
                        Text = "hohohohohoho",
                        ResourceId = 2,
                        CommentId = null
                    }
                });

            builder.Entity<Reaction>()
                .HasData(new List<Reaction>
                {
                    new Reaction
                    {
                        Id = 1,
                        ReactorId = 1,
                        PostId = 1,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                       Id = 2,
                        ReactorId = 1,
                        PostId = 2,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                       Id = 3,
                        ReactorId = 1,
                        PostId = 3,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                        Id = 4,
                        ReactorId = 1,
                        PostId = 4,
                        IsUpVote = true
                    }
                });
        }
    }
}
