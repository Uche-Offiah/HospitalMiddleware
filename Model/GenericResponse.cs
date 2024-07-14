using System;

namespace HospitalMiddleware.Model
{
    public class GenericResponse
    {
        public int StatusCode { get; set; }
        public object? Data { get; set; }
    }
}
