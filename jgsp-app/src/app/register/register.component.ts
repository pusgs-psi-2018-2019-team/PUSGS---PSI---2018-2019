import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from 'src/services/http/auth.service';
import { Router } from '@angular/router';
import { User } from '../user';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private http:AuthHttpService, private router: Router) { }

  ngOnInit() {
  }

  register(user: User,form: NgForm){
    this.http.register(user).subscribe(temp => {
      if(temp == "uspesno")
      {
        console.log(temp);
        this.router.navigate(["/home"])
      }
      else if(temp == "neuspesno")
      {
        console.log(temp);
        this.router.navigate(["/login"])
      }
    });
    form.reset();
  }

}
