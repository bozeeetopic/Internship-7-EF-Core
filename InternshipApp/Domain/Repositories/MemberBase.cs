using Data.Entities;
using Data.Entities.Models;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public class MemberBase : RepositoryBase
    {
        public MemberBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(Member member)
        {
            DbContext.Members.Add(member);

            return SaveChanges();
        }
        public ResponseResultType Edit(Member member, int memberId)
        {
            var edittingMember = DbContext.Members.Find(memberId);
            if (edittingMember is null)
            {
                return ResponseResultType.NotFound;
            }
            edittingMember.Name = member.Name;
            edittingMember.Surname = member.Surname;
            edittingMember.Username = member.Username;
            edittingMember.Password = member.Password;

            return SaveChanges();
        }

        public ICollection<Member> GetAll() => DbContext.Members.ToList();
    }
}
