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
                        ReputationPoints = 1000000,
                        BannedUntil = DateTime.Now
                    },
                    new Member
                    {
                        Id = 2,
                        Name = "Marjan",
                        Surname = "Marjanovic",
                        Username = "asdasd",
                        Password = "",
                        ReputationPoints = 10000,
                        BannedUntil = DateTime.Now
                    },
                    new Member
                    {
                        Id = 3,
                        Name = "Ivo",
                        Surname = "Ivic",
                        Username = "asfffassf",
                        Password = "",
                        ReputationPoints = 1,
                        BannedUntil = DateTime.Now
                    },
                    new Member
                    {
                        Id = 4,
                        Name = "Pero",
                        Surname = "Peric",
                        Username = "sd",
                        Password = "",
                        ReputationPoints = 1,
                        BannedUntil = DateTime.Now
                    },
                    new Member
                    {
                        Id = 999,
                        Name = "Isus",
                        Surname = "Krist",
                        Username = "sus",
                        Password = "",
                        ReputationPoints = 100000,
                        BannedUntil = DateTime.Now
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
                        Domain = ResourceDomain.General,
                        SeenCounter = 4
                    },
                    new Resource
                    {
                        Id = 2,
                        AuthorId = 1,
                        Header = "Hoho",
                        Date = new DateTime(2020, 3, 12),
                        Text = "hohohohhohohohohohohoho",
                        Domain = ResourceDomain.General,
                        SeenCounter = 4
                    },
                    new Resource
                    {
                        Id = 3,
                        AuthorId = 2,
                        Header = "Nešto smisleno",
                        Date = new DateTime(2020, 4, 12),
                        Text = "Lorem ipsum or sumtin",
                        Domain = ResourceDomain.Marketing,
                        SeenCounter = 4
                    },
                    new Resource
                    {
                        Id = 100,
                        AuthorId = 999,
                        Header = "Jesus was here!",
                        Date = new DateTime(1,1,1),
                        Text = "Need more proof? im real! Just look at the date!",
                        Domain = ResourceDomain.General,
                        SeenCounter = 9999
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
                        CommentId = null,
                        UpVotes = 3,
                        DownVotes = 1
                    },
                    new Comment
                    {
                        Id = 5,
                        AuthorId = 2,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        ResourceId = 1,
                        CommentId = 4,
                        UpVotes = 3,
                        DownVotes = 1
                    },
                    new Comment
                    {
                        Id = 6,
                        AuthorId = 3,
                        Date = new DateTime(2020, 5, 12),
                        Text = "Hehehehehehehehehehe",
                        ResourceId = 1,
                        CommentId = 5,
                        UpVotes = 3,
                        DownVotes = 1
                    },
                    new Comment
                    {
                        Id = 7,
                        AuthorId = 1,
                        Date = new DateTime(2020, 5, 12),
                        Text = "hohohohohoho",
                        ResourceId = 2,
                        CommentId = null,
                        UpVotes = 3,
                        DownVotes = 1
                    }
                });

            builder.Entity<Reaction>()
                .HasData(new List<Reaction>
                {
                    new Reaction
                    {
                        Id = 1,
                        ReactorId = 1,
                        CommentId = 4,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                       Id = 2,
                        ReactorId = 1,
                        CommentId = 5,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                       Id = 3,
                        ReactorId = 1,
                        CommentId = 6,
                        IsUpVote = true
                    },
                    new Reaction
                    {
                        Id = 4,
                        ReactorId = 1,
                        CommentId = 7,
                        IsUpVote = true
                    }
                });

            builder.Entity<Perception>()
                .HasData(new List<Perception>
                {
                    new Perception
                    {
                        Id = 1,
                        PerceiverId = 1,
                        ResourceId = 1
                    },
                    new Perception
                    {
                        Id = 2,
                        PerceiverId = 1,
                        ResourceId = 2
                    },
                    new Perception
                    {
                        Id = 3,
                        PerceiverId = 1,
                        ResourceId = 3
                    }
                });
        }
    }
}
