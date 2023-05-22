using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

using System;
using System.Threading.Tasks;
using System.Net;
using TMPro;

public class AWSPluginTest : MonoBehaviour {
    private IAmazonCognitoIdentityProvider cognitoService;
    public TextMeshProUGUI textComponent;

    public async void Start() {
        cognitoService = new AmazonCognitoIdentityProviderClient(
            new AnonymousAWSCredentials(), RegionEndpoint.USEast1);
        
        /*var userAttrs = new AttributeType {
            Name = "email",
            Value = "jack@is-land.com.tw"
        };
        var userAttrsList = new List<AttributeType>();
        userAttrsList.Add(userAttrs);

        var signUpRequest = new SignUpRequest {
            UserAttributes = userAttrsList,
            Username = "user100",
            ClientId = "7bdudnl6iogq0cthqumnne8bai",
            Password = "9775630345"
        };
        var response = await cognitoService.SignUpAsync(signUpRequest);
        Debug.Log(response);

        if (response.HttpStatusCode == HttpStatusCode.OK) {
            Debug.Log("The user100 was created");
        }*/

        /*string username = "user100";
        string code = "114492";
        var confirmSignUpRequest = new ConfirmSignUpRequest {
            ClientId = "7bdudnl6iogq0cthqumnne8bai",
            ConfirmationCode = code,
            Username = username
        };

        var response = await cognitoService.ConfirmSignUpAsync(confirmSignUpRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK) {
            Debug.Log(username + " was confirmed.");
        }*/

        String username = "user100";
        String password = "9775630345";

        var authParameters = new Dictionary<string, string>();
        authParameters.Add("USERNAME", username);
        authParameters.Add("PASSWORD", password);

        var authRequest = new InitiateAuthRequest {
            ClientId = "7bdudnl6iogq0cthqumnne8bai",
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };
        InitiateAuthResponse response = await cognitoService.InitiateAuthAsync(authRequest);
        var authResult = response.AuthenticationResult;
        // var idToken = authResult.IdToken;
        // textComponent.text = idToken;
        textComponent.text = authResult.IdToken;
    }
}
