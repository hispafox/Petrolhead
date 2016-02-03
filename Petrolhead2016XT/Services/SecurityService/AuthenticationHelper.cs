using GalaSoft.MvvmLight.Messaging;
using Petrolhead2016XT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Petrolhead2016XT.Services.SecurityService
{
    class AuthenticationHelper
    {
        private async Task<string> CheckFingerprintAvailability()
        {
            var returnMessage = "";

            try
            {
                // Check the availability of fingerprint authentication.
                var ucvAvailability = await Windows.Security.Credentials.UI.UserConsentVerifier.CheckAvailabilityAsync();

                switch (ucvAvailability)
                {
                    case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.Available:
                        returnMessage = "Identity verification is available.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DeviceBusy:
                        returnMessage = "Biometric device is busy.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DeviceNotPresent:
                        returnMessage = "No identity verification method found.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DisabledByPolicy:
                        returnMessage = "Identity verification is disabled by policy.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.NotConfiguredForUser:
                        returnMessage = "The user has no identity verification methods registered. Please add a PIN to the " +
                                        "current user account and try again.";
                        break;
                    default:
                        returnMessage = "Identity verification is currently unavailable.";
                        break;
                }
            }
            catch (Exception ex)
            {
                returnMessage = "Identity authentication availability check failed: " + ex.ToString();
            }

            return returnMessage;
        }

        public async Task<string> VerifyAndDisplayAuthentication()
        {
            var status = "";

            try
            {
                status = await CheckFingerprintAvailability();
                MessageDialog dialog = new MessageDialog(status, "Identity Verification Status");
                await dialog.ShowAsync();
                
            }
            catch (Exception ex)
            {
                App.Telemetry.TrackException(ex);
                MessageDialog dialog = new MessageDialog("An error prevented the authentication check from occurring. Please try again later.");
                status = "ERROR";
            }
            return status;
        }

        private async System.Threading.Tasks.Task<string> RequestConsent(string userMessage)
        {
            string returnMessage;

            if (String.IsNullOrEmpty(userMessage))
            {
                userMessage = "Please provide fingerprint verification.";
            }

            try
            {
                // Request the logged on user's consent via fingerprint swipe.
                var consentResult = await Windows.Security.Credentials.UI.UserConsentVerifier.RequestVerificationAsync(userMessage);

                switch (consentResult)
                {
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.Verified:
                        returnMessage = "Identity verified.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.DeviceBusy:
                        returnMessage = "Identity verification service already in use.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.DeviceNotPresent:
                        returnMessage = "No verification method found.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.DisabledByPolicy:
                        returnMessage = "Identity verification is disabled by policy.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.NotConfiguredForUser:
                        returnMessage = "The user has no verification methods registered. Please add a PIN to the " +
                                        "current user account and try again.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.RetriesExhausted:
                        returnMessage = "There have been too many failed attempts. Identity verification canceled.";
                        break;
                    case Windows.Security.Credentials.UI.UserConsentVerificationResult.Canceled:
                        returnMessage = "Identity verification canceled.";
                        break;
                    default:
                        returnMessage = "Identity verification is currently unavailable.";
                        break;
                }
            }
            catch (Exception ex)
            {
                returnMessage = "Identity verification failed: " + ex.ToString();
            }

            return returnMessage;
        }

       
        public async Task RequestAuthentication(string userMessage)
        {
            try
            {
                string result = await RequestConsent(userMessage);
                MessageDialog dialog = new MessageDialog(result, "Authentication Result");
                dialog.Commands.Add(new UICommand("OK", (command) =>
                {
                    if (result != "Identity verified.")
                        throw new IdentityVerificationException(result);
                }));
                
            }
            catch (Exception ex)
            {
                App.Telemetry.TrackException(ex);
                throw new IdentityVerificationException("Identity verification error : " + ex.Message);
            }
        }
    }
}
