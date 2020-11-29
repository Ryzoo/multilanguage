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
            var plLangHandler = new PlLanguageHandler();
            var enLangHandler = new EnLanguageHandler();
            var defaultLangHandler = new DefaultLanguageHandler();

            enLangHandler.SetNext(plLangHandler);
            defaultLangHandler.SetNext(enLangHandler);
            
            _defaultLangHandler = defaultLangHandler;
        }
        
        public T PrepareContent<T>(ICollection<T> elements) where T : ILanguageContent
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            return _defaultLangHandler.Handle(elements, currentCulture);
        }
    }
}