using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using Java.IO;
using Android.Media;
using Audacious.Droid.Utility;
using Android.Content;

namespace Audacious.Droid
{
    [Activity(Label = "Audacious", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
       
        VcAudioRecorder mAudioRecorder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnTap);

            bool result = hasMicrophone();

            if(result)
            {
                button.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                button.Visibility = Android.Views.ViewStates.Gone;
                return;
            }

            mAudioRecorder = new VcAudioRecorder();

            button.Click += delegate 
            {
                if(button.Text.Equals(Resources.GetString(Resource.String.StartRecording)))
                {
                    //Start Recording
                    mAudioRecorder.mAudioFilePath = audioFilePath();
                    mAudioRecorder.startRecording();

                    button.Text = Resources.GetString(Resource.String.StopRecording);

                }
                else
                {
                    //Stop Recording
                    mAudioRecorder.stopRecording();

                    button.Text = Resources.GetString(Resource.String.StartRecording);
                }
            };

        }


        //Check feature availability
        public bool hasMicrophone()
        {
            bool has = false;
            try
            {
                has = PackageManager.HasSystemFeature(PackageManager.FeatureMicrophone);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}, Exception: {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return has;
        }

        private string audioFilePath ()
        {
            string filePath = null;
            try
            {
                File folder = new File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/" + Resources.GetString(Resource.String.app_name));
                //File folder = GetDir(Resources.GetString(Resource.String.app_name, FileCreationMode.Private));//new File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/" + Resources.GetString(Resource.String.app_name));
                bool result = false;
                if (!folder.Exists())
                {
                    result = folder.Mkdir();
                }

                filePath = folder.Path + "/" + DateTime.Now.ToString() + ".3gp" ;
                System.Diagnostics.Debug.WriteLine("filePath :{0} ", filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}, Exception: {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return filePath;
        }

    }
}

