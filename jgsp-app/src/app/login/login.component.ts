import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthHttpService } from 'src/services/http/auth.service';
import { User } from '../user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http:AuthHttpService, private router: Router) { }

  ngOnInit() {
  }

  login(user: User,form: NgForm){
    this.http.logIn(user.Username,user.Password).subscribe(temp => {
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
