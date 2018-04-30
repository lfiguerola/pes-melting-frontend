using Android.App;
using Android.Content.PM;
using Android.OS;
using MeltingApp.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace MeltingApp.Droid
{
    [Activity(Label = "MeltingApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //registrem les funcionalitats del sistema operatiu
            DependencyService.Register<IOperatingSystemMethods, OperatingSystemMethods>();

            CrossCurrentActivity.Current.Activity = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

