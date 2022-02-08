using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public bool AddFeedback(FeedbackModel model);
        List<FeedbackModel> GetFeedback(int userId);
    }
}
