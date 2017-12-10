using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PotLogService
{

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void CreateUser(string Firstname, string Lastname, string Email, string Password);

        [OperationContract]
        User LogIn(string email, string clearTextPw);

        [OperationContract]
        Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic, User admin);

        [OperationContract]
        Event FindEventById(int id);

        [OperationContract]
        void SignUpForEvent(string userEmail, int eventId);

        [OperationContract]
        bool IsRegisteredToEvent(User u, Event e);

        [OperationContract]
        Category FindCategoryById(int id);


        [OperationContract]
        User UpdateUserInfo(User u);

        [OperationContract]
        void AddCategoryToEvent(int eventId, string categoryTitle, string categoryDescription, Component parent);

        [OperationContract]
        void AddItemToCategory(int eventId, int categoryId, int amount, string itemTitle, string itemDescription);

        [OperationContract]
        List<Component> FindComponentByParentId(int id);
    }
}
