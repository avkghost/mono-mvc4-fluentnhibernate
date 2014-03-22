using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PushSharp;
using PushSharp.Core;
using PushSharp.Android;

using log4net;

namespace MVC4.Controllers
{
    public class PushController : Controller
    {
		public static readonly ILog log = LogManager.GetLogger(typeof(PushController));

        public ActionResult Index()
        {
			//Create our push services broker
			var push = new PushBroker();

			//Wire up the events for all the services that the broker registers
			push.OnNotificationSent += NotificationSent;
			push.OnChannelException += ChannelException;
			push.OnServiceException += ServiceException;
			push.OnNotificationFailed += NotificationFailed;
			push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
			push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
			push.OnChannelCreated += ChannelCreated;
			push.OnChannelDestroyed += ChannelDestroyed;


			push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyAOzXibpgbeXJpAzRvo7GorjZIwoq_-ld0"));

			push.QueueNotification(new GcmNotification().ForDeviceRegistrationId("DEVICE REGISTRATION ID HERE")
			                       .WithJson(@"{""alert"":""Hello World!"",""badge"":7,""sound"":""sound.caf""}"));


			//Stop and wait for the queues to drains
			push.StopAllServices();

            return View ();
        }

		public void NotificationSent (object sender, INotification notification)
		{
		}

		public void ChannelException (object sender, IPushChannel channel, Exception error)
		{
			log.Error (error);
		}

		public void ServiceException (object sender, Exception error)
		{
			log.Error (error);
		}

		public void NotificationFailed (object sender, INotification notification, Exception error)
		{
			log.Error (error);
		}

		public void DeviceSubscriptionExpired (object sender, string expiredSubscriptionId, DateTime expirationDateUtc, INotification notification)
		{
		}

		public void DeviceSubscriptionChanged (object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
		{
		}

		public void ChannelCreated (object sender, IPushChannel pushChannel)
		{
		}

		public void ChannelDestroyed (object sender)
		{
		}
    }
}
