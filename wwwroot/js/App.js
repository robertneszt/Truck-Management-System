


const config = {
    cUrl: 'https://countriesnow.space/api/v0.1/countries/',
    cUrlStates: 'https://countriesnow.space/api/v0.1/countries/states',
    cUrlCities:'https://countriesnow.space/api/v0.1/countries/state/cities'
  //  cToken: 'oXW-w5tqDI3lsDDsTieiC3CksTuPrmhFdEiTXclU-eTrD9gtCfr3TBK6ZxuscHKGx3A'
}

var countrySelect = document.querySelector('.Country'),
    countryState = document.querySelector('.State'),
    countryCity = document.querySelector('.City'),
    countryDelSelect = document.getElementById('CountryDel'),
    countryDelState = document.getElementById('StateDel'),
    countryDelCity = document.getElementById('CityDel')


function loadCountries() {
    let apiEndpoint = config.cUrlStates

    fetch(apiEndpoint)
        .then(Response => Response.json())
        .then(data => {
           // console.log( data.data.length);

            data.data.forEach(country => {
                // console.log("hi")
                const option = document.createElement('option')
                option.value = country.name
                option.textContent = country.name
                countrySelect.appendChild(option)
            })
        }).catch(error => console.error('Error loading countries:'), error)
}

function loadDelCountries() {
    let apiEndpoint = config.cUrlStates

    fetch(apiEndpoint)
        .then(Response => Response.json())
        .then(data => {
            // console.log( data.data.length);

            data.data.forEach(country => {
                // console.log("hi")
                const option = document.createElement('option')
                option.value = country.name
                option.textContent = country.name
                countryDelSelect.appendChild(option)
            })
        }).catch(error => console.error('Error loading countries:'), error)
}




function loadStates() {
    const postCountryData = countrySelect.value;
    countryState.innerHTML= '<option value=""> Select State</option>' //clearing the exixting states

    const postData = {
        country: postCountryData
    }
    //  console.log(postData);
    fetch(config.cUrlStates, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    })
        .then(Response => Response.json())
        .then(data => {
           // console.log(data.data.states)
            data.data.states.forEach(state => {
               // console.log(state)
                const option = document.createElement('option')
                option.value = state.name
                option.textContent = state.name
                countryState.appendChild(option)
            })
        }).catch(error => console.error('Error loading States:'), error)
   
}

function loadDelStates() {
    const postCountryData = countryDelSelect.value;
    countryDelState.innerHTML = '<option value=""> Select State</option>' //clearing the exixting states

    const postData = {
        country: postCountryData
    }
    //  console.log(postData);
    fetch(config.cUrlStates, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    })
        .then(Response => Response.json())
        .then(data => {
            // console.log(data.data.states)
            data.data.states.forEach(state => {
                // console.log(state)
                const option = document.createElement('option')
                option.value = state.name
                option.textContent = state.name
                countryDelState.appendChild(option)
            })
        }).catch(error => console.error('Error loading States:'), error)

}

function loadCities() {
    const postCountryData = countrySelect.value;
    const postStateData = countryState.value;

    countryCity.innerHTML = '<option value=""> Select City</option>' //clearing the exixting cities
    const postData = {
        country: postCountryData,
        state: postStateData
    }
    fetch(config.cUrlCities, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    })
        .then(Response => Response.json())
        .then(data => {
            // console.log(data.data.states)
            data.data.forEach(city => {
               // console.log(city)
                const option = document.createElement('option')
                option.value = city
                option.textContent = city
                countryCity.appendChild(option)
            })
        }).catch(error => console.error('Error loading cities:'), error)
}

function loadDelCities() {
    const postCountryData = countryDelSelect.value;
    const postStateData = countryDelState.value;

    countryDelCity.innerHTML = '<option value=""> Select City</option>' //clearing the exixting cities
    const postData = {
        country: postCountryData,
        state: postStateData
    }
    fetch(config.cUrlCities, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    })
        .then(Response => Response.json())
        .then(data => {
            // console.log(data.data.states)
            data.data.forEach(city => {
                // console.log(city)
                const option = document.createElement('option')
                option.value = city
                option.textContent = city
                countryDelCity.appendChild(option)
            })
        }).catch(error => console.error('Error loading cities:'), error)
}

window.onload = (loadCountries, loadDelCountries)