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
        public const string ResponseConsultation = ConsultationEndpoint + "/{id}/response";
        public const string FeedbackConsultation = ConsultationEndpoint + "/feedback";
    }

    public static class Doctor
    {
        public const string DoctorEndPoint = ApiEndpoint + "/doctors";
        public const string ConsultationDoctor = DoctorEndPoint + "/{doctorId}/consultations";
        public const string GetChildProfile = DoctorEndPoint + "/consultations/{id}";
    }


    public static class Child
    {
        public const string ChildEndPoint = ApiEndpoint + "/children";
        public const string GrowthRecordChild = ChildEndPoint + "/{childId}/growth-records";
        public const string GrowthAlertChild = ChildEndPoint + "/{childId}/growth-alerts";
    }

    public static class User
    {
        public const string UserEndPoint = ApiEndpoint + "/users";
        public const string SignUp = UserEndPoint + "/signup";
        public const string SignIn = UserEndPoint + "/signin";
    }
    
    public static class MembershipPlan
    {
        public const string MembershipPlanEndPoint = ApiEndpoint + "/membership-plans";
        public const string GetMembershipPlanById = MembershipPlanEndPoint + "/{id}";
        public const string CreateMembershipPlan = MembershipPlanEndPoint;
        public const string UpdateMembershipPlan = MembershipPlanEndPoint;
        public const string InactiveMembershipPlan = MembershipPlanEndPoint + "/{id}";
    }
}