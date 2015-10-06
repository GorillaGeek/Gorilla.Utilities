using System;
using System.Runtime.Serialization;

namespace Gorilla.Utilities.Bags
{
    /// <summary>
    /// Generic class for feedback
    /// </summary>
    [DataContract]
    public class MethodFeedback
    {
        public MethodFeedback(bool success = false, string message = null)
        {
            this.Success = success;
            this.Message = message;
        }

        [DataMember(IsRequired = true, Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }


        public void Failed(Exception e)
        {
            this.Success = false;
            this.Message = e.ToString();
        }

        public static MethodFeedback Succeed(string message = null)
        {
            return new MethodFeedback(true, message);
        }

        public static MethodFeedback Fail(string message)
        {
            return new MethodFeedback(false, message);
        }

        public static MethodFeedback Fail(Exception ex)
        {
            return new MethodFeedback(false, ex.ToString());
        }
    }
}
