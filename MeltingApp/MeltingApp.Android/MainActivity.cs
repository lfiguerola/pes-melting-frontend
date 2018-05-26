using Android.App;
using Android.Content.PM;
using Android.OS;
using MeltingApp.Interfaces;
using Plugin.Permissions;
using Xamarin.Forms;

namespace MeltingApp.Droid
{
    [Activity(Label = "MeltingApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            base.OnCreate(bundle);
            RegisterDependencies();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private static void RegisterDependencies()
        {
            //registrem les funcionalitats del sistema operatiu
            DependencyService.Register<IOperatingSystemMethods, OperatingSystemMethods>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

