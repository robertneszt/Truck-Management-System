var pickupCity = document.querySelector('.pickUpCity'),
    pickupState = document.querySelector('.pickUpState'),
    pickupCountry = document.querySelector('.pickUpCountry'),
    deliveryCity = document.querySelector('.deliveryCity'),
    deliveryState = document.querySelector('.deliveryState'),
    deliveryCountry = document.querySelector('.deliveryCountry'),
    contentDiv = document.getElementById('weatherContent');
    

const pickupCityName = (pickupCity.innerHTML).trim(),
    pickupStateName = (pickupState.innerHTML).trim(),  
    pickupCountryName = (pickupCountry.innerHTML).trim(),  
    deliveryCityName = (deliveryCity.innerHTML).trim(),
    deliveryStateName = (deliveryState.innerHTML).trim(),
    deliveryCountryName = (deliveryCountry.innerHTML).trim()

const configfile = {
  /*  cUrl: 'http://api.weatherstack.com/current',
    access_key: '19ef65d00abf0a6eb052e55721852b4e'*/
}
const requestUrls = {
    
    base: ' https://api.weatherapi.com/v1/current.json',
    furute: ' https://api.weatherapi.com/v1/forecast.json'
} 
const querryParams = {
    key: '017f74a24d1d4d51be8235820232008',
    q: "bulk"
}

const queryString = new URLSearchParams(querryParams).toString();
const apiUrl = `${requestUrls.base}?${queryString}`;
function  LoadWeather() {
    const postData = {
        "locations": [
            {
                "q": pickupCityName,
                "custom_id": pickupStateName
            },
            {
                "q": deliveryCityName,
                "custom_id": deliveryStateName
            }

        ]
    }
    fetch(apiUrl, {
        method: 'POST',
        headers: {'Content-type': 'application/json' },
        body: JSON.stringify(postData)
        })
        .then(Response => Response.json())
        .then(data => {
            data.bulk.forEach(bulk => {
               // console.log(bulk.query.current.temp_c + " " + bulk.query.current.condition.icon )

               
          
                // Insert image
                const imageElement = document.createElement('img');
                imageElement.src = bulk.query.current.condition.icon;
                // Insert text
                const textElement = document.createElement('p');
                textElement.textContent = "The Current weather conditions at " + bulk.query.location.name + ": " + bulk.query.current.temp_c + " C" + ", wind conditions, " + bulk.query.current.wind_kph + " km/hr";

                contentDiv.appendChild(imageElement);
                contentDiv.appendChild(textElement);
              

            })
        //console.log(data.current.temp_c)
    })
 
}


