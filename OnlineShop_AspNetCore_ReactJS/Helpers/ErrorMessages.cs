using System;

namespace OnlineShop_AspNetCore_ReactJS.Helpers
{
    public static class ErrorMessage
    {
        public static string InvalidData(string errorType, Type errorEntity, string errorDetail, string errorData)
        {
            return $"[Error]: {errorType}. {errorEntity.Name} {errorDetail} {errorData} is invalid";
        }
    }
}
