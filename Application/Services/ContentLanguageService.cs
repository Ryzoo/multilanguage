using System.Collections.Generic;
using System.Globalization;
using Application.ChainOfResponsibility.Language;
using Application.Interfaces.Database.Models;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class ContentLanguageService : IContentLanguageService
    {
        private readonly DefaultLanguageHandler _defaultLangHandler;

        public ContentLanguageService()
        {
            _defaultLangHandler = new DefaultLanguageHandler();
            
            _defaultLangHandler
                .SetNext(new EnLanguageHandler())
                .SetNext(new PlLanguageHandler());
        }
        
        public T PrepareContent<T>(ICollection<T> elements) where T : ILanguageContent
        {
            if(elements == null || elements.Count == 0)
                return default;
            
            return _defaultLangHandler.Handle(elements, CultureInfo.CurrentCulture.Name);
        }
    }
}