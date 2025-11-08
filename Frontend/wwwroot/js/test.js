var API = 'https://localhost:7151/api/BanDoc';
fetch(API)
    .then(function(response) {
        return response.json();
    })
    .then(data => {
        var post = JSON.stringify(data);
        document.getElementById('hi').innerHTML = post
    })
        

