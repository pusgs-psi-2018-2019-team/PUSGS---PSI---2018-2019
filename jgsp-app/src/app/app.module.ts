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
import { RegisterComponent } from './register/register.component';
import { UiModule } from './ui/ui.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RedvoznjeComponent } from './redvoznje/redvoznje.component';
import { RedVoznjeHttpService } from 'src/services/redvoznje.service';
import { CardVerificationComponent } from './card-verification/card-verification.component';
import { CardVerificationHttpService } from 'src/services/cardVerification.service';

const routes : Routes = [
  {path : "login", component: LoginComponent},
  {path : "home", component: HomeComponent},
  {path : "register", component: RegisterComponent},
  {path : "redvoznje", component: RedvoznjeComponent},
  {path: "cardVerification", component: CardVerificationComponent},
  {path : "", component: HomeComponent, pathMatch:"full"},
  {path : "**", redirectTo: "login"},
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    RedvoznjeComponent,
    CardVerificationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),  
    ReactiveFormsModule,
    UiModule  
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, AuthHttpService, RedVoznjeHttpService, CardVerificationHttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
