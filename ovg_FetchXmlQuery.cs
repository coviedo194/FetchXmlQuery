using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace ovg_FetchXmlQuery
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class ovg_FetchXmlQuery : PluginBase
    {
        public ovg_FetchXmlQuery(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ovg_FetchXmlQuery))
        {
            // Constructor reservado para configuración segura/no segura si se requiere.
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;
            var service = localPluginContext.PluginUserService;
            try
            {
                if (!context.InputParameters.Contains("ovg_fetchxmlquery"))
                {
                    throw new InvalidPluginExecutionException("Parametro no valido");
                }

                string fetchXmlQuery = context.InputParameters["ovg_fetchxmlquery"] as string;

                if (string.IsNullOrWhiteSpace(fetchXmlQuery))
                {
                    throw new InvalidPluginExecutionException("Parametro no valido");
                }

                EntityCollection resultado = service.RetrieveMultiple(new FetchExpression(fetchXmlQuery));
                context.OutputParameters["ovg_fetchxmlresponse"] = resultado;
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"Ha ocurrido un error: {ex.Message}");
            }



        }
    }
}
