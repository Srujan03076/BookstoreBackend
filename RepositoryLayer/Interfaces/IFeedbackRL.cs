using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public bool AddFeedback(FeedbackModel model);
        List<FeedbackModel> GetFeedback(int bookId);
    }
}
