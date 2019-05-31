import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { RouterModule,Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthHttpService } from 'src/services/http/auth.service';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './home/home.component';

const routes : Routes = [
  {path : "login", component: LoginComponent},
  {path : "home", component: HomeComponent},
  {path : "", component: LoginComponent, pathMatch:"full"},
  {path : "**", redirectTo: "login"},
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),    
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, AuthHttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
