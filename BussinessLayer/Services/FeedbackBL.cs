using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class FeedbackBL: IFeedbackBL
    {
        IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;

        }

        public bool AddFeedback(FeedbackModel model)
        {
            try
            {
                return this.feedbackRL.AddFeedback(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<FeedbackModel> GetFeedback(int userId)
        {
            try
            {
                return this.feedbackRL.GetFeedback(userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
