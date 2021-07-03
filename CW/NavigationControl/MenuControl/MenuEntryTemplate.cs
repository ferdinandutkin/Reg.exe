using System;

namespace CWRegexTester
{
    public class MenuEntryTemplate
    {

        public MenuEntryTemplate(Func<object> entryFactory, MenuEntryPreview previewInstance)
            => (EntryFactory, PreviewInstance) = (entryFactory, previewInstance);
         
        public Func<object> EntryFactory
        { get; set; }

        public MenuEntryPreview PreviewInstance
        {
            get;
            set;
        }

    }
}
