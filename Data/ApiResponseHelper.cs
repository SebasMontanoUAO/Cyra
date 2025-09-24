using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cyra.Data
{
    public class ApiResponseHelper
    {

        public static ApiResponse GetResponse(ResponseType responseType, string? message, object? data = null)
        {
            switch (responseType)
            {
                case ResponseType.Success:
                    return new ApiResponse
                    {
                        Code = ResponseType.Success.ToString(),
                        Message = message ?? "Operación existosa!",
                        ResponseData = data
                    };
                case ResponseType.NotFound:
                    return new ApiResponse
                    {
                        Code = ResponseType.NotFound.ToString(),
                        Message = message ?? "Recurso no encontrado.",
                        ResponseData = null
                    };
                    case ResponseType.Failure:
                    return new ApiResponse
                    {
                        Code = ResponseType.Failure.ToString(),
                        Message = message ?? "Ha ocurrido un error, operación no existosa.",
                        ResponseData = data
                    };
                default:
                    throw new ArgumentException("Tipo de respuesta no válido");
            }
        }

        public static ApiResponse ExceptionResponse(Exception ex)
        {
            return new ApiResponse
            {
                Code = ResponseType.Failure.ToString(),
                Message = ex.Message,
                ResponseData = ex
            };
        }
    }
}
