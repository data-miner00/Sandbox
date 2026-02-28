namespace Sandbox.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    internal static class ExceptionBehavior
    {
        /// <summary>
        /// To find out whether exception rethrowing is a cascading behavior.
        /// Found out it's actually not. Each catch statement is the last executing block and rethrowing does not being catch again.
        /// </summary>
        public static void Demo()
        {
            try
            {
                ProblematicMethod(exceptionType: "amex");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(nameof(InvalidOperationException));
                throw;
            }
            catch (AbandonedMutexException)
            {
                Console.WriteLine(nameof(AbandonedMutexException));
                throw;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine(nameof(DivideByZeroException));
                throw;
            }
            catch (CustomException)
            {
                Console.WriteLine(nameof(CustomException));
                throw;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine(nameof(NotSupportedException));
                throw;
            }
            catch (StackOverflowException)
            {
                Console.WriteLine(nameof(StackOverflowException));
                throw;
            }
            catch (Exception ex)
            {
                var isKnowException = ex is InvalidOperationException
                    || ex is AbandonedMutexException
                    || ex is DivideByZeroException
                    || ex is CustomException
                    || ex is NotSupportedException
                    || ex is StackOverflowException;

                Console.WriteLine("Is know exception? {0}", isKnowException);
            }
        }

        public static void ProblematicMethod(string exceptionType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(exceptionType);

            if (exceptionType == "ioex")
            {
                throw new InvalidOperationException();
            }

            if (exceptionType == "amex")
            {
                throw new AbandonedMutexException();
            }

            if (exceptionType == "dbzex")
            {
                throw new DivideByZeroException();
            }

            if (exceptionType == "nsex")
            {
                throw new NotSupportedException();
            }

            if (exceptionType == "custom")
            {
                throw new CustomException();
            }

            throw new StackOverflowException();
        }
    }

    file sealed class CustomException : Exception
    {
        public CustomException()
        {
            this.CustomErrorCode = nameof(HttpStatusCode.InternalServerError);
        }

        public CustomException(string message, string errorCode)
            : base(message)
        {
            this.CustomErrorCode = errorCode;
        }

        public CustomException(string message, Exception innerException, string errorCode)
            : base(message, innerException)
        {
            this.CustomErrorCode = errorCode;
        }

        public string CustomErrorCode { get; set; }
    }
}
