using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
         bool AddFeedback(FeedbackModel model);
        List<FeedbackModel> GetFeedback(int userId);
    }
}
