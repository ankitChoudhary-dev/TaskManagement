import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../models/login-request';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {


  isSubmitted = false;


  loginForm;


  constructor(
    private fb: FormBuilder,
    private authService: Auth
  ){

    this.loginForm = this.fb.group({

      email: [
        '',
        [
          Validators.required,
          Validators.email
        ]
      ],

      password: [
        '',
        [
          Validators.required
        ]
      ]

    });

  }



  onSubmit(){
    console.log("ON SUBMIT CALLED");

    this.isSubmitted = true;


    if(this.loginForm.invalid){
      return;
    }


    const request: LoginRequest = {

      email: this.loginForm.value.email ?? '',

      password: this.loginForm.value.password ?? ''

    };


    this.authService.login(request)
    .subscribe({

      next:(response)=>{

        console.log(
          "LOGIN SUCCESS",
          response
        );

      },


      error:(error)=>{

        console.log(
          "LOGIN ERROR",
          error
        );

      }

    });


  }

}