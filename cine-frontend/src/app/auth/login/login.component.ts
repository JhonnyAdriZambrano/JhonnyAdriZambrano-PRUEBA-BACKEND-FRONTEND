import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginErrorMessage: string | null = null;

  //Datos
  private defaulUser: string = 'admin';
  private defaulPass: string = '1234';

  constructor(private router: Router) { }

  onSubmit(form: NgForm): void{
    this.loginErrorMessage=null;
    if(form.invalid){
      form.control.markAllAsTouched();
      return;
    }

    const username = form.value.username;
    const password = form.value.password;

    //Validacion
    if (username === this.defaulUser && password === this.defaulPass) {
      console.log('Login exitoso!');

      localStorage.setItem('isLoggedIn', 'true');

      this.router.navigate(['/app/dashboard']);
    }
    else {
      console.log('Login fallido');
      this.loginErrorMessage = 'Usuario o contraseña incorrectos.';

      localStorage.removeItem('isLoggedIn');
    }
    
  }
  ngOnInit(): void {}
}

