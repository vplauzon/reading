function startScript() {
    var sampleUrl = document.getElementById('sampleUrl');
    var policy = document.getElementById('policy');

    //  Update one on start
    updateProxy(sampleUrl.value, policy.value)

    //  Update when the sample URL is changed
    sampleUrl.oninput = function () {
        updateProxy(sampleUrl.value, policy.value)
    };
    //  Update when the policy is changed
    policy.oninput = function () {
        updateProxy(sampleUrl.value, policy.value)
    };
}

function updateProxy(sampleUrl, policy) {
    var result = document.getElementById('result');
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.status === 200) {
            result.innerHTML = xhr.responseText
        }
    };
    xhr.open("POST", "/api/proxy", true);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send("sampleUrl=" + encodeURI(sampleUrl) + "&selectionPolicy=" + encodeURI(policy));

    result.innerHTML = xhr.response;
}