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
        
        private string attIDCurrentPlayer;

        private int attPreviousBodyCount;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerEnterKinectSensor.Arguments> PlayerEnterKinectSensorEvent;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerLeaveKinectSensor.Arguments> PlayerLeaveKinectSensorEvent;

        public PlayerChecker()
        {
            attIDCurrentPlayer = null;
            attPreviousBodyCount = 0;
        }

        /// <summary>
        /// Return a <see cref="string"/> value that represent the current <see cref="Model.Player"/>'s ID
        /// </summary>
        public string IDCurrentPlayer
        {
            get { return attIDCurrentPlayer; }
        }
        
        /// <summary>
        /// Check if there is a palyer standing before the kinect and if so check if it is just arrived or if he was there since before
        /// </summary>
        /// <param name="IDFound">A <see cref="string"/> representing the ID of the Player standing before the kinect,
        ///  set to null if there is no player or if there is unkwon</param>
        /// <param name="BodyCount"></param>
        public void CheckPlayer(string IDFound, int BodyCount)
        {
            Relation wvRel = CheckRelation(attIDCurrentPlayer, IDFound, BodyCount);
            switch (wvRel)
            {
                case Relation.CP_NULL_NP_VALUE:
                    attIDCurrentPlayer = IDFound;
                    RaisePlayerEnterKinectSensorEvent(IDFound);
                    break;
                case Relation.CP_VALUE_NP_NULL:
                    attIDCurrentPlayer = null;
                    RaisePlayerLeaveKinectSensorEvent();
                    break;
                case Relation.CP_VALUE_NP_DIFF:
                    attIDCurrentPlayer = null;
                    RaisePlayerLeaveKinectSensorEvent();        
                    break;
                default:
                    break;
            }

        }

        private Relation CheckRelation(string ActualID, string NewID, int BodyCount)
        {
            Relation wvRel;
            if (ActualID == null)
            {
                if (NewID == null)
                    wvRel = Relation.CP_NULL_NP_NULL;
                else
                    wvRel = Relation.CP_NULL_NP_VALUE;
            }
            else
            {
                if (NewID == null && BodyCount < 1)
                    wvRel = Relation.CP_VALUE_NP_NULL;
                else if (NewID == null && BodyCount >= 1 )//this allow to play the game to a previously recognized player
                    wvRel = Relation.CP_VALUE_NP_EQ;
                else if (NewID == ActualID)
                    wvRel = Relation.CP_VALUE_NP_EQ;
                else
                    wvRel = Relation.CP_VALUE_NP_DIFF;
            }
            return wvRel;
        }


        #region Section to raise the event PlayerEnterKinectSensor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerEnterKinectSensor(PlayerEnterKinectSensor.Arguments e)
        {
            if (PlayerEnterKinectSensorEvent != null)
            {
                PlayerEnterKinectSensorEvent(this, e );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RaisePlayerEnterKinectSensorEvent(string ID)
        {
            PlayerEnterKinectSensor.Arguments wvParameters = new PlayerEnterKinectSensor.Arguments(ID);


            OnPlayerEnterKinectSensor(wvParameters);
        }
        #endregion

        #region Section to raise the event PlayerLeaveKinectSensor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerLeaveKinectSensor(PlayerLeaveKinectSensor.Arguments e)
        {
           
            if (PlayerLeaveKinectSensorEvent != null)
            {
                PlayerLeaveKinectSensorEvent(this, e);
            }
        }

        public void RaisePlayerLeaveKinectSensorEvent()
        {
            //TODO completare i parametri
            PlayerLeaveKinectSensor.Arguments wvParameters = new PlayerLeaveKinectSensor.Arguments();


            OnPlayerLeaveKinectSensor(wvParameters);
        }
        #endregion

        /// <summary>
        /// Manage the generation of the event PlayerEnterKinectSensor that occure when a <see cref="Model.Player"/> 
        /// enter the KinectSensor region and is recognized by the game.
        /// </summary>
        public class PlayerEnterKinectSensor
        {
            /// <summary>
            /// Contains information used by the event <see cref="PlayerEnterKinectSensor"/>
            /// </summary>
            public class Arguments : EventArgs
            {

                private string attID;

                public Arguments(string ID) : base()
                {
                    attID = ID;
                }

                public string ID
                {
                    get { return attID; }
                }
                
            }

        }

        public class PlayerLeaveKinectSensor
        {
            public class Arguments : EventArgs
            {

            }
        }

        public enum Relation
        {
            STILL_NOT_AVAILABLE=-1,
            CP_NULL_NP_NULL=0,//The actual player and the next one have both null ID
            CP_NULL_NP_VALUE=1,//The actual player has null ID and the next player has an ID
            CP_VALUE_NP_NULL=2,//The actual player has an ID and the next player has null ID
            CP_VALUE_NP_VALUE=4,//Old value not used anymore 

            CP_VALUE_NP_EQ=5,//The actual player and the next one have both the same ID
            CP_VALUE_NP_DIFF=6//The actual player and th enext one have different ID
    }
        
    }
}
