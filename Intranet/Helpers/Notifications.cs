using Intranet.Constants;
using Microsoft.Azure.NotificationHubs;

namespace Intranet.Helpers
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString(AzureNotficationHubConstants.ConnectionString,
                                                                         AzureNotficationHubConstants.HubName);
        }
    }
}
