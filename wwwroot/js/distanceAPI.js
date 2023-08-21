document.addEventListener("DOMContentLoaded",() => {
    LoadWeather();
    setTimeout(() => {
        LoadDistance();
    },200)
})

var distanceContentDiv = document.getElementById('distanceContent');

const requestLinks = {

    base: ' https://api.distancematrix.ai/maps/api/distancematrix/json'
}
const requestParams = {
    origins: [pickupCityName, pickupStateName, pickupCountryName],
    destinations: [deliveryCityName, deliveryStateName, deliveryCountryName],
    departure_time: "now",
    key: 'mXShjoQqBXEMbpw2U3ZDTQe4L3AIa'
}

const queryString2 = new URLSearchParams(requestParams).toString();
const apiUrl2 = `${requestLinks.base}?${queryString2}`;

function LoadDistance() {
    
    fetch(apiUrl2, {
         headers: { 'Content-type': 'application/json' }
    })
        .then(Response => Response.json())
        .then(data => {
         
            //console.log(data.rows[0].elements[0].duration.text)
            // Insert text
            const textElementDistance = document.createElement('p');
            textElementDistance.style.color = 'blue';
            textElementDistance.textContent = "The Total Distance between pick up and delivery destination is: " + data.rows[0].elements[0].distance.text + " and Approximate travel time is: " + data.rows[0].elements[0].duration.text +" .";

            distanceContentDiv.appendChild(textElementDistance);


        })

}

