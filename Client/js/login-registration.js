import { Passenger } from "./Passenger.js";

    let RegisterBtn = document.querySelector(".addPassenger");
    RegisterBtn.addEventListener("click", function(){
            let name = document.querySelector(".name").value;
            if (name == "") {
                console.log("Unesite ime");
                return;
            }
            let surname = document.querySelector(".surname").value;
            if (surname == "") {
                console.log("Unesite prezime");
                return;
            }
            let email = document.querySelector(".email").value;
            if (email == "") {
                console.log("Unesite email adresu");
                return;
            }
            let password = document.querySelector(".password").value;
            if(password == "")
            {
                console.log("Unesite lozinku");
                return;
            }
            let nationality = document.querySelector(".nationality").value;
            if(nationality == "")
            {
                console.log("INacionalnost");
                return;
            }

            let phone = document.querySelector(".phone").value;
            if(phone == "")
            {
                console.log("Unesite broj telefona");
                return;
            }
            let birthday = document.querySelector(".birthday").value;
            if(birthday == "")
            {
                console.log("Unesite datum rodjenja");
                return;
            }
            let passport = document.querySelector(".passport").value;
            if(passport == "")
            {
                console.log("Unesite broj vaseg pasosa");
                return;
            }
            let street = document.querySelector(".street").value;
            if(street == "")
            {
                console.log("Unesite ulicu");
                return;
            }
            let stnumber = document.querySelector(".stnumber").value;
            if(stnumber == "")
            {
                console.log("Unesite broj");
                return;
            }
            console.log(name, surname);

        /*fetch(`http://localhost:5163/Passenger/AddPassenger/`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			email: email,
            password: password,
            name: name,
            surname:surname,
            nationality:nationality,
            passport: passport,
            birthday:birthday,
            phone:phone,
            street:street,
            stnumber:stnumber
		})
        }).then(p=>{
            if(p.ok){
                location.reload();
            }
            else
            {
                console.log("nesto je poslo po zlu iks de");
            }
        }).catch(errorMsg=>console.log(errorMsg));*/
});
