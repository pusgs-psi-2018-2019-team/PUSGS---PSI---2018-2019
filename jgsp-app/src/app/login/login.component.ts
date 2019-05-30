import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthHttpService } from 'src/services/http/auth.service';
import { User } from '../user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http:AuthHttpService) { }

  ngOnInit() {
  }

  login(user: User,form: NgForm){
    this.http.logIn(user.username,user.password);
    form.reset();
  }
}
