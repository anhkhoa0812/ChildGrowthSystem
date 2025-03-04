namespace ChildGrowth.API.Constants;

public static class ApiEndPointConstant
{
    static ApiEndPointConstant()
    {
    }
    
    public const string RootEndPoint = "/api";
    public const string ApiVersion = "/v1";
    public const string ApiEndpoint = RootEndPoint + ApiVersion;
    
    public static class Consultation
    {
        public const string ConsultationEndpoint = ApiEndpoint + "/consultations";
    }

    public static class Doctor
    {
        public const string DoctorEndPoint = ApiEndpoint + "/doctors";
        public const string ConsultationDoctor = DoctorEndPoint + "/{doctorId}/consultations";
    }
}