namespace webapiasp.DTO
{
    public class UpdateGuideStatusRequest
    {
        public string OrderId { get; set; }
        public string GuideStatus { get; set; }
        public string GuideTime { get; set; }
        public string GuideDate { get; set; }
        public int GuideId { get; set; }

        public override string ToString()
        {
            return $"UpdateGuideStatusRequest [orderId={OrderId}, guideStatus={GuideStatus}, guideTime={GuideTime}, guideDate={GuideDate}]";
        }
    }
}
