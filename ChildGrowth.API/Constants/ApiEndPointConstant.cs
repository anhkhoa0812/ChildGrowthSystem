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
        public const string SharedData = ConsultationEndpoint + "/shared-data";
        public const string Pending = ConsultationEndpoint + "/pending";
        public const string PendingById = Pending + "/{id}";
    }

    public static class Doctor
    {
        public const string DoctorEndPoint = ApiEndpoint + "/doctors";
        public const string ConsultationDoctor = DoctorEndPoint + "/consultations";
        public const string GetChildProfile = DoctorEndPoint + "/consultations/{id}";
        public const string ApproveConsultation = DoctorEndPoint + "/consultations/{consultationId}/approve";
        public const string RequestChildGrowthRecord = DoctorEndPoint + "/consultations/{consultationId}/request";
        public const string Dashboard = DoctorEndPoint + "/dashboard";
    }


    public static class Child
    {
        public const string ChildEndPoint = ApiEndpoint + "/children";
        public const string GetById = ChildEndPoint + "/{childId}";
        public const string Create = ChildEndPoint;
        public const string Update = ChildEndPoint + "/{childId}";
        public const string Delete = ChildEndPoint + "/{childId}";
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
    public static class GrowthRecord
    {
        public const string GrowthRecordEndPoint = ApiEndpoint + "/growth-records";
        public const string GetById = GrowthRecordEndPoint + "/{recordId}";
        public const string Create = GrowthRecordEndPoint;
        public const string Update = GrowthRecordEndPoint + "/{recordId}";
        public const string Delete = GrowthRecordEndPoint + "/{recordId}";
    }
    public static class Blog
    {
        public const string BlogEndPoint = ApiEndpoint + "/blogs";
        public const string GetById = BlogEndPoint + "/{blogId}";
        public const string Create = BlogEndPoint;
        public const string Update = BlogEndPoint + "/{blogId}";
        public const string Delete = BlogEndPoint + "/{blogId}";
        public const string Search = BlogEndPoint + "/search";
        public const string View = BlogEndPoint + "/{blogId}/view";
        public const string Like = BlogEndPoint + "/{blogId}/like";
        public const string Comment = BlogEndPoint + "/{blogId}/comment";
    }
}