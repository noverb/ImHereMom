using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Util;
using Firebase.Messaging;
using Firebase.Iid;
using ImHereMom_Client.BusinessLayer;
using Firebase;

namespace ImHereMom_Client
{
    [Activity(Label = "I'm Here Mom", MainLauncher = true)]
    public class MainActivity : Activity
    {
        const string TAG = "MainActivity";
        TextView txtMsg;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            txtMsg = FindViewById<TextView>(Resource.Id.txtMsg);
            Button btnlogToken = FindViewById<Button>(Resource.Id.btnlogToken);

            IsPlayServicesAvailable();
            btnlogToken.Click += delegate
            {
                try
                {
                    Log.Debug(TAG, $"Instance Id Token: {FirebaseInstanceId.Instance.Token}");
                    Toast.MakeText(this, $"Instance Id Token: {FirebaseInstanceId.Instance.Token}", ToastLength.Long).Show();
                }
                catch (System.Exception e)
                {
                    // When requesting the token, check for connectivity to the internet
                    Toast.MakeText(this, $"Check your Internet connection. {e.Message}", ToastLength.Long).Show();
                }
            };
            
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, $"Key: {key} - Value: {value}");
                    Toast.MakeText(this, $"Key: {key} - Value: {value}",ToastLength.Long);
                }
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            IsPlayServicesAvailable();
        }

        public bool IsPlayServicesAvailable()
        {
            // There is a result code of type integer
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    txtMsg.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    txtMsg.Text = "Sorry pal, this device is not supported. You need a newer version of Google Play Services";
                    Finish(); // Close the Activity!!!
                }
                return false;
            }
            else
            {
                txtMsg.Text = "You are in luck! Google Play Services is up-to-par";
                return true;
            }
        }
    }
}

