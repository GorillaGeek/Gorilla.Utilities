using System;
using System.Runtime.Serialization;

namespace Gorilla.Utilities.Bags
{
    /// <summary>
    /// Generic class for feedback with typed Data
    /// </summary>
    /// <typeparam name="T">Class to type Data</typeparam>
    [DataContract]
    public class TypedMethodFeedback<T>
    {
        private T _data;

        public TypedMethodFeedback(bool success = false, string message = null)
        {
            this.Success = success;
            this.Message = message;
        }

        [DataMember(IsRequired = true, Name = "success")]
        public bool Success { get; set; }


        [DataMember(Name = "message")]
        public string Message { get; set; }


        [DataMember(Name = "data")]
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;
                if (value != null)
                {
                    this.Success = true;
                }
            }
        }

        public void Failed(Exception e)
        {
            this.Success = false;
            this.Message = e.ToString();
        }


        public static TypedMethodFeedback<T> Succeed(T data, string message = null)
        {
            return new TypedMethodFeedback<T>(true, message)
            {
                Data = data
            };
        }

        public static TypedMethodFeedback<T> Fail(string message)
        {
            return new TypedMethodFeedback<T>(false, message);
        }

        public static TypedMethodFeedback<T> Fail(Exception ex)
        {
            return new TypedMethodFeedback<T>(false, ex.ToString());
        }
    }
}
