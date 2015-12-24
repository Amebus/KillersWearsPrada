using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Controller
{
    /// <summary>
    /// Contains information about the current <see cref="Model.Player"/>
    /// </summary>
    class PlayerChecker
    {
        private PlayerStillOnKinectSensor attPlayerStillOnKinectSensorManager;

        private String attIDCurrentPalyer;
        private Boolean attPlayreStillOnKinectSensor;



        public PlayerChecker()
        {
            attPlayerStillOnKinectSensorManager = new PlayerStillOnKinectSensor();
            attIDCurrentPalyer = "";
            attPlayreStillOnKinectSensor = false;
            //TODO finire il costruttore e implementare la chiamata per scatenare l'evento di cambiamento di giocatore
        }

        /// <summary>
        /// Return a <see cref="String"/> value that represent the current <see cref="Model.Player"/>'s ID
        /// </summary>
        public String IDCurrentPlayer
        {
            get { return attIDCurrentPalyer; }
        }

        /// <summary>
        /// Return a <see cref="Boolean"/> value that indicate if the <see cref="Model.Player"/> is still in on screen
        /// </summary>
        public Boolean IsPlayerStillOnScreen
        {
            get { return attPlayreStillOnKinectSensor; }
        }

        /// <summary>
        /// Manage the generation of the event PlayerStillOnScreenChange that occure when change the status of the <see cref="Model.Player"/>.
        /// If the <see cref="Model.Player"/> enter or leave <see cref="Microsoft.Kinect.KinectSensor"/>
        /// </summary>
        public class PlayerStillOnKinectSensor
        {
            /// <summary>
            /// Delegate that handle the event raised by <see cref="PlayerStillOnKinectSensor"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public delegate void PlayerStillOnKinectSensorEventHandler(object sender, PlayerStillOnKinectSensorArgs e);


            /// <summary>
            /// Instance of the delegate
            /// </summary>
            public static PlayerStillOnKinectSensorEventHandler PlayerStillOnKinectSensorChanged;


            /// <summary>
            /// Raise the Event associated to the <see cref="PlayerStillOnKinectSensorEventHandler"/> delegate
            /// </summary>
            /// <param name="e"></param>
            public void RaiseEventPlayerStillOnKinectSensorChanged(PlayerStillOnKinectSensorArgs e)
            {
                if (PlayerStillOnKinectSensorChanged != null)
                {
                    PlayerStillOnKinectSensorChanged(this, e);
                }
            }

        }


        /// <summary>
        /// Contains information used by the event <see cref="PlayerStillOnKinectSensor"/>
        /// </summary>
        public class PlayerStillOnKinectSensorArgs : EventArgs
        {

            private String attNewID;
            private String attPreviousID;


            public PlayerStillOnKinectSensorArgs(String PreviousID, String NewID) : base()
            {
                attNewID = NewID;
                attPreviousID = PreviousID;
            }

            public String PreviousID
            {
                get { return attPreviousID; }
            }

            public String NewID
            {
                get { return attNewID; }
            }


        }

    }
}
