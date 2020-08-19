/*
 * NOTE: Generated using OracleSDKGenerator, API Version: 20200430
 * DO NOT EDIT this file manually.
 *
 * Copyright (c) 2020, Oracle and/or its affiliates.
 * This software is dual-licensed to you under the Universal Permissive License (UPL) 1.0 as shown at https://oss.oracle.com/licenses/upl or Apache License 2.0 as shown at http://www.apache.org/licenses/LICENSE-2.0. You may choose either license.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Oci.DataintegrationService.Requests;
using Oci.DataintegrationService.Responses;
using Oci.DataintegrationService.Models;

namespace Oci.DataintegrationService.Cmdlets
{
    [Cmdlet("Get", "OCIDataintegrationWorkspacesList")]
    [OutputType(new System.Type[] { typeof(Oci.DataintegrationService.Models.WorkspaceSummary), typeof(Oci.DataintegrationService.Responses.ListWorkspacesResponse) })]
    public class GetOCIDataintegrationWorkspacesList : OCIDataIntegrationCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID of the compartment in which to list resources.")]
        public string CompartmentId { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"This filter parameter can be used to filter by the name of the object.")]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"This parameter allows users to set the maximum number of items to return per page.  The value must be between 1 and 100 (inclusive).  Default value is 100.", ParameterSetName = LimitSet)]
        public System.Nullable<int> Limit { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"This parameter will control pagination.  Values for the parameter should come from the `opc-next-page` or `opc-prev-page` header in previous response.")]
        public string Page { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"Lifecycle state of the resource.")]
        public System.Nullable<Oci.DataintegrationService.Models.Workspace.LifecycleStateEnum> LifecycleState { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"This parameter is used to control the sort order.  Supported values are `ASC` (ascending) and `DESC` (descending).")]
        public System.Nullable<Oci.DataintegrationService.Requests.ListWorkspacesRequest.SortOrderEnum> SortOrder { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"This parameter allows users to specify a sort field.  Supported sort fields are `name`, `identifier`, `timeCreated`, and `timeUpdated`.  Default sort order is the descending order of `timeCreated` (most recently created objects at the top).  Sorting related parameters are ignored when parameter `query` is present (search operation and sorting order is by relevance score in descending order).")]
        public System.Nullable<Oci.DataintegrationService.Requests.ListWorkspacesRequest.SortByEnum> SortBy { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = @"Unique Oracle-assigned identifier for the request. If you need to contact Oracle about a particular request, please provide the request ID.")]
        public string OpcRequestId { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"Fetches all pages of results.", ParameterSetName = AllPageSet)]
        public SwitchParameter All { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ListWorkspacesRequest request;

            try
            {
                request = new ListWorkspacesRequest
                {
                    CompartmentId = CompartmentId,
                    Name = Name,
                    Limit = Limit,
                    Page = Page,
                    LifecycleState = LifecycleState,
                    SortOrder = SortOrder,
                    SortBy = SortBy,
                    OpcRequestId = OpcRequestId
                };
                IEnumerable<ListWorkspacesResponse> responses = GetRequestDelegate().Invoke(request);
                foreach (var item in responses)
                {
                    response = item;
                    WriteOutput(response, response.Items, true);
                }
                FinishProcessing(response);
            }
            catch (Exception ex)
            {
                TerminatingErrorDuringExecution(ex);
            }
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
            TerminatingErrorDuringExecution(new OperationCanceledException("Cmdlet execution interrupted"));
        }

        private RequestDelegate GetRequestDelegate()
        {
            IEnumerable<ListWorkspacesResponse> DefaultRequest(ListWorkspacesRequest request) => Enumerable.Repeat(client.ListWorkspaces(request).GetAwaiter().GetResult(), 1);
            if (ParameterSetName.Equals(AllPageSet))
            {
                return req => client.Paginators.ListWorkspacesResponseEnumerator(req);
            }
            return DefaultRequest;
        }

        private ListWorkspacesResponse response;
        private delegate IEnumerable<ListWorkspacesResponse> RequestDelegate(ListWorkspacesRequest request);
        private const string AllPageSet = "AllPages";
        private const string LimitSet = "Limit";
    }
}
