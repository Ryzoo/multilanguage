using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.ChainOfResponsibility.Language;
using Application.Interfaces.Database.Models;

namespace Application.ChainOfResponsibility.Language
{
    public abstract class LanguageHandler : ILanguageHandler
    {
        private ILanguageHandler _nextHandler;

        public ILanguageHandler SetNext(ILanguageHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual T Handle<T>(ICollection<T> request) where T : ILanguageContent
        {
            if (_nextHandler == null)
                return request.FirstOrDefault();
            
            return _nextHandler.Handle(request);
        }
    }
}

