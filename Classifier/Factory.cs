using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public interface IFactory
    {
        void Execute();
        IOutputData outputData { get; }
    }

    public class Factory : IFactory
    {
        NodeFeed mf = new NodeFeed();
        IInputData data;
        ICodes Codes { get; set; }
        ISearchCodes SearchingResult { get; set; }
        CodeProcessing processing { get; set; }
        ITypeAndKind Types { get; set; }
        IBTI Bti { get; set; }
        public IOutputData outputData { get; private set; }

        public Factory(IInputData data)
        {
            this.data = data;
            Codes = new Codes(mf);
        }

        private IBTI CreateBTI()
        {
            return new BTI(data.BtiVri, data.Lo_lvl, data.Mid_lvl, data.Hi_lvl);
        }

        private ISearchCodes CreateISearch()
        {
            return new SearchCodes(data.Vri_doc, Codes, mf);
        }

        private CodeProcessing CreateProcessing()
        {
            return new CodeProcessing(Codes, Bti, data.Vri_doc, data.Area, SearchingResult.IsFederalSearch, mf);
        }

        private ITypeAndKind CreateTypes()
        {
            return new TypeAndKind(Codes);
        }

        private IOutputData CreateOutputData()
        {
            return new OutputData(Codes.Show, SearchingResult.Matches,
                SearchingResult.IsMainSearch, SearchingResult.IsPZZSearch,
                    SearchingResult.IsFederalSearch, processing.Landscaping,
                        processing.Maintenance, Types.Type, Types.Kind);
        }

        public void Execute()
        {
            SearchingResult = CreateISearch();
            SearchingResult.MainLoop();
            Bti = CreateBTI();
            processing = CreateProcessing();
            processing.FullProcessing();
            Types = CreateTypes();

            outputData = CreateOutputData();
        }
    }
}
