using Merjane.Business.Interfaces;

namespace Merjane.Business.Implementations
{
    // WARN: Should not be changed during the exercise
    public class NotificationService : INotificationService
    {
        public void SendDelayNotification(int leadTime, string productName)
        {
        }

        public void SendOutOfStockNotification(string productName)
        {
        }

        public void SendExpirationNotification(string productName, DateTime expiryDate)
        {
        }
    }
}
