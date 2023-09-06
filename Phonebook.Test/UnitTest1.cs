using System.Security.Cryptography.X509Certificates;

namespace Phonebook.Test
{
    public class Tests
    {
        private Phonebook phonebook;

        [SetUp]
        public void Setup()
        {
            this.phonebook = new Phonebook();
        }

        [OneTimeSetUp]
        public void OneSetup()
        {
        }

        public static Subscriber CreateSubscriber()
        {
            List<PhoneNumber> listPhone = new List<PhoneNumber> {
                new PhoneNumber("95018182923", PhoneNumberType.Work),
                new PhoneNumber("95018182923", PhoneNumberType.Personal)
            };
            return new Subscriber(Guid.NewGuid(), "Boba", listPhone);
        }
        [Test]
        public void AddSubsriber_NewSubscriber_SeccessfullyAdded()
        {
            //Arrange
            var subscriber = CreateSubscriber();

            //Act
            this.phonebook.AddSubscriber(subscriber);
            Subscriber actualSubscriber = this.phonebook.GetSubscriber(subscriber.Id);
            this.phonebook.DeleteSubscriber(actualSubscriber);

            //Assert
            Assert.AreEqual(subscriber, actualSubscriber);
        }

        [Test]
        public void DeleteSubsriber_Subscriber_SeccessfullyRemove()
        {
            //Arrange
            var subscriber = CreateSubscriber();

            //Act
            this.phonebook.AddSubscriber(subscriber);
            this.phonebook.DeleteSubscriber(subscriber);

            //Assert
            Assert.Throws<InvalidOperationException>(() => this.phonebook.GetSubscriber(subscriber.Id));
        }

        [Test]
        public void GetAll_AllSubscribers_SeccessfullyGetAll()
        {
            //Arrange
            List<Subscriber> subscribers = new List<Subscriber> { };
            for (int i = 0; i < 5; i++)
            {
                subscribers.Add(CreateSubscriber());
            }
            //Act
            foreach (var sub in subscribers)
                this.phonebook.AddSubscriber(sub);

            var actualSubscribers = this.phonebook.GetAll();

            //Assert
            CollectionAssert.AreEqual(subscribers, actualSubscribers);
        }

        [Test]
        public void GetSubscriber_Subscriber_SeccessfullyGetSub()
        {
            //Arrange
            var subscriber = CreateSubscriber();

            //Act
            this.phonebook.AddSubscriber(subscriber);
            Subscriber actualSubscriber = this.phonebook.GetSubscriber(subscriber.Id);

            //Assert
            Assert.AreEqual(subscriber, actualSubscriber);           
        }

        [Test]
        public void AddNumberToSubscriber_NewNumberToSubscriber_SeccessfullyAddNumber()
        {
            //Arrange
            var subscriber = CreateSubscriber();
            PhoneNumber number = new PhoneNumber("95018182924", PhoneNumberType.Work);

            //Act
            this.phonebook.AddSubscriber(subscriber);
            Subscriber actualSubscriber = this.phonebook.GetSubscriber(subscriber.Id);
            this.phonebook.AddNumberToSubscriber(actualSubscriber, number);

            //Assert
            Assert.AreEqual(subscriber, actualSubscriber);
        }

        [Test]
        public void RenameSubscriber_Subscriber_SeccessfullyRename()
        {
            //Arrange
            var subscriber = CreateSubscriber();
            string newName = "Biba";

            //Act
            this.phonebook.AddSubscriber(subscriber);
            Subscriber actualSubscriber = this.phonebook.GetSubscriber(subscriber.Id);
            this.phonebook.RenameSubscriber(actualSubscriber, newName);

            //Assert
            Assert.AreEqual(subscriber, actualSubscriber);

        }
    }
}