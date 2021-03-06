/*
 * NOTE: Generated using OracleSDKGenerator, API Version: 20180115
 * DO NOT EDIT this file manually.
 *
 * Copyright (c) 2020, Oracle and/or its affiliates.
 * This software is dual-licensed to you under the Universal Permissive License (UPL) 1.0 as shown at https://oss.oracle.com/licenses/upl or Apache License 2.0 as shown at http://www.apache.org/licenses/LICENSE-2.0. You may choose either license.
 */

using System;
using System.Management.Automation;
using Oci.Common.Retry;
using Oci.DnsService;

namespace Oci.DnsService.Cmdlets
{
    public abstract class OCIDnsCmdlet : Oci.PSModules.Common.Cmdlets.OCICmdlet
    { 

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            retryConfig = (NoRetry) ? null : new RetryConfiguration();
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            client?.Dispose();
            client = new DnsClient(AuthProvider, new Oci.Common.ClientConfiguration
            {
                RetryConfiguration = retryConfig,
                TimeoutMillis = TimeOutInMillis,
                ClientUserAgent = PSUserAgent
            });
            try
            {
                string region = GetPreferredRegion();
                if (region != null)
                {
                    WriteDebug("Choosing Region:" + region);
                    client.SetRegion(region);
                }
                if (Endpoint != null)
                {
                    WriteDebug("Choosing Endpoint:" + Endpoint);
                    client.SetEndpoint(Endpoint);
                }
            }
            catch (Exception ex)
            {
                TerminatingErrorDuringExecution(ex);
            }
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            client.Dispose();
        }

        protected void TerminatingErrorDuringExecution(Exception ex)
        {
            ErrorRecord er;
            if (ex == null)
            {
                ex = new OperationCanceledException("Cmdlet execution interrupted");
                er = new ErrorRecord(ex, "Interrupted", ErrorCategory.OperationStopped, null);
            }
            else
            {
                er = new ErrorRecord(ex, ex.GetType().ToString(), ErrorCategory.NotSpecified, client);
                if (ex is OperationCanceledException)
                {
                    er.ErrorDetails = new ErrorDetails("Operation timed out. Retry with a larger TimeOutInMillis value");
                }
            }
            client.Dispose();
            FinishProcessing(ex);
            //ThrowTerminatingError will be the last statement as this throws pipeline stopped
            //exception which is unhandled by any OCICmdlet and control flow goes to the caller of the cmdlet
            ThrowTerminatingError(er);
        }

        protected DnsClient client;
        private RetryConfiguration retryConfig;
    }
}