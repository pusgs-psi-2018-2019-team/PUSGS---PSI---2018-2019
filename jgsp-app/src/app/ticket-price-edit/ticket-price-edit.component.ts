import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TicketPriceEditHttpService } from 'src/services/ticketPriceEdit.service';
import { Router } from '@angular/router';
import { TempTicket } from '../models/tempTicket';

@Component({
  selector: 'app-ticket-price-edit',
  templateUrl: './ticket-price-edit.component.html',
  styleUrls: ['./ticket-price-edit.component.css']
})
export class TicketPriceEditComponent implements OnInit {

  ticketPriceEdit = this.fb.group({
    Price: ['', Validators.required],
  });

  ticketTypeId: number
  selectedTicket: string
  allTicketTypes: string[] = ["Vremenska karta", "Dnevna karta", "Mesečna karta", "Godišnja karta"]

  tempTicket: TempTicket = new TempTicket()
  constructor(private fb: FormBuilder, private http: TicketPriceEditHttpService, private router: Router) { }

  ngOnInit() {
    this.selectedTicket = this.allTicketTypes[0]
  }

  Izaberi(){
    if(this.selectedTicket == "Vremenska karta"){
      this.ticketTypeId = 1
    }
    else if(this.selectedTicket == "Dnevna karta"){
      this.ticketTypeId = 2
    }
    else if(this.selectedTicket == "Mesečna karta"){
      this.ticketTypeId = 3
    }
    else if(this.selectedTicket == "Godišnja karta"){
      this.ticketTypeId = 4
    }

    this.http.getPrice(this.ticketTypeId).subscribe((item) =>  {
        console.log(item)
        this.ticketPriceEdit.patchValue({Price: item})
      
    });
  }

  Izmeni(){
    console.log("AAAAAAAAAa")
    this.tempTicket = this.ticketPriceEdit.value
    this.http.editPrice(this.ticketTypeId, this.tempTicket.Price).subscribe((item) =>  {
      if (item == "uspesno") {
        alert("INFO: Uspesno izmenjena cena.")
        this.router.navigate(["/home"]);
      } 
      else {
        err => console.log("Greska pri izmeni profila");
      }
    });
  }

}
