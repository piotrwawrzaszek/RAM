using System;
using System.Collections.Generic;
using System.Linq;
using RAM.Domain.Model;

namespace RAM.Infrastructure.Data
{
    public interface ITapeMemberProvider : IDataProvider
    {
        TapeMember GetTapeMember(Guid tapeMemberId);
        IEnumerable<TapeMember> GetAllTapeMembers();
    }

    public class TapeMemberProvider : ITapeMemberProvider
    {
        private readonly IEnumerable<TapeMember> _tapeMembers;

        public TapeMemberProvider()
        {
            _tapeMembers = new List<TapeMember>
            {
                new TapeMember(1, "aaa"),
                new TapeMember(2, "4"),
                new TapeMember(3, "bbc")
            };
        }

        public TapeMember GetTapeMember(Guid tapeMemberId)
            => _tapeMembers.SingleOrDefault(x => x.Id == tapeMemberId);

        public IEnumerable<TapeMember> GetAllTapeMembers()
            => _tapeMembers;
    }
}