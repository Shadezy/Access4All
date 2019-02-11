using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Access4All.Fragments;

namespace Access4All
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.activity_main);
            
            BottomNavigationView navigation = (BottomNavigationView)FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    fragment = homeFragment.NewInstance();
                    if (fragment == null)
                        return false;
                    SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();

                    return true;

                case Resource.Id.navigation_search:
                    fragment = searchFragment.NewInstance();
                    if (fragment == null)
                        return false;
                    SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();

                    return true;

                case Resource.Id.navigation_categories:
                    fragment = categoriesFragment.NewInstance();
                    if (fragment == null)
                        return false;
                    SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();

                    return true;

            }
            return true;
        }
    }
}

