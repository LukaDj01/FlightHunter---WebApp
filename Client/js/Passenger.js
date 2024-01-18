export class Passenger{
    constructor(email, password, passport, phone, birth_date, nationality, first_name, last_name, addr_street, addr_stNo, feedbacks, tickets){
        this.email=email;
        this.password=password;
        this.first_name=first_name;
        this.last_name=last_name;
        this.nationality=nationality;
        this.passport=passport;
        this.birth_date=birth_date;
        this.phone=phone;
        this.addr_street=addr_street;
        this.addr_stNo=addr_stNo;
        this.feedbacks=feedbacks;
        this.tickets=tickets;
    }
}