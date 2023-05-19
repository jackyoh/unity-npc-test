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

public class AWSPluginTest : MonoBehaviour {

    public void Start() {
        Debug.Log("RUN AWS PLUGIN TEST ..................");
    }
}
