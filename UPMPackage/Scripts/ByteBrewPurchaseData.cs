using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ByteBrewSDK
{
	[Serializable]
    public class ByteBrewPurchaseData
    {
		/// <summary>
        /// tells if the purchase is real or fake. True for Real, and False for Fake. If you want more info on the result check the returned message.
        /// </summary>
		public bool isValid;
		/// <summary>
        /// Extra info on whether the purchase was actually sent to servers and processed correctly, if False check returned message.
        /// </summary>
		public bool purchaseProcessed;
		/// <summary>
        /// Message telling the reason and explaination of the result. 
        /// </summary>
		public string message;
		/// <summary>
        /// Item/Product ID of the purchase sent to get verified.
        /// </summary>
		public string itemID;
		/// <summary>
        /// Platform purchase belongs to, ie. iOS, Android.
        /// </summary>
		public string platform;
		/// <summary>
        /// Time Verification occured for the purchase.
        /// </summary>
		public string timestamp;
    }
}