using Moq;
using Prism.Events;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;

namespace RAM.UITests.ViewModel
{
    public class RegisterPanelViewModelTests
    {
        public RegisterPanelViewModelTests()
        {
            _eventAggregatorMock = new Mock<IEventAggregator>();

            var languageChangedEventMock = new Mock<LanguageChangedEvent>();
            _eventAggregatorMock
                .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
                .Returns(languageChangedEventMock.Object);

            _registerPanelViewModel = new RegisterPanelViewModel(_eventAggregatorMock.Object);
        }

        private readonly IRegisterPanelViewModel _registerPanelViewModel;
        private readonly Mock<IEventAggregator> _eventAggregatorMock;
    }
}
