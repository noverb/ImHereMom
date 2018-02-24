using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Firebase.Iid;

namespace ImHereMom_Client.BusinessLayer
{
    [Service]
    [IntentFilter (new[] {"com.google.firebase.INSTANCE_ID_EVENT"})]
    public class MyFirebaseIidService: FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIidService";

        public override void OnTokenRefresh()
        {
            //base.OnTokenRefresh();
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, $"Refreshed Token: {refreshedToken}");
            SendRegistrationToServer(refreshedToken);
        }

        private void SendRegistrationToServer(string token)
        {
            Log.Debug(TAG, $"If you had a Server, this token {token} would be sent to it");
            // TBD what to do
        }
    }
}