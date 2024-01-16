import { Passenger } from "./Passenger.js";

    let RegisterBtn = document.querySelector(".addPassenger");
    RegisterBtn.addEventListener("click", function(){
            let name = document.querySelector(".name").value;
            if (!validateField(name, "name")) {
                return;
            }
            let surname = document.getElementById(".surname").value;
            if (!validateField(surname, "surname")) {
                return;
            }
            let email = document.getElementById(".email").value;
            if (!validateField(email, "email")) 
                return;

            let password = document.getElementById(".password").value;
            if(password == "")
            {
                console.log("Izaberite dolazni gate");
                return;
            }
            let nationality = document.getElementById(".nationality").value;
            if(nationality == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }

            let phone = document.getElementById(".phone").value;
            if(phone == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let birthday = document.getElementById(".birthday").value;
            if(birthday == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let passport = document.getElementById(".passport").value;
            if(passport == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let street = document.getElementById(".street").value;
            if(street == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let stnumber = document.getElementById(".stnumber").value;
            if(stnumber == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            console.log("OCem");

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
