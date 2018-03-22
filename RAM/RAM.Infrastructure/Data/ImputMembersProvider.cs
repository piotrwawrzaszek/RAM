using System;
using System.Collections.Generic;
using System.Linq;
using RAM.Domain.Model;
using RAM.Infrastructure.Services;

namespace RAM.Infrastructure.Data
{
    public interface IImputMembersProvider : IDataProvider
    {
        TapeMember GetTapeMember(Guid tapeMemberId);
        IEnumerable<TapeMember> GetAllTapeMembers();
    }

    public class ImputMembersProvider : IImputMembersProvider
    {
        private readonly Func<IFileDataService> _fileDataServiceCreator;

        public ImputMembersProvider(Func<IFileDataService> fileDataServiceCreator)
        {
            _fileDataServiceCreator = fileDataServiceCreator;

            _tapeMembers = new List<TapeMember>
            {
                new TapeMember(1, "aaa"),
                new TapeMember(2, "4"),
                new TapeMember(3, "bbc")
            };
        }

        private readonly IEnumerable<TapeMember> _tapeMembers;

        public TapeMember GetTapeMember(Guid tapeMemberId)
            => _tapeMembers.SingleOrDefault(x => x.Id == tapeMemberId);

        public IEnumerable<TapeMember> GetAllTapeMembers()
            => _tapeMembers;
    }
}