function getSampleUrl() {
    return document.getElementById('sampleUrl');
}

function getPolicy() {
    return document.getElementById('policy');
}

function getResult() {
    return document.getElementById('result');
}

function displayPolicyError(isError) {
    var policy = getPolicy();
    var result = getResult();

    if (isError) {
        policy.classList.add("error");
        result.classList.add("error");
        result.classList.remove("success");
    } else {
        policy.classList.remove("error");
        result.classList.add("success");
        result.classList.remove("error");
    }
}

function displayResultError(isError) {
    var result = getResult();

    if (isError) {
        result.classList.add("error");
        result.classList.remove("success");
    } else {
        result.classList.add("success");
        result.classList.remove("error");
    }
}

function startScript() {
    //  Update one on start
    updateProxy()

    //  Update when the sample URL is changed
    sampleUrl.oninput = function () {
        updateProxy()
    };
    //  Update when the policy is changed
    policy.oninput = function () {
        updateProxy()
    };
}

function updateProxy(sampleUrl, policy) {
    try {
        var policy = getPolicy();
        var policyObj = JSON.parse(policy.innerHTML);
        var url = getSampleUrl().value;
        var payload = {
            requestUrl: url,
            policy: policyObj
        };
        var xhr = new XMLHttpRequest();
        var result = getResult();

        displayPolicyError(false);
        xhr.onreadystatechange = function () {
            if (xhr.status === 200) {
                result.innerHTML = xhr.responseText
                displayResultError(false);
            }
            else {
                displayResultError(true);
            }
        };
        xhr.open("POST", "/api/proxy", true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.send(JSON.stringify(payload));
    }
    catch (err) {
        displayPolicyError(true);
    }
}